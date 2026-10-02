using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item;

public sealed class ExpenseDocumentItemEditPresenter
{
	private readonly IExpenseDocumentItemEditView view;
	private readonly IViewModelVerifier<ExpenseDocumentItemViewModel> verifier;
	private readonly IValidationErrorDialog validationErrorDialog;
	private ScreenResult<ExpenseDocumentItemViewModel> saveResult = ScreenResult<ExpenseDocumentItemViewModel>.Cancelled();

	public ExpenseDocumentItemEditPresenter(
		IExpenseDocumentItemEditView view,
		IViewModelVerifier<ExpenseDocumentItemViewModel> verifier,
		IValidationErrorDialog validationErrorDialog)
	{
		this.view = view;
		this.verifier = verifier;
		this.validationErrorDialog = validationErrorDialog;
		view.SaveRequested += View_SaveRequested;
	}

	public ScreenResult<ExpenseDocumentItemViewModel> EditItem(ExpenseDocumentItemViewModel item, IReadOnlyList<BudgetLineOptionViewModel> budgetLines, IReadOnlyList<BudgetTagOptionViewModel> budgetTags)
	{
		saveResult = ScreenResult<ExpenseDocumentItemViewModel>.Cancelled();
		view.SetBudgetLines(budgetLines); view.SetBudgetTags(budgetTags); view.SetItem(item);
		return view.ShowModal() == ModalResult.Ok
			? saveResult
			: ScreenResult<ExpenseDocumentItemViewModel>.Cancelled();
	}

	private void View_SaveRequested()
	{
		var item = view.GetItem();
		var validation = verifier.Verify(item);
		if (!validation.IsValid)
		{
			validationErrorDialog.Show(validation.Errors);
			return;
		}

		saveResult = ScreenResult<ExpenseDocumentItemViewModel>.Success(item);
		view.CloseWithOk();
	}
}
