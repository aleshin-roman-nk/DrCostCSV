using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.ExpenseDocuments.Abstractions.Persistence;
using Application.CurrencyDefaults.Abstractions;
using Application.Currencies.Abstractions;
using Domain;
using Microsoft.Extensions.Logging;

namespace Application.ExpenseDocuments.CreateExpenseDocument;

public sealed class CreateExpenseDocumentUseCase
{
	private readonly IExpenseDocumentRepository documentRepository;
	private readonly IBudgetLineEnsurer budgetLineEnsurer;
	private readonly IBudgetTagEnsurer budgetTagEnsurer;
	private readonly IUnitOfWork unitOfWork;
	private readonly ILogger<CreateExpenseDocumentUseCase> logger;
	private readonly ICurrencyDefaultSettingsRepository currencyDefaults;
	private readonly ICurrencyReader currencies;

	public CreateExpenseDocumentUseCase(IExpenseDocumentRepository documentRepository,
		IBudgetLineEnsurer budgetLineEnsurer, IBudgetTagEnsurer budgetTagEnsurer,
		IUnitOfWork unitOfWork, ILogger<CreateExpenseDocumentUseCase> logger,
		ICurrencyDefaultSettingsRepository currencyDefaults, ICurrencyReader currencies)
	{
		this.documentRepository = documentRepository;
		this.budgetLineEnsurer = budgetLineEnsurer;
		this.budgetTagEnsurer = budgetTagEnsurer;
		this.unitOfWork = unitOfWork;
		this.logger = logger;
		this.currencyDefaults = currencyDefaults;
		this.currencies = currencies;
	}

	public UseCaseResult<CreateExpenseDocumentResult> Execute(CreateExpenseDocumentCommand command)
	{
		var validationResult = ValidateCommand(command);
		if (!validationResult.IsValid)
			return UseCaseResult<CreateExpenseDocumentResult>.Failure(validationResult.Error!);

		var explicitValues = command.CurrencyValues;
		if (explicitValues is not null)
		{
			var currencyError = CurrencySelectionValidator.Validate(command.CurrencyId, explicitValues, currencies);
			if (currencyError is not null)
				return UseCaseResult<CreateExpenseDocumentResult>.Failure(currencyError);
		}

		var budgetLineMap = budgetLineEnsurer.EnsureAndGetMap(command.Items
			.Where(x => x.BudgetLineId is null).Select(x => x.BudgetLineName)
			.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x!.Trim())
			.Distinct(StringComparer.OrdinalIgnoreCase).ToList());

		var document = new ExpenseDocument(command.Date, command.SellerName);
		if (explicitValues is not null)
			document.SetCurrencyValues(command.CurrencyId,
				explicitValues.Select(value => new CurrencyValue(value.CurrencyId, value.Value)));
		else
		{
			var defaults = currencyDefaults.Get();
			if (defaults is not null)
				document.SetCurrencyValues(defaults.DocumentCurrencyId, defaults.CurrencyValues);
		}
		foreach (var item in command.Items)
		{
			var budgetLineId = ResolveBudgetLineId(item, budgetLineMap);
			if (!budgetLineId.HasValue)
				return UseCaseResult<CreateExpenseDocumentResult>.Failure(
					new UseCaseError("budget_line_not_found", $"Строка бюджета позиции «{item.Name}» не найдена."));

			var budgetTagId = ResolveBudgetTagId(item, budgetLineId.Value);
			var hasBudgetTag = item.BudgetTagId.HasValue || !string.IsNullOrWhiteSpace(item.BudgetTagName);
			if (hasBudgetTag && !budgetTagId.HasValue)
				return UseCaseResult<CreateExpenseDocumentResult>.Failure(
					new UseCaseError("budget_tag_not_found", $"Уточняющий тег позиции «{item.Name}» не найден в выбранной строке бюджета."));

			document.AddItem(item.Name, item.Price, item.Amount, budgetLineId.Value, budgetTagId);
		}

		documentRepository.Add(document);
		unitOfWork.SaveChanges();
		return UseCaseResult<CreateExpenseDocumentResult>.Success(new CreateExpenseDocumentResult(document.Id));
	}

	private static int? ResolveBudgetLineId(CreateExpenseDocumentItemCommand item, IReadOnlyDictionary<string, int> budgetLineMap)
	{
		if (item.BudgetLineId.HasValue)
			return budgetLineMap.Values.Contains(item.BudgetLineId.Value)
				? item.BudgetLineId
				: null;
		if (string.IsNullOrWhiteSpace(item.BudgetLineName)) return null;
		return budgetLineMap.TryGetValue(Normalize(item.BudgetLineName), out var id) ? id : null;
	}

	private int? ResolveBudgetTagId(CreateExpenseDocumentItemCommand item, int budgetLineId)
	{
		var names = item.BudgetTagId.HasValue || string.IsNullOrWhiteSpace(item.BudgetTagName)
			? Array.Empty<string>()
			: new[] { item.BudgetTagName };
		var budgetTagMap = budgetTagEnsurer.EnsureAndGetMap(budgetLineId, names);

		if (item.BudgetTagId.HasValue)
			return budgetTagMap.Values.Contains(item.BudgetTagId.Value)
				? item.BudgetTagId
				: null;

		return !string.IsNullOrWhiteSpace(item.BudgetTagName)
			&& budgetTagMap.TryGetValue(Normalize(item.BudgetTagName), out var id)
				? id
				: null;
	}

	private static string Normalize(string value) => value.Trim().ToUpperInvariant();

	private static CommandValidationResult ValidateCommand(CreateExpenseDocumentCommand command)
	{
		if (string.IsNullOrWhiteSpace(command.SellerName)) return CommandValidationResult.Failure("0", "Seller name is required.");
		if (command.Items.Count == 0) return CommandValidationResult.Failure("0", "Document must contain at least one item.");
		for (var i = 0; i < command.Items.Count; i++)
		{
			var item = command.Items[i];
			if (string.IsNullOrWhiteSpace(item.Name)) return CommandValidationResult.Failure("0", $"Item #{i + 1}: item name is required.");
			if (item.Amount <= 0) return CommandValidationResult.Failure("0", $"Item '{item.Name}': amount must be greater than zero.");
			if (!item.BudgetLineId.HasValue && string.IsNullOrWhiteSpace(item.BudgetLineName))
				return CommandValidationResult.Failure("budget_line_required", $"Позиция «{item.Name}»: укажите строку бюджета.");
		}
		return CommandValidationResult.Success();
	}
}
