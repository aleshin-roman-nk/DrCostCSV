using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public class ExpenseDocumentViewModel
{
	public int? Id { get; set; }
	public string Seller { get; set; } = string.Empty;
	public DateTime Date { get; set; } = DateTime.Today;
	public int? CurrencyId { get; set; }
	public BindingList<ExpenseDocumentCurrencyValueViewModel> CurrencyValues { get; set; } = new();
	public BindingList<ExpenseDocumentItemViewModel> Items { get; set; } = new();

}
