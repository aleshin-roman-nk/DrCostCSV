namespace Application.BudgetLines.Abstractions;

public interface IBudgetLineReader
{
	IReadOnlyList<BudgetLineDto> GetAll();
}
