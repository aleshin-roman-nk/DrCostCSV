namespace Application.Reports.GetBudgetLineExpensesByMonth;

public sealed class GetBudgetLineExpensesByMonthQuery
{
	public GetBudgetLineExpensesByMonthQuery(int year, int month)
	{
		Year = year;
		Month = month;
	}

	public int Year { get; }
	public int Month { get; }
}

public sealed class BudgetLineExpenseDto
{
	public required string BudgetLineName { get; init; }
	public decimal TotalSum { get; init; }
	public required IReadOnlyList<BudgetTagExpenseDto> Tags { get; init; }
}

public sealed class BudgetTagExpenseDto
{
	public required string BudgetTagName { get; init; }
	public decimal TotalSum { get; init; }
}
