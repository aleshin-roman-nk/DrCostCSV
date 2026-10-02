using Presentation.Screens.Common;

namespace Presentation.Screens.Currencies.List;

public sealed class CurrencyListFlow(IScreenScopedExecutor executor)
{
	public ScreenResult Run() =>
		executor.Execute<CurrencyListPresenter, ScreenResult>(presenter => presenter.Run());
}
