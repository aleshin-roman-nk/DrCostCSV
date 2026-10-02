using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System.ComponentModel;

namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

public sealed class ExpenseDocumentCurrencyEditViewModel
{
	public int? CurrencyId { get; set; }
	public BindingList<ExpenseDocumentCurrencyValueViewModel> CurrencyValues { get; set; } = new();
}
