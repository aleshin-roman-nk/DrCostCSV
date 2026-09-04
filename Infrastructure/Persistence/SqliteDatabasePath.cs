namespace Infrastructure.Persistence;

public static class SqliteDatabasePath
{
	public static string GetDefaultDatabasePath()
	{
		return Path.Combine(AppContext.BaseDirectory, "drcost.sqlite");
	}

	public static string GetLegacyDatabasePath()
	{
		return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "Economy", "drcost.sqlite"));
	}

	public static string GetConnectionString(string databasePath)
	{
		var dbPath = Path.GetFullPath(databasePath);
		Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
		return new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder
		{
			DataSource = dbPath
		}.ToString();
	}
}
