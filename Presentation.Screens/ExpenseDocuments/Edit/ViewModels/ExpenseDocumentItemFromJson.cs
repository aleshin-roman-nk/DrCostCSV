using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public class ExpenseDocumentItemFromJson
{
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public decimal Amount { get; set; }
	public string BudgetLine { get; set; } = string.Empty;
	public string? BudgetTag { get; set; }
}
