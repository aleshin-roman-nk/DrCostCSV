using Presentation.Screens.Common;

namespace Presentation.Screens.Main.DatabasePathSettings;

public sealed class DatabasePathSettingsFlow
{
	private readonly IScreenScopedExecutor screenScopedExecutor;

	public DatabasePathSettingsFlow(IScreenScopedExecutor screenScopedExecutor)
	{
		this.screenScopedExecutor = screenScopedExecutor;
	}

	public ScreenResult Run() => screenScopedExecutor.Execute<DatabasePathSettingsPresenter, ScreenResult>(presenter => presenter.Run());
}
