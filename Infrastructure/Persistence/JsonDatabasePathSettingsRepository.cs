using Application.DatabasePathSettings.Abstractions;
using System.Text.Json;

namespace Infrastructure.Persistence;

public sealed class JsonDatabasePathSettingsRepository : IDatabasePathSettingsRepository
{
	private static readonly JsonSerializerOptions serializerOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true
	};

	private readonly string settingsFilePath;

	public JsonDatabasePathSettingsRepository()
		: this(Path.Combine(AppContext.BaseDirectory, "settings.json"))
	{
	}

	internal JsonDatabasePathSettingsRepository(string settingsFilePath)
	{
		this.settingsFilePath = settingsFilePath;
	}

	public bool HasConfiguredDatabasePath()
	{
		if (!File.Exists(settingsFilePath))
			return false;

		var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(settingsFilePath), serializerOptions)
			?? throw new InvalidOperationException("Файл settings.json содержит недопустимые настройки.");

		return !string.IsNullOrWhiteSpace(settings.DatabasePath);
	}

	public string GetDatabasePath()
	{
		if (!File.Exists(settingsFilePath))
		{
			var legacyDatabasePath = SqliteDatabasePath.GetLegacyDatabasePath();

			return File.Exists(legacyDatabasePath)
				? legacyDatabasePath
				: SqliteDatabasePath.GetDefaultDatabasePath();
		}

		var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(settingsFilePath), serializerOptions)
			?? throw new InvalidOperationException("Файл settings.json содержит недопустимые настройки.");

		return string.IsNullOrWhiteSpace(settings.DatabasePath)
			? SqliteDatabasePath.GetDefaultDatabasePath()
			: Path.GetFullPath(settings.DatabasePath);
	}

	public void SaveDatabasePath(string databasePath)
	{
		var settingsDirectory = Path.GetDirectoryName(settingsFilePath)
			?? throw new InvalidOperationException("Не удалось определить папку для settings.json.");

		Directory.CreateDirectory(settingsDirectory);

		var settings = new Settings
		{
			DatabasePath = Path.GetFullPath(databasePath)
		};

		File.WriteAllText(settingsFilePath, JsonSerializer.Serialize(settings, serializerOptions));
	}

	private sealed class Settings
	{
		public string? DatabasePath { get; init; }
	}
}
