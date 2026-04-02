using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Commands.CreateExpenseDocument
{
	public class CreateExpenseDocumentCommand
	{
		public string? SellerName { get; init; }
		public DateOnly Date { get; init; }
		public IReadOnlyList<CreateExpenseDocumentItemDto> Items { get; init; } = [];
	}
}
