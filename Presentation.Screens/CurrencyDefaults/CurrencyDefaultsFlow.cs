using Presentation.Screens.Common;

namespace Presentation.Screens.CurrencyDefaults;

public sealed class CurrencyDefaultsFlow(IScreenScopedExecutor executor)
{
	public ScreenResult Run() =>
		executor.Execute<CurrencyDefaultsPresenter, ScreenResult>(presenter => presenter.Run());
}
