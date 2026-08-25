using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence;

public class AppDbContext: DbContext
{
	public DbSet<ExpenseDocument> ExpenseDocuments => Set<ExpenseDocument>();
	public DbSet<ExpenseDocumentItem> ExpenseDocumentItems => Set<ExpenseDocumentItem>();
	public DbSet<BudgetLine> BudgetLines => Set<BudgetLine>();
	public DbSet<BudgetTag> BudgetTags => Set<BudgetTag>();
	public DbSet<JsonImportPromptSettings> JsonImportPromptSettings => Set<JsonImportPromptSettings>();

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(AppDbContext).Assembly);
	}
}
