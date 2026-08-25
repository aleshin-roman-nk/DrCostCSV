using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.CreateExpenseDocument;

public class CreateExpenseDocumentItemCommand
{
	public required string Name { get; set; }
	public decimal Price { get; set; }
	public decimal Amount { get; set; }
	public int? BudgetLineId { get; set; }
	public string? BudgetLineName { get; set; }
	public int? BudgetTagId { get; set; }
	public string? BudgetTagName { get; set; }
}
