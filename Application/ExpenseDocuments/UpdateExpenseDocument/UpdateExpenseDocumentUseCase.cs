using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.ExpenseDocuments.Abstractions.Persistence;

namespace Application.ExpenseDocuments.UpdateExpenseDocument;

public sealed class UpdateExpenseDocumentUseCase
{
	private readonly IExpenseDocumentRepository documentRepository;
	private readonly IBudgetLineEnsurer budgetLineEnsurer;
	private readonly IBudgetTagEnsurer budgetTagEnsurer;
	private readonly IUnitOfWork unitOfWork;

	public UpdateExpenseDocumentUseCase(IExpenseDocumentRepository documentRepository,
		IBudgetLineEnsurer budgetLineEnsurer, IBudgetTagEnsurer budgetTagEnsurer, IUnitOfWork unitOfWork)
	{
		this.documentRepository = documentRepository;
		this.budgetLineEnsurer = budgetLineEnsurer;
		this.budgetTagEnsurer = budgetTagEnsurer;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult<UpdateExpenseDocumentResult> Execute(UpdateExpenseDocumentCommand command)
	{
		var validation = ValidateCommand(command);
		if (!validation.IsValid) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(validation.Error!);
		var document = documentRepository.GetById(command.DocumentId);
		if (document is null) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("document_not_found", "Документ не найден."));

		var existingIds = document.Items.Select(x => x.Id).ToHashSet();
		var submittedIds = command.Items.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToList();
		if (submittedIds.Count != submittedIds.Distinct().Count()) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("duplicate_item", "Документ содержит повторяющиеся позиции."));
		if (submittedIds.Any(id => !existingIds.Contains(id))) return UseCaseResult<UpdateExpenseDocumentResult>.Failure(new UseCaseError("foreign_item", "Обнаружена позиция, не принадлежащая документу."));

		var lineMap = budgetLineEnsurer.EnsureAndGetMap(command.Items.Where(x => x.BudgetLineId is null)
			.Select(x => x.BudgetLineName).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList());
		var prepared = new List<(UpdateExpenseDocumentItemCommand Item, int? BudgetLineId, int? BudgetTagId)>();
		foreach (var item in command.Items)
		{
			var lineId = item.BudgetLineId ?? (!string.IsNullOrWhiteSpace(item.BudgetLineName) && lineMap.TryGetValue(Normalize(item.BudgetLineName), out var ensuredLineId) ? ensuredLineId : null);
			var tagId = item.BudgetTagId;
			if (lineId.HasValue && !tagId.HasValue && !string.IsNullOrWhiteSpace(item.BudgetTagName))
				tagId = budgetTagEnsurer.EnsureAndGetMap(lineId.Value, new[] { item.BudgetTagName }).TryGetValue(Normalize(item.BudgetTagName), out var ensuredTagId) ? ensuredTagId : null;
			if (!lineId.HasValue)
				tagId = null;
			prepared.Add((item, lineId, tagId));
		}

		document.ChangeDate(command.Date);
		document.ChangeSellerName(command.SellerName);
		foreach (var itemId in existingIds.Where(id => !submittedIds.Contains(id)).ToList()) document.RemoveItem(itemId);
		foreach (var item in prepared)
		{
			if (item.Item.Id.HasValue) document.UpdateItem(item.Item.Id.Value, item.Item.Name, item.Item.Price!.Value, item.Item.Amount!.Value, item.BudgetLineId, item.BudgetTagId);
			else document.AddItem(item.Item.Name, item.Item.Price!.Value, item.Item.Amount!.Value, item.BudgetLineId, item.BudgetTagId);
		}
		unitOfWork.SaveChanges();
		return UseCaseResult<UpdateExpenseDocumentResult>.Success(new UpdateExpenseDocumentResult { DocumentId = document.Id });
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
			if (!item.Price.HasValue || item.Price.Value < 0) return CommandValidationResult.Failure("invalid_price", $"Некорректная цена позиции «{item.Name}».");
			if (!item.Amount.HasValue || item.Amount.Value <= 0) return CommandValidationResult.Failure("invalid_amount", $"Некорректное количество позиции «{item.Name}».");
		}
		return CommandValidationResult.Success();
	}
}
