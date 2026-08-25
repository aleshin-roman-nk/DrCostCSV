using Domain;

namespace Application.BudgetLines.Abstractions;

public interface IBudgetLineRepository
{
	IReadOnlyList<BudgetLine> GetAll();
	void Add(BudgetLine budgetLine);
	bool IsUsedByDocumentItem(int budgetLineId);
	void Remove(BudgetLine budgetLine);
}
