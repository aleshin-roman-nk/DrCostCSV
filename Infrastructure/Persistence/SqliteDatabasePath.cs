namespace Infrastructure.Persistence;

public static class SqliteDatabasePath
{
	public static string GetConnectionString()
	{
		var dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "Economy", "drcost.sqlite"));
		Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
		return $"Data Source={dbPath}";
	}
}