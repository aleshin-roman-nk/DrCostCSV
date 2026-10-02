using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.ExpenseDocuments.Abstractions.Persistence;
using Application.Currencies.Abstractions;
using Domain;

namespace Application.ExpenseDocuments.UpdateExpenseDocument;

public sealed class UpdateExpenseDocumentUseCase
{
	private readonly IExpenseDocumentRepository documentRepository;
	private readonly IBudgetLineEnsurer budgetLineEnsurer;
	private readonly IBudgetTagEnsurer budgetTagEnsurer;
	private readonly IUnitOfWork unitOfWork;
	private readonly ICurrencyReader currencies;

	public UpdateExpenseDocumentUseCase(IExpenseDocumentRepository documentRepository,
		IBudgetLineEnsurer budgetLineEnsurer, IBudgetTagEnsurer budgetTagEnsurer, IUnitOfWork unitOfWork,
		ICurrencyReader currencies)
	{
		this.documentRepository = documentRepository;
		this.budgetLineEnsurer = budgetLineEnsurer;
		this.budgetTagEnsurer = budgetTagEnsurer;
		this.unitOfWork = unitOfWork;
		this.currencies = currencies;
	}

	public UseCaseResult<UpdateExpenseDocumentResult> Execute(UpdateExpenseDocumentCommand command)
	{
		var validation = ValidateCommand(command);
		if (!validation.IsValid) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(validation.Error!);
		if (command.CurrencyValues is not null)
		{
			var currencyError = CurrencySelectionValidator.Validate(command.CurrencyId, command.CurrencyValues, currencies);
			if (currencyError is not null)
				return UseCaseResult<UpdateExpenseDocumentResult>.Failure(currencyError);
		}
		var document = documentRepository.GetById(command.DocumentId);
		if (document is null) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("document_not_found", "Документ не найден."));

		var existingIds = document.Items.Select(x => x.Id).ToHashSet();
		var submittedIds = command.Items.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToList();
		if (submittedIds.Count != submittedIds.Distinct().Count()) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("duplicate_item", "Документ содержит повторяющиеся позиции."));
		if (submittedIds.Any(id => !existingIds.Contains(id))) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("foreign_item", "Обнаружена позиция, не принадлежащая документу."));

		var lineMap = budgetLineEnsurer.EnsureAndGetMap(command.Items.Where(x => x.BudgetLineId is null)
			.Select(x => x.BudgetLineName).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList());
		var prepared = new List<(UpdateExpenseDocumentItemCommand Item, int BudgetLineId, int? BudgetTagId)>();
		foreach (var item in command.Items)
		{
			var lineId = ResolveBudgetLineId(item, lineMap);
			if (!lineId.HasValue)
				return UseCaseResult<UpdateExpenseDocumentResult>.Failure(
					new UseCaseError("budget_line_not_found", $"Строка бюджета позиции «{item.Name}» не найдена."));

			var tagId = ResolveBudgetTagId(item, lineId.Value);
			var hasBudgetTag = item.BudgetTagId.HasValue || !string.IsNullOrWhiteSpace(item.BudgetTagName);
			if (hasBudgetTag && !tagId.HasValue)
				return UseCaseResult<UpdateExpenseDocumentResult>.Failure(
					new UseCaseError("budget_tag_not_found", $"Уточняющий тег позиции «{item.Name}» не найден в выбранной строке бюджета."));

			prepared.Add((item, lineId.Value, tagId));
		}

		document.ChangeDate(command.Date);
		document.ChangeSellerName(command.SellerName);
		if (command.CurrencyValues is not null)
			document.SetCurrencyValues(command.CurrencyId,
				command.CurrencyValues.Select(value => new CurrencyValue(value.CurrencyId, value.Value)));
		foreach (var itemId in existingIds.Where(id => !submittedIds.Contains(id)).ToList()) document.RemoveItem(itemId);
		foreach (var item in prepared)
		{
			if (item.Item.Id.HasValue) document.UpdateItem(item.Item.Id.Value, item.Item.Name, item.Item.Price!.Value, item.Item.Amount!.Value, item.BudgetLineId, item.BudgetTagId);
			else document.AddItem(item.Item.Name, item.Item.Price!.Value, item.Item.Amount!.Value, item.BudgetLineId, item.BudgetTagId);
		}
		unitOfWork.SaveChanges();
		return UseCaseResult<UpdateExpenseDocumentResult>.Success(new UpdateExpenseDocumentResult { DocumentId = document.Id });
	}

	private static int? ResolveBudgetLineId(UpdateExpenseDocumentItemCommand item, IReadOnlyDictionary<string, int> lineMap)
	{
		if (item.BudgetLineId.HasValue)
			return lineMap.Values.Contains(item.BudgetLineId.Value)
				? item.BudgetLineId
				: null;

		return lineMap.TryGetValue(Normalize(item.BudgetLineName), out var lineId)
			? lineId
			: null;
	}

	private int? ResolveBudgetTagId(UpdateExpenseDocumentItemCommand item, int budgetLineId)
	{
		var names = item.BudgetTagId.HasValue || string.IsNullOrWhiteSpace(item.BudgetTagName)
			? Array.Empty<string>()
			: new[] { item.BudgetTagName };
		var tagMap = budgetTagEnsurer.EnsureAndGetMap(budgetLineId, names);

		if (item.BudgetTagId.HasValue)
			return tagMap.Values.Contains(item.BudgetTagId.Value)
				? item.BudgetTagId
				: null;

		return !string.IsNullOrWhiteSpace(item.BudgetTagName)
			&& tagMap.TryGetValue(Normalize(item.BudgetTagName), out var tagId)
			? tagId
			: null;
	}

	private static string Normalize(string value) => value.Trim().ToUpperInvariant();
	private static CommandValidationResult ValidateCommand(UpdateExpenseDocumentCommand command)
	{
		if (command.DocumentId <= 0) return CommandValidationResult.Failure("invalid_id", "Некорректный идентификатор документа.");
		if (string.IsNullOrWhiteSpace(command.SellerName)) return CommandValidationResult.Failure("seller_required", "Укажите продавца.");
		if (command.Items.Count == 0) return CommandValidationResult.Failure("items_required", "Документ должен содержать хотя бы одну позицию.");
		foreach (var item in command.Items)
		{
			if (string.IsNullOrWhiteSpace(item.Name)) return CommandValidationResult.Failure("item_name_required", $"Укажите наименование позиции «{item.Name}».");
			if (!item.Price.HasValue) return CommandValidationResult.Failure("invalid_price", $"Некорректная цена позиции «{item.Name}».");
			if (!item.Amount.HasValue || item.Amount.Value <= 0) return CommandValidationResult.Failure("invalid_amount", $"Некорректное количество позиции «{item.Name}».");
			if (!item.BudgetLineId.HasValue && string.IsNullOrWhiteSpace(item.BudgetLineName))
				return CommandValidationResult.Failure("budget_line_required", $"Позиция «{item.Name}»: укажите строку бюджета.");
		}
		return CommandValidationResult.Success();
	}
}
