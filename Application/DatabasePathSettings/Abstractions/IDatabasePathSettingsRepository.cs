namespace Application.DatabasePathSettings.Abstractions;

public interface IDatabasePathSettingsRepository
{
	string GetDatabasePath();

	void SaveDatabasePath(string databasePath);
}
