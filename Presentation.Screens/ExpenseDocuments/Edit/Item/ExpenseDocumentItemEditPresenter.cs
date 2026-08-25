using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item;

public sealed class ExpenseDocumentItemEditPresenter
{
	private readonly IExpenseDocumentItemEditView view;
	public ExpenseDocumentItemEditPresenter(IExpenseDocumentItemEditView view) => this.view = view;
	public ScreenResult<ExpenseDocumentItemViewModel> EditItem(ExpenseDocumentItemViewModel item, IReadOnlyList<BudgetLineOptionViewModel> budgetLines, IReadOnlyList<BudgetTagOptionViewModel> budgetTags)
	{
		view.SetBudgetLines(budgetLines); view.SetBudgetTags(budgetTags); view.SetItem(item);
		return view.ShowModal() == ModalResult.Ok ? ScreenResult<ExpenseDocumentItemViewModel>.Success(view.GetItem()) : ScreenResult<ExpenseDocumentItemViewModel>.Cancelled();
	}
}
