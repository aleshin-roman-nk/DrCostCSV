using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

public sealed class WithoutCurrencyFlow(IScreenScopedExecutor screenScopedExecutor)
{
	public ScreenResult Run() =>
		screenScopedExecutor.Execute<WithoutCurrencyPresenter, ScreenResult>(presenter => presenter.Run());
}
