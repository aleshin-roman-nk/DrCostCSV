namespace Application.BudgetTags.Abstractions;

public interface IBudgetTagReader
{
	IReadOnlyList<BudgetTagDto> GetAll();
}
