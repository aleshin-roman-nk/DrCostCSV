namespace Application.BudgetTags.Abstractions;

public interface IBudgetTagEnsurer
{
	IReadOnlyDictionary<string, int> EnsureAndGetMap(int budgetLineId, IReadOnlyList<string> tagNames);
}
