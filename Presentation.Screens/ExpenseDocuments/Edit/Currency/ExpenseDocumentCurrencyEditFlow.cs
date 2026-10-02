using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

public sealed class ExpenseDocumentCurrencyEditFlow(IScreenScopedExecutor screenScopedExecutor)
{
	public ScreenResult<ExpenseDocumentCurrencyEditViewModel> Edit(
		int? currencyId,
		IReadOnlyList<ExpenseDocumentCurrencyValueViewModel> values,
		IReadOnlyList<CurrencyOptionViewModel> currencies)
	{
		ArgumentNullException.ThrowIfNull(values);
		ArgumentNullException.ThrowIfNull(currencies);
		return screenScopedExecutor.Execute<ExpenseDocumentCurrencyEditPresenter,
			ScreenResult<ExpenseDocumentCurrencyEditViewModel>>(
			presenter => presenter.Edit(currencyId, values, currencies));
	}
}
