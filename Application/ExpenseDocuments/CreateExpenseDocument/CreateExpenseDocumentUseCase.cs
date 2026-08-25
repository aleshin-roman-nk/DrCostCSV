using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.ExpenseDocuments.Abstractions.Persistence;
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

	public CreateExpenseDocumentUseCase(IExpenseDocumentRepository documentRepository,
		IBudgetLineEnsurer budgetLineEnsurer, IBudgetTagEnsurer budgetTagEnsurer,
		IUnitOfWork unitOfWork, ILogger<CreateExpenseDocumentUseCase> logger)
	{
		this.documentRepository = documentRepository;
		this.budgetLineEnsurer = budgetLineEnsurer;
		this.budgetTagEnsurer = budgetTagEnsurer;
		this.unitOfWork = unitOfWork;
		this.logger = logger;
	}

	public UseCaseResult<CreateExpenseDocumentResult> Execute(CreateExpenseDocumentCommand command)
	{
		var validationResult = ValidateCommand(command);
		if (!validationResult.IsValid)
			return UseCaseResult<CreateExpenseDocumentResult>.Failure(validationResult.Error!);

		var budgetLineMap = budgetLineEnsurer.EnsureAndGetMap(command.Items
			.Where(x => x.BudgetLineId is null).Select(x => x.BudgetLineName)
			.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x!.Trim())
			.Distinct(StringComparer.OrdinalIgnoreCase).ToList());

		var document = new ExpenseDocument(command.Date, command.SellerName);
		foreach (var item in command.Items)
		{
			var budgetLineId = ResolveBudgetLineId(item, budgetLineMap);
			var budgetTagId = budgetLineId.HasValue
				? ResolveBudgetTagId(item, budgetLineId.Value)
				: null;
			document.AddItem(item.Name, item.Price, item.Amount, budgetLineId, budgetTagId);
		}

		documentRepository.Add(document);
		unitOfWork.SaveChanges();
		return UseCaseResult<CreateExpenseDocumentResult>.Success(new CreateExpenseDocumentResult(document.Id));
	}

	private static int? ResolveBudgetLineId(CreateExpenseDocumentItemCommand item, IReadOnlyDictionary<string, int> budgetLineMap)
	{
		if (item.BudgetLineId.HasValue) return item.BudgetLineId;
		if (string.IsNullOrWhiteSpace(item.BudgetLineName)) return null;
		return budgetLineMap.TryGetValue(Normalize(item.BudgetLineName), out var id) ? id : null;
	}

	private int? ResolveBudgetTagId(CreateExpenseDocumentItemCommand item, int budgetLineId)
	{
		if (item.BudgetTagId.HasValue) return item.BudgetTagId;
		if (string.IsNullOrWhiteSpace(item.BudgetTagName)) return null;
		return budgetTagEnsurer.EnsureAndGetMap(budgetLineId, new[] { item.BudgetTagName })
			.TryGetValue(Normalize(item.BudgetTagName), out var id) ? id : null;
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
			if (item.Price < 0) return CommandValidationResult.Failure("0", $"Item '{item.Name}': price cannot be negative.");
			if (item.Amount <= 0) return CommandValidationResult.Failure("0", $"Item '{item.Name}': amount must be greater than zero.");
		}
		return CommandValidationResult.Success();
	}
}
