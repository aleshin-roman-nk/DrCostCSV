using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

public interface IExpenseDocumentCurrencyEditView
{
	event Action? SaveRequested;
	event Action? AddRequested;
	event Action? RemoveRequested;
	int SelectedRowIndex { get; }
	ModalResult ShowModal();
	void SetModel(ExpenseDocumentCurrencyEditViewModel model, IReadOnlyList<CurrencyOptionViewModel> currencies);
	bool ApplyInput();
	void SelectRow(int index);
	void ShowError(string message);
	void CloseSuccessfully();
}
