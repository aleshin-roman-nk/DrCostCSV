namespace Application.BudgetLines.Abstractions;

public interface IBudgetLineEnsurer
{
	IReadOnlyDictionary<string, int> EnsureAndGetMap(IReadOnlyList<string> budgetLineNames);
}
