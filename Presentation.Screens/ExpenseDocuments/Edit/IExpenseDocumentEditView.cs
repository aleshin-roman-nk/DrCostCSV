using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public interface IExpenseDocumentEditView
{
	event Action? JsonItemsRequested;
	event Action? SaveRequested;
	public event Action<ExpenseDocumentItemViewModel>? EditDocumentItemRequested;
	public event Action<ExpenseDocumentItemViewModel>? DeleteDocumentItemRequested;
	public event Action? NewDocumentItemRequested;
	event Action? EditCurrencyRequested;


	ModalResult ShowModal();
	void CloseWithOk();
	void RefreshItems();
	void SetDocument(ExpenseDocumentViewModel document);
	void SetCurrencySummary(string summary);
	void SetItemsList(IReadOnlyList<ExpenseDocumentItemViewModel> rows);
	void SetDocumentDate(DateTime date);
	bool ApplyInputToDocument();

	void ShowError(string message);
}
