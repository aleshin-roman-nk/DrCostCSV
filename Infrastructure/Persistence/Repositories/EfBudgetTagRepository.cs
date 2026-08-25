using Application.BudgetTags.Abstractions;
using Domain;

namespace Infrastructure.Persistence.Repositories;

public sealed class EfBudgetTagRepository : IBudgetTagRepository
{
	private readonly AppDbContext dbContext;
	public EfBudgetTagRepository(AppDbContext dbContext) => this.dbContext = dbContext;
	public IReadOnlyList<BudgetTag> GetAll() => dbContext.BudgetTags.ToList();
	public void Add(BudgetTag budgetTag) => dbContext.BudgetTags.Add(budgetTag);
	public bool IsUsedByDocumentItem(int budgetTagId) => dbContext.ExpenseDocumentItems.Any(x => x.BudgetTagId == budgetTagId);
	public void Remove(BudgetTag budgetTag) => dbContext.BudgetTags.Remove(budgetTag);
}
