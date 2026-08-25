using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public sealed class AppDbContextFactory
	: IDesignTimeDbContextFactory<AppDbContext>
{
	public AppDbContext CreateDbContext(string[] args)
	{
		var connectionString = SqliteDatabasePath.GetConnectionString();

		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseSqlite(connectionString)
			.Options;

		return new AppDbContext(options);
	}
}