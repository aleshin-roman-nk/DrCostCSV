namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public sealed record CurrencyOptionViewModel(int Id, string Code);

public sealed record ExpenseDocumentCurrencyDefaultsViewModel(
	int? CurrencyId, IReadOnlyList<ExpenseDocumentCurrencyValueViewModel> CurrencyValues);
