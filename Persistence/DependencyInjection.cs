using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddSqlitePersistence(
							this IServiceCollection services,
							string connectionString)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlite(connectionString));

			services.AddScoped<IExpenseDocumentRepository, ExpenseDocumentRepository>();
			return services;
		}
	}
}
