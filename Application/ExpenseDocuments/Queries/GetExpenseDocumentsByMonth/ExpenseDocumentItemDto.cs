using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Queries.GetExpenseDocumentsByMonth;

public sealed class ExpenseDocumentItemDto
{
	public string? Name { get; set; }
	public decimal? Price { get; set; }
	public decimal? Amount { get; set; }
	public decimal? Sum => Price * Amount;
}
