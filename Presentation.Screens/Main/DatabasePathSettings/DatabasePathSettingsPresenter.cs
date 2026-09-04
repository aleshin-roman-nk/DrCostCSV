using Presentation.Screens.Common;

namespace Presentation.Screens.Main.DatabasePathSettings;

public sealed class DatabasePathSettingsPresenter
{
	private readonly IDatabasePathSettingsView view;
	private readonly DatabasePathSettingsActions actions;

	public DatabasePathSettingsPresenter(IDatabasePathSettingsView view, DatabasePathSettingsActions actions)
	{
		this.view = view;
		this.actions = actions;
		view.SaveRequested += View_SaveRequested;
	}

	public ScreenResult Run()
	{
		var databasePath = actions.GetDatabasePath();
		if (!databasePath.IsSuccess)
			return ScreenResult.Failure(databasePath.Error ?? "Не удалось прочитать путь к файлу базы данных.");

		view.SetDatabasePath(databasePath.Data ?? string.Empty);

		return view.ShowModal() == ModalResult.Ok
			? ScreenResult.Success()
			: ScreenResult.Cancelled();
	}

	private void View_SaveRequested()
	{
		var result = actions.SaveDatabasePath(view.GetDatabasePath());
		if (!result.IsSuccess)
		{
			view.ShowError(result.Error ?? "Не удалось сохранить путь к файлу базы данных.");
			return;
		}

		view.CloseSuccessfully();
	}
}
