using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport
{
	public interface IExpenseDocumentJsonImportFlow
	{
		ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>> GetList();
	}
}
