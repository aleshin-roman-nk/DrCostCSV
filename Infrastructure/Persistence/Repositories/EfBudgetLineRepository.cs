using Application.BudgetLines.Abstractions;
using Domain;

namespace Infrastructure.Persistence.Repositories;

public sealed class EfBudgetLineRepository : IBudgetLineRepository
{
	private readonly AppDbContext dbContext;
	public EfBudgetLineRepository(AppDbContext dbContext) => this.dbContext = dbContext;
	public IReadOnlyList<BudgetLine> GetAll() => dbContext.BudgetLines.ToList();
	public void Add(BudgetLine budgetLine) => dbContext.BudgetLines.Add(budgetLine);
	public bool IsUsedByDocumentItem(int budgetLineId) => dbContext.ExpenseDocumentItems.Any(x =>
		x.BudgetLineId == budgetLineId || (x.BudgetTag != null && x.BudgetTag.BudgetLineId == budgetLineId));
	public void Remove(BudgetLine budgetLine) => dbContext.BudgetLines.Remove(budgetLine);
}
