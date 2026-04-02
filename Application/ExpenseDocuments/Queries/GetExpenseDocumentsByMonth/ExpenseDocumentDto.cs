using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Queries.GetExpenseDocumentsByMonth;

public sealed class ExpenseDocumentDto
{
	public int id { get; set; }
	public string? SellerName { get; set; }
	public DateOnly Date { get; set; }
	public List<ExpenseDocumentItemDto>? ExpenseDocumentItem { get; set; }
}
