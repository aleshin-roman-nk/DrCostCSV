using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetDailyExpensesByMonth;

public class GetDailyExpensesByMonthQuery
{
	public int Year { get; init; }
	public int Month { get; init; }

	public GetDailyExpensesByMonthQuery(int year, int month)
	{
		Year = year;
		Month = month;
	}
}

public class GetDailyExpensesByMonthResult
{
	public int Year { get; init; }
	public int Month { get; init; }
	public IReadOnlyList<DailyExpenseDto> Days { get; init; } = [];
	public int ExcludedDocumentCount { get; init; }
	public string CurrencyCode { get; init; }
	public GetDailyExpensesByMonthResult(
		int year, int month, IReadOnlyList<DailyExpenseDto> data,
		int excludedDocumentCount, string currencyCode)
	{
		Year = year;
		Month = month;
		Days = data;
		ExcludedDocumentCount = excludedDocumentCount;
		CurrencyCode = currencyCode;
	}
}

public class DailyExpenseDto
{
	public DateTime Date { get; set; }
	public decimal TotalSum { get; set; }
	public int ExcludedDocumentCount { get; set; }
}
