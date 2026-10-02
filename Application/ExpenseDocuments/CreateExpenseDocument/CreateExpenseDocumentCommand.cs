using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.CreateExpenseDocument
{
	public class CreateExpenseDocumentCommand
	{
		public int? CurrencyId { get; init; }
		public IReadOnlyList<Application.CurrencyDefaults.CurrencyValueDto>? CurrencyValues { get; init; }
		public string? SellerName { get; init; }
		public DateTime Date { get; init; }
		public IReadOnlyList<CreateExpenseDocumentItemCommand> Items { get; init; } = [];
	}
}
