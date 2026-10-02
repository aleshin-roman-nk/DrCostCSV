using Application.ExpenseDocuments.Abstractions;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.ExpenseDocuments.GetDocumentsWithoutCurrency;
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
					CurrencyId = document.CurrencyId,
					CurrencyValues = document.CurrencyValues
						.Select(value => new Application.CurrencyDefaults.CurrencyValueDto(value.CurrencyId, value.Value))
						.ToList(),
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
							BudgetLineName = item.BudgetLine.Name,
							BudgetTagId = item.BudgetTagId,
							BudgetTagName = item.BudgetTag == null
								? null
								: item.BudgetTag.Name
						})
						.ToList()
				})
				.SingleOrDefault();
		}

		public IReadOnlyList<DocumentWithoutCurrencyDto> GetWithoutCurrency() =>
			dbContext.ExpenseDocuments
				.AsNoTracking()
				.Where(document => document.CurrencyId == null)
				.OrderByDescending(document => document.Date)
				.ThenByDescending(document => document.Id)
				.Select(document => new DocumentWithoutCurrencyDto(
					document.Id,
					document.Date,
					document.SellerName ?? string.Empty,
					document.Items.Count))
				.ToList();
	}
}
