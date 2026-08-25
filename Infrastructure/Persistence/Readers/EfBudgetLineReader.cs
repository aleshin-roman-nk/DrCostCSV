using Application.BudgetLines;
using Application.BudgetLines.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Readers;

public sealed class EfBudgetLineReader : IBudgetLineReader
{
	private readonly AppDbContext dbContext;
	public EfBudgetLineReader(AppDbContext dbContext) => this.dbContext = dbContext;
	public IReadOnlyList<BudgetLineDto> GetAll() => dbContext.BudgetLines.AsNoTracking().OrderBy(x => x.Name)
		.Select(x => new BudgetLineDto { Id = x.Id, Name = x.Name }).ToList();
}
