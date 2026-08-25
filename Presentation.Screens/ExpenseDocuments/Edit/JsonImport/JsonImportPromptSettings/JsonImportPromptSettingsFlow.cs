using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

public sealed class JsonImportPromptSettingsFlow
{
	private readonly IScreenScopedExecutor screenScopedExecutor;

	public JsonImportPromptSettingsFlow(IScreenScopedExecutor screenScopedExecutor)
	{
		this.screenScopedExecutor = screenScopedExecutor;
	}

	public ScreenResult Run() => screenScopedExecutor.Execute<JsonImportPromptSettingsPresenter, ScreenResult>(presenter => presenter.Run());
}
