using Presentation.Screens.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Main;

public interface IMainView
{
	event Action<DateTime> OpenDocumentList;

	event Action<DateTime> DateChanged;

	event Action CreateDocument;

	event Action DatabasePathSettingsRequested;

	DateTime CurrentDate {  get; }

	void SetDailyExpenses(IReadOnlyList<DailyExpenseRowViewModel> list);

	void SetMonthlyReport(string report);

	void ShowMsg(string msg);

}
