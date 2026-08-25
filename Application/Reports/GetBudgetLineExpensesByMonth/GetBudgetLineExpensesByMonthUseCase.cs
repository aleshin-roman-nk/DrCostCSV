using Application.Common;
using Application.Reports.Abstractions;

namespace Application.Reports.GetBudgetLineExpensesByMonth;

public sealed class GetBudgetLineExpensesByMonthUseCase
{
	private readonly IExpenseReportsReader expenseReportsReader;
	public GetBudgetLineExpensesByMonthUseCase(IExpenseReportsReader expenseReportsReader) => this.expenseReportsReader = expenseReportsReader;

	public UseCaseResult<IReadOnlyList<BudgetLineExpenseDto>> Execute(GetBudgetLineExpensesByMonthQuery query)
	{
		if (query.Month is < 1 or > 12)
			return UseCaseResult<IReadOnlyList<BudgetLineExpenseDto>>.Failure(new UseCaseError("invalid_month", "Некорректный месяц отчёта."));

		return UseCaseResult<IReadOnlyList<BudgetLineExpenseDto>>.Success(
			expenseReportsReader.GetBudgetLineExpensesByMonth(query.Year, query.Month));
	}
}
