using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.ExpenseDocument.Dto
{
	public sealed class ExpenseDocumentDto
	{
		public int id { get; set; }
		public string? SellerName { get; set; }
		public DateOnly Date { get; set; }
		public IReadOnlyList<ExpenseDocumentItemDto>? ExpenseDocumentItem { get; set; }
	}
}
