using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.GetExpenseDocumentForEdit;

public class ExpenseDocumentForEditDto
{
	public int Id { get; init; }
	public DateTime Date { get; init; }
	public required string SellerName { get; init; }

	public required IReadOnlyList<ExpenseDocumentItemForEditDto> Items
	{
		get;
		init;
	}
}

public class ExpenseDocumentItemForEditDto
{
	public int Id { get; init; }
	public required string Name { get; init; }
	public decimal Price { get; init; }
	public decimal Amount { get; init; }
	public int? BudgetLineId { get; init; }
	public required string BudgetLineName { get; init; }
	public int? BudgetTagId { get; init; }
	public string? BudgetTagName { get; init; }
}
