using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.CreateExpenseDocument
{
	public class CreateExpenseDocumentCommand
	{
		public string? SellerName { get; init; }
		public DateTime Date { get; init; }
		public IReadOnlyList<CreateExpenseDocumentItemCommand> Items { get; init; } = [];
	}
}
