using Application.Common;
using Application.Reports.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetDailyExpensesByMonth;

public class GetDailyExpensesByMonthUseCase
{
	private readonly IExpenseReportsReader expenseReportsReader;

	public GetDailyExpensesByMonthUseCase(IExpenseReportsReader expenseReportsReader)
	{
		this.expenseReportsReader = expenseReportsReader;
	}

	public UseCaseResult<GetDailyExpensesByMonthResult> Execute(
		GetDailyExpensesByMonthQuery p)
	{
		var data = expenseReportsReader.GetDailyExpensesByMonth(p.Year, p.Month);

		var datares = new GetDailyExpensesByMonthResult(p.Year, p.Month, data);

		var res = UseCaseResult<GetDailyExpensesByMonthResult>.Success(datares);

		return res;
	}
}
