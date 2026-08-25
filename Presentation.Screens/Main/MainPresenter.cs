using Application.Reports.GetCategoryExpensesByMonth;
using Microsoft.Extensions.Logging;
using Presentation.Screens.ExpenseDocuments.Edit;
using Presentation.Screens.ExpenseDocuments.List;
using Presentation.Screens.Common;
using Presentation.Screens.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Main;

public class MainPresenter
{
	private readonly IMainView view;
	private readonly ExpenseDocumentListFlow expenseDocumentListRunner;
	private readonly MainActions mainActionsRunner;
	private readonly ExpenseDocumentEditFlow editExpenseDocumentFlow;
	private readonly ILogger<MainPresenter> logger;

	public IMainView View => view;

	public MainPresenter(IMainView v,
		ExpenseDocumentListFlow expenseDocumentsRunner,
		MainActions mainActionsRunner,

		ExpenseDocumentEditFlow editExpenseDocumentRunner,

		ILogger<MainPresenter> logger

		)
	{
		this.view = v;

		this.expenseDocumentListRunner = expenseDocumentsRunner;
		this.mainActionsRunner = mainActionsRunner;
		this.editExpenseDocumentFlow = editExpenseDocumentRunner;
		this.logger = logger;
		this.view.OpenDocumentList += View_OpenDocumentList;
		this.view.DateChanged += View_DateChanged;
		this.view.CreateDocument += View_CreateDocument;

		Init();
	}

	private void View_CreateDocument()
	{
		var res = editExpenseDocumentFlow.RunCreate();

		if (res.IsCancelled) return;

		ReloadDailyExpenses(view.CurrentDate);
	}

	private void View_DateChanged(DateTime obj)
	{
		ReloadDailyExpenses(obj);
	}

	private void Init()
	{
		ReloadDailyExpenses(DateTime.Now);
	}

	private void ReloadDailyExpenses(DateTime dt)
	{
		var res = mainActionsRunner.GetDayExpenses(dt.Year, dt.Month);

		if (res.IsSuccess && res.Data is not null)
			view.SetDailyExpenses(res.Data);

		var report = mainActionsRunner.GetMonthlyReport(dt.Year, dt.Month);

		if (report.IsSuccess && report.Data is not null)
			view.SetMonthlyReport(report.Data);
	}

	private void View_OpenDocumentList(DateTime arg1)
	{
		expenseDocumentListRunner.Run(arg1);
		ReloadDailyExpenses(arg1);
	}
}
