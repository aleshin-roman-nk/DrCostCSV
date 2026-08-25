using Microsoft.Extensions.Logging;
using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.Item;
using Presentation.Screens.ExpenseDocuments.Edit.JsonImport;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System.ComponentModel;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public sealed class ExpenseDocumentEditPresenter
{
	private readonly IExpenseDocumentEditView view;
	private readonly IExpenseDocumentJsonImportFlow jsonImportFlow;
	private readonly ExpenseDocumentItemEditFlow itemEditFlow;
	private readonly ExpenseDocumentEditActions actions;
	private readonly IViewModelVerifier<ExpenseDocumentViewModel> verifier;
	private readonly IValidationErrorDialog validationErrorDialog;
	private readonly List<BudgetLineOptionViewModel> budgetLines = new();
	private readonly List<BudgetTagOptionViewModel> budgetTags = new();
	private ExpenseDocumentViewModel? currentDocument;
	private ScreenResult saveResult = ScreenResult.Cancelled();

	public ExpenseDocumentEditPresenter(IExpenseDocumentEditView view, IExpenseDocumentJsonImportFlow jsonImportFlow,
		ExpenseDocumentItemEditFlow itemEditFlow, ExpenseDocumentEditActions actions,
		IViewModelVerifier<ExpenseDocumentViewModel> verifier, IValidationErrorDialog validationErrorDialog,
		ILogger<ExpenseDocumentEditPresenter> logger)
	{
		this.view = view; this.jsonImportFlow = jsonImportFlow; this.itemEditFlow = itemEditFlow; this.actions = actions; this.verifier = verifier; this.validationErrorDialog = validationErrorDialog;
		view.JsonItemsRequested += View_JsonItemsRequested; view.SaveRequested += View_SaveRequested; view.EditDocumentItemRequested += View_EditDocumentItemRequested; view.DeleteDocumentItemRequested += View_DeleteDocumentItemRequested; view.NewDocumentItemRequested += View_NewDocumentItemRequested;
	}
	public ScreenResult CreateDocument(DateTime date)
	{
		var load = LoadLookups(); if (!load.IsSuccess) return load;
		currentDocument = new ExpenseDocumentViewModel { Date = date.Date, Seller = string.Empty, Items = new BindingList<ExpenseDocumentItemViewModel>() };
		view.SetDocument(currentDocument); return view.ShowModal() == ModalResult.Ok ? saveResult : ScreenResult.Cancelled();
	}
	public ScreenResult OpenDocument(int id)
	{
		if (id <= 0) return ScreenResult.Failure("Некорректный идентификатор документа.");
		var doc = actions.GetDocument(id); if (!doc.IsSuccess || doc.Data is null) return ScreenResult.Failure(doc.Error ?? "Документ не найден.");
		var load = LoadLookups(); if (!load.IsSuccess) return load;
		currentDocument = doc.Data; saveResult = ScreenResult.Cancelled(); view.SetDocument(currentDocument);
		return view.ShowModal() == ModalResult.Ok ? saveResult : ScreenResult.Cancelled();
	}
	private void View_NewDocumentItemRequested()
	{
		var item = new ExpenseDocumentItemViewModel { Amount = 1 };
		var result = itemEditFlow.EditItem(item, budgetLines, budgetTags); if (!result.IsSuccess || result.Data is null) return;
		RequireCurrentDocument().Items.Add(result.Data); view.RefreshItems();
	}
	private void View_EditDocumentItemRequested(ExpenseDocumentItemViewModel item)
	{
		var copy = new ExpenseDocumentItemViewModel { Id = item.Id, Name = item.Name, Price = item.Price, Amount = item.Amount, BudgetLineId = item.BudgetLineId, BudgetLineName = item.BudgetLineName, BudgetTagId = item.BudgetTagId, BudgetTagName = item.BudgetTagName };
		var result = itemEditFlow.EditItem(copy, budgetLines, budgetTags); if (!result.IsSuccess || result.Data is null) return;
		item.Name = result.Data.Name; item.Price = result.Data.Price; item.Amount = result.Data.Amount; item.BudgetLineId = result.Data.BudgetLineId; item.BudgetLineName = result.Data.BudgetLineName; item.BudgetTagId = result.Data.BudgetTagId; item.BudgetTagName = result.Data.BudgetTagName; view.RefreshItems();
	}
	private void View_DeleteDocumentItemRequested(ExpenseDocumentItemViewModel item) { view.ApplyInputToDocument(); RequireCurrentDocument().Items.Remove(item); view.RefreshItems(); }
	private void View_SaveRequested()
	{
		view.ApplyInputToDocument(); var document = RequireCurrentDocument(); var validation = verifier.Verify(document);
		if (!validation.IsValid) { validationErrorDialog.Show(validation.Errors); return; }
		var result = document.Id.HasValue ? actions.UpdateDocument(document) : actions.CreateDocument(document);
		if (!result.IsSuccess) { validationErrorDialog.Show(new[] { result.Error ?? "Не удалось сохранить документ." }); return; }
		saveResult = ScreenResult.Success(); view.CloseWithOk();
	}
	private void View_JsonItemsRequested()
	{
		var result = jsonImportFlow.GetList();

		if (result.IsCancelled)
			return;

		if (!result.IsSuccess || result.Data is null)
		{
			view.ShowError(result.Error ?? "Не удалось распознать JSON-документ.");
			return;
		}

		var lines = budgetLines.ToDictionary(x => Normalize(x.Name));
		view.SetItemsList(result.Data.Select(x =>
		{
			var budgetLineName = x.BudgetLine.Trim();
			lines.TryGetValue(Normalize(budgetLineName), out var line);
			var budgetTagName = x.BudgetTag?.Trim() ?? string.Empty;
			var tag = line is null || string.IsNullOrEmpty(budgetTagName)
				? null
				: budgetTags.FirstOrDefault(t =>
					t.BudgetLineId == line.Id &&
					Normalize(t.Name) == Normalize(budgetTagName));

			return new ExpenseDocumentItemViewModel
			{
				Name = x.Name.Trim(),
				Price = x.Price,
				Amount = x.Amount,
				BudgetLineId = line?.Id,
				BudgetLineName = line?.Name ?? budgetLineName,
				BudgetTagId = tag?.Id,
				BudgetTagName = tag?.Name ?? budgetTagName
			};
		}).ToList());
	}
	private ScreenResult LoadLookups()
	{
		var lines = actions.GetBudgetLines(); if (!lines.IsSuccess) return ScreenResult.Failure(lines.Error ?? "Не удалось загрузить строки бюджета.");
		var tags = actions.GetBudgetTags(); if (!tags.IsSuccess) return ScreenResult.Failure(tags.Error ?? "Не удалось загрузить теги.");
		budgetLines.Clear(); budgetLines.AddRange(lines.Data ?? []); budgetTags.Clear(); budgetTags.AddRange(tags.Data ?? []); return ScreenResult.Success();
	}
	private ExpenseDocumentViewModel RequireCurrentDocument() => currentDocument ?? throw new InvalidOperationException("Current document was not initialized.");
	private static string Normalize(string value) => value.Trim().ToUpperInvariant();
}
