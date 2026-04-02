using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Commands.CreateExpenseDocument
{
	public class CreateExpenseDocumentItemDto
	{
		public required string Name { get; set; }
		public decimal? Price { get; set; }
		public decimal? Amount { get; set; }
		public int CategoryId {  get; set; }
	}
}
