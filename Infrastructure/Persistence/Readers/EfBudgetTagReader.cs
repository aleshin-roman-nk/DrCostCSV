using Application.BudgetTags;
using Application.BudgetTags.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Readers;

public sealed class EfBudgetTagReader : IBudgetTagReader
{
	private readonly AppDbContext dbContext;
	public EfBudgetTagReader(AppDbContext dbContext) => this.dbContext = dbContext;
	public IReadOnlyList<BudgetTagDto> GetAll() => dbContext.BudgetTags.AsNoTracking().OrderBy(x => x.Name)
		.Select(x => new BudgetTagDto { Id = x.Id, BudgetLineId = x.BudgetLineId, Name = x.Name }).ToList();
}
