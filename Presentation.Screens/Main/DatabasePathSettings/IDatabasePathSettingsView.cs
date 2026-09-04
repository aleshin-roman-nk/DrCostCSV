using Presentation.Screens.Common;

namespace Presentation.Screens.Main.DatabasePathSettings;

public interface IDatabasePathSettingsView
{
	event Action? SaveRequested;

	ModalResult ShowModal();
	string GetDatabasePath();
	void SetDatabasePath(string databasePath);
	void CloseSuccessfully();
	void ShowError(string message);
}
