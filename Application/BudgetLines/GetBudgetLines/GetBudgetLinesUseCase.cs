using Application.BudgetLines.Abstractions;
using Application.Common;

namespace Application.BudgetLines.GetBudgetLines;

public sealed class GetBudgetLinesUseCase
{
	private readonly IBudgetLineReader budgetLineReader;

	public GetBudgetLinesUseCase(IBudgetLineReader budgetLineReader)
	{
		this.budgetLineReader = budgetLineReader;
	}

	public UseCaseResult<IReadOnlyList<BudgetLineDto>> Execute()
	{
		return UseCaseResult<IReadOnlyList<BudgetLineDto>>.Success(budgetLineReader.GetAll());
	}
}
