using Presentation.Screens.ExpenseDocuments.List.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.List;

public interface IExpenseDocumentListView
{
	event Action CreateExpenseDocument;
	event Action<int> OpenExpenseDocument;

	void ShowModal();

	void SetDocumentTitles(IReadOnlyList<ExpenseDocumentTitleViewModel> rows);
	void SetDate(DateTime dt);

	void ShowError(string msg);
}
