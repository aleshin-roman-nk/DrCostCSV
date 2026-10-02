using Application.Reports.GetCategoryExpensesByMonth;
using Microsoft.Extensions.Logging;
using Presentation.Screens.ExpenseDocuments.Edit;
using Presentation.Screens.ExpenseDocuments.List;
using Presentation.Screens.Common;
using Presentation.Screens.Main;
using Presentation.Screens.Main.DatabasePathSettings;
using Presentation.Screens.Currencies.List;
using Presentation.Screens.CurrencyDefaults;
using Presentation.Screens.ExpenseDocuments.WithoutCurrency;
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
	private readonly DatabasePathSettingsFlow databasePathSettingsFlow;
	private readonly CurrencyListFlow currencyListFlow;
	private readonly CurrencyDefaultsFlow currencyDefaultsFlow;
	private readonly WithoutCurrencyFlow withoutCurrencyFlow;

	public IMainView View => view;

	public MainPresenter(IMainView v,
		ExpenseDocumentListFlow expenseDocumentsRunner,
		MainActions mainActionsRunner,

		ExpenseDocumentEditFlow editExpenseDocumentRunner,
		DatabasePathSettingsFlow databasePathSettingsFlow,
		CurrencyListFlow currencyListFlow,
		CurrencyDefaultsFlow currencyDefaultsFlow,
		WithoutCurrencyFlow withoutCurrencyFlow,

		ILogger<MainPresenter> logger

		)
	{
		this.view = v;

		this.expenseDocumentListRunner = expenseDocumentsRunner;
		this.mainActionsRunner = mainActionsRunner;
		this.editExpenseDocumentFlow = editExpenseDocumentRunner;
		this.databasePathSettingsFlow = databasePathSettingsFlow;
		this.currencyListFlow = currencyListFlow;
		this.currencyDefaultsFlow = currencyDefaultsFlow;
		this.withoutCurrencyFlow = withoutCurrencyFlow;
		this.logger = logger;
		this.view.OpenDocumentList += View_OpenDocumentList;
		this.view.DateChanged += View_DateChanged;
		this.view.CreateDocument += View_CreateDocument;
		this.view.DatabasePathSettingsRequested += View_DatabasePathSettingsRequested;
		this.view.CurrenciesRequested += View_CurrenciesRequested;
		this.view.CurrencyDefaultsRequested += View_CurrencyDefaultsRequested;
		this.view.DocumentsWithoutCurrencyRequested += View_DocumentsWithoutCurrencyRequested;

		Init();
	}

	private void View_CreateDocument()
	{
		var res = editExpenseDocumentFlow.RunCreate();

		if (res.IsCancelled) return;

		ReloadDailyExpenses(view.CurrentDate);
	}

	private void View_CurrenciesRequested()
	{
		var result = currencyListFlow.Run();
		if (!result.IsSuccess && !result.IsCancelled)
			view.ShowMsg(result.Error ?? "Не удалось открыть справочник валют.");
	}

	private void View_CurrencyDefaultsRequested()
	{
		var result = currencyDefaultsFlow.Run();
		if (!result.IsSuccess && !result.IsCancelled)
			view.ShowMsg(result.Error ?? "Не удалось открыть настройки валют.");
		else if (result.IsSuccess)
			ReloadDailyExpenses(view.CurrentDate);
	}

	private void View_DocumentsWithoutCurrencyRequested()
	{
		var result = withoutCurrencyFlow.Run();
		if (result.IsSuccess) ReloadDailyExpenses(view.CurrentDate);
		else if (!result.IsCancelled) view.ShowMsg(result.Error ?? "Не удалось открыть документы без валюты.");
	}

	private void View_DateChanged(DateTime obj)
	{
		ReloadDailyExpenses(obj);
	}

	private void Init()
	{
		ReloadDailyExpenses(DateTime.Now);
	}

	private void View_DatabasePathSettingsRequested()
	{
		var result = databasePathSettingsFlow.Run();
		if (!result.IsSuccess && !result.IsCancelled)
			view.ShowMsg(result.Error ?? "Не удалось открыть настройки пути к файлу базы данных.");
		else if (result.IsSuccess)
			view.ShowMsg("Путь к файлу базы данных сохранён. Перезапустите приложение, чтобы применить изменение.");
	}

	private void ReloadDailyExpenses(DateTime dt)
	{
		var res = mainActionsRunner.GetDayExpenses(dt.Year, dt.Month);

		if (res.IsSuccess && res.Data is not null)
			view.SetDailyExpenses(res.Data);
		else
			view.SetDailyExpenses(Enumerable.Range(1, DateTime.DaysInMonth(dt.Year, dt.Month))
				.Select(day => new ViewModels.DailyExpenseRowViewModel
				{
					Date = new DateTime(dt.Year, dt.Month, day),
					TotalSum = null
				}).ToList());

		var report = mainActionsRunner.GetMonthlyReport(dt.Year, dt.Month);

		if (report.IsSuccess && report.Data is not null)
			view.SetMonthlyReport(report.Data);
		else
			view.SetMonthlyReport(report.Error ?? "Не удалось построить отчёт.");
	}

	private void View_OpenDocumentList(DateTime arg1)
	{
		expenseDocumentListRunner.Run(arg1);
		ReloadDailyExpenses(arg1);
	}
}
