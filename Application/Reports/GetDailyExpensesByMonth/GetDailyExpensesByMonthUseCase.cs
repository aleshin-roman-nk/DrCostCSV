using Application.Common;
using Application.Reports.Abstractions;
using Application.CurrencyDefaults.Abstractions;
using Application.Currencies.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetDailyExpensesByMonth;

public class GetDailyExpensesByMonthUseCase
{
	private readonly IExpenseReportsReader expenseReportsReader;
	private readonly ICurrencyDefaultSettingsRepository settingsRepository;
	private readonly ICurrencyReader currencies;

	public GetDailyExpensesByMonthUseCase(
		IExpenseReportsReader expenseReportsReader, ICurrencyDefaultSettingsRepository settingsRepository,
		ICurrencyReader currencies)
	{
		this.expenseReportsReader = expenseReportsReader;
		this.settingsRepository = settingsRepository;
		this.currencies = currencies;
	}

	public UseCaseResult<GetDailyExpensesByMonthResult> Execute(
		GetDailyExpensesByMonthQuery p)
	{
		if (p.Month is < 1 or > 12 || p.Year is < 1 or >= 9999)
			return UseCaseResult<GetDailyExpensesByMonthResult>.Failure(
				new UseCaseError("invalid_period", "Некорректный месяц отчёта."));
		var reportCurrencyId = settingsRepository.Get()?.ReportCurrencyId;
		if (!reportCurrencyId.HasValue)
			return UseCaseResult<GetDailyExpensesByMonthResult>.Failure(
				new UseCaseError("report_currency_required", "Выберите валюту отчёта в настройках валют."));
		var reportCurrency = currencies.GetById(reportCurrencyId.Value);
		if (reportCurrency is null)
			return UseCaseResult<GetDailyExpensesByMonthResult>.Failure(
				new UseCaseError("report_currency_not_found", "Валюта отчёта больше не найдена."));
		var data = expenseReportsReader.GetDailyExpensesByMonth(p.Year, p.Month, reportCurrencyId.Value);

		var datares = new GetDailyExpensesByMonthResult(
			p.Year, p.Month, data.Rows, data.ExcludedDocumentCount, reportCurrency.Code);

		var res = UseCaseResult<GetDailyExpensesByMonthResult>.Success(datares);

		return res;
	}
}
