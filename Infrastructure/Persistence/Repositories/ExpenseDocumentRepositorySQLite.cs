using Application.ExpenseDocuments.Abstractions.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
	public class ExpenseDocumentRepositorySQLite : IExpenseDocumentRepository
	{
		private readonly AppDbContext db;

		public ExpenseDocumentRepositorySQLite(AppDbContext db)
		{
			this.db = db;
		}

		public void Add(ExpenseDocument document)
		{
			db.ExpenseDocuments.Add(document);
		}

		public ExpenseDocument? GetById(int id)
		{
			return db.ExpenseDocuments
				.Include(document => document.CurrencyValues)
				.Include(document => document.Items)
					.ThenInclude(item => item.BudgetTag)
				.SingleOrDefault(document => document.Id == id);
		}

		public void Remove(ExpenseDocument document)
		{
			ArgumentNullException.ThrowIfNull(document);

			db.ExpenseDocuments.Remove(document);
		}
	}
}
