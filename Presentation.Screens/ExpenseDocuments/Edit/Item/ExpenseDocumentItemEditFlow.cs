using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item
{
	public class ExpenseDocumentItemEditFlow
	{
		private readonly IScreenScopedExecutor screenScopedExecutor;

		public ExpenseDocumentItemEditFlow(
			IScreenScopedExecutor screenScopedExecutor)
		{
			this.screenScopedExecutor = screenScopedExecutor;
		}

		public ScreenResult<ExpenseDocumentItemViewModel> EditItem(
			ExpenseDocumentItemViewModel item,
			IReadOnlyList<BudgetLineOptionViewModel> budgetLines,
			IReadOnlyList<BudgetTagOptionViewModel> budgetTags)
		{
			ArgumentNullException.ThrowIfNull(item);

			return screenScopedExecutor.Execute<
				ExpenseDocumentItemEditPresenter,
				ScreenResult<ExpenseDocumentItemViewModel>>(
				presenter => presenter.EditItem(item, budgetLines, budgetTags));
		}
	}
}
