using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport;

public sealed class ExpenseDocumentJsonImportFlow : IExpenseDocumentJsonImportFlow
{
	private readonly IScreenScopedExecutor screenScopedExecutor;

	public ExpenseDocumentJsonImportFlow(
		IScreenScopedExecutor screenScopedExecutor)
	{
		this.screenScopedExecutor = screenScopedExecutor;
	}

	public ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>> GetList()
	{
		return screenScopedExecutor.Execute<
			ExpenseDocumentJsonImportPresenter,
			ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>>>(
			presenter => presenter.GetList());
	}
}
