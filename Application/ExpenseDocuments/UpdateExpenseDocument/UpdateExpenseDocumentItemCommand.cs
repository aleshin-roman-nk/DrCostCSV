using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.UpdateExpenseDocument
{
	public class UpdateExpenseDocumentItemCommand
	{
		// null означает новую позицию документа.
		public int? Id { get; init; }

		public required string Name { get; init; }

		public decimal? Price { get; init; }

		public decimal? Amount { get; init; }

		public int? BudgetLineId { get; init; }
		public required string BudgetLineName { get; init; }
		public int? BudgetTagId { get; init; }
		public string? BudgetTagName { get; init; }
	}
}
