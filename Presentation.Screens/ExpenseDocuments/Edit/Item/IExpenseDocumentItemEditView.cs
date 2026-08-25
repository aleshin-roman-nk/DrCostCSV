using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item;

public interface IExpenseDocumentItemEditView
{
	void SetItem(ExpenseDocumentItemViewModel itemViewModel);
	ExpenseDocumentItemViewModel GetItem();
	void SetBudgetLines(IReadOnlyList<BudgetLineOptionViewModel> budgetLines);
	void SetBudgetTags(IReadOnlyList<BudgetTagOptionViewModel> budgetTags);
	ModalResult ShowModal();
}
