using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public class ExpenseDocumentItemViewModel
{
	public int? Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public decimal Amount { get; set; }
	public decimal Sum => Price * Amount;
	public int? BudgetLineId { get; set; }
	public string BudgetLineName { get; set; } = string.Empty;
	public int? BudgetTagId { get; set; }
	public string BudgetTagName { get; set; } = string.Empty;
}
