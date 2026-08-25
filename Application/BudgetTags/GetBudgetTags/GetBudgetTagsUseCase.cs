using Application.BudgetTags.Abstractions;
using Application.Common;

namespace Application.BudgetTags.GetBudgetTags;

public sealed class GetBudgetTagsUseCase
{
	private readonly IBudgetTagReader budgetTagReader;
	public GetBudgetTagsUseCase(IBudgetTagReader budgetTagReader) => this.budgetTagReader = budgetTagReader;
	public UseCaseResult<IReadOnlyList<BudgetTagDto>> Execute() =>
		UseCaseResult<IReadOnlyList<BudgetTagDto>>.Success(budgetTagReader.GetAll());
}
