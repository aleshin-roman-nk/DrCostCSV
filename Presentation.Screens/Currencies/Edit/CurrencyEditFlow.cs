using Presentation.Screens.Common;

namespace Presentation.Screens.Currencies.Edit;

public sealed class CurrencyEditFlow(IScreenScopedExecutor executor)
{
	public ScreenResult<int> Run(int? id) =>
		executor.Execute<CurrencyEditPresenter, ScreenResult<int>>(presenter => presenter.Run(id));
}
