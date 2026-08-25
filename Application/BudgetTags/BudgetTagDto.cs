namespace Application.BudgetTags;

public sealed class BudgetTagDto
{
	public int Id { get; init; }
	public int BudgetLineId { get; init; }
	public required string Name { get; init; }
}
