using Application.Abstractions.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddSqlitePersistence(
							this IServiceCollection services,
							string connectionString)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlite(connectionString));

			services.AddScoped<IExpenseDocumentRepository, ExpenseDocumentRepositorySQLite>();
			return services;
		}
	}
}
