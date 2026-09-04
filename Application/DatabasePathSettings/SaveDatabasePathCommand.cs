namespace Application.DatabasePathSettings;

public sealed class SaveDatabasePathCommand
{
	public string DatabasePath { get; init; } = string.Empty;
}
