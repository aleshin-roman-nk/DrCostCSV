using Domain;

namespace Application.BudgetTags.Abstractions;

public interface IBudgetTagRepository
{
	IReadOnlyList<BudgetTag> GetAll();
	void Add(BudgetTag budgetTag);
	bool IsUsedByDocumentItem(int budgetTagId);
	void Remove(BudgetTag budgetTag);
}
