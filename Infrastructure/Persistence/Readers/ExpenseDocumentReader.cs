using Application.ExpenseDocuments.Abstractions;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Readers
{
	public class ExpenseDocumentReader : IExpenseDocumentReader
	{
		private readonly AppDbContext dbContext;

		public ExpenseDocumentReader(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public ExpenseDocumentForEditDto? GetForEdit(int documentId)
		{
			return dbContext.ExpenseDocuments
				.AsNoTracking()
				.Where(document => document.Id == documentId)
				.Select(document => new ExpenseDocumentForEditDto
				{
					Id = document.Id,
					Date = document.Date,
					SellerName = document.SellerName ?? string.Empty,

					Items = document.Items
						.Select(item => new ExpenseDocumentItemForEditDto
						{
							Id = item.Id,
							Name = item.Name,
							Price = item.Price,
							Amount = item.Amount,
							BudgetLineId = item.BudgetLineId,
							BudgetLineName = item.BudgetLine != null
								? item.BudgetLine.Name
								: string.Empty,
							BudgetTagId = item.BudgetTagId,
							BudgetTagName = item.BudgetTag != null
								? item.BudgetTag.Name
								: null
						})
						.ToList()
				})
				.SingleOrDefault();
		}
	}
}
