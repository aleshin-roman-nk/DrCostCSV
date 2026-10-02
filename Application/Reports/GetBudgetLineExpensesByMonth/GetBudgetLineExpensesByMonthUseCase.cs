using Application.Common;
using Application.Reports.Abstractions;
using Application.CurrencyDefaults.Abstractions;
using Application.Currencies.Abstractions;

namespace Application.Reports.GetBudgetLineExpensesByMonth;

public sealed class GetBudgetLineExpensesByMonthUseCase
{
	private readonly IExpenseReportsReader expenseReportsReader;
	private readonly ICurrencyDefaultSettingsRepository settingsRepository;
	private readonly ICurrencyReader currencies;
	public GetBudgetLineExpensesByMonthUseCase(
		IExpenseReportsReader expenseReportsReader, ICurrencyDefaultSettingsRepository settingsRepository,
		ICurrencyReader currencies)
	{
		this.expenseReportsReader = expenseReportsReader;
		this.settingsRepository = settingsRepository;
		this.currencies = currencies;
	}

	public UseCaseResult<BudgetLineExpensesByMonthResult> Execute(GetBudgetLineExpensesByMonthQuery query)
	{
		if (query.Month is < 1 or > 12 || query.Year is < 1 or >= 9999)
			return UseCaseResult<BudgetLineExpensesByMonthResult>.Failure(
				new UseCaseError("invalid_month", "Некорректный месяц отчёта."));

		var reportCurrencyId = settingsRepository.Get()?.ReportCurrencyId;
		if (!reportCurrencyId.HasValue)
			return UseCaseResult<BudgetLineExpensesByMonthResult>.Failure(
				new UseCaseError("report_currency_required", "Выберите валюту отчёта в настройках валют."));
		var reportCurrency = currencies.GetById(reportCurrencyId.Value);
		if (reportCurrency is null)
			return UseCaseResult<BudgetLineExpensesByMonthResult>.Failure(
				new UseCaseError("report_currency_not_found", "Валюта отчёта больше не найдена."));

		var data = expenseReportsReader.GetBudgetLineExpensesByMonth(
			query.Year, query.Month, reportCurrencyId.Value);
		return UseCaseResult<BudgetLineExpensesByMonthResult>.Success(
			new(reportCurrency.Code, data.Rows, data.ExcludedDocumentCount));
	}
}
