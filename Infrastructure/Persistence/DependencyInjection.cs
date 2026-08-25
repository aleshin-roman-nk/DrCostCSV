using Application.BudgetLines.Abstractions;
using Application.BudgetLines.GetBudgetLines;
using Application.BudgetTags.Abstractions;
using Application.BudgetTags.GetBudgetTags;
using Application.Common.Abstractions.Persistence;
using Application.ExpenseDocuments.Abstractions;
using Application.ExpenseDocuments.Abstractions.Persistence;
using Application.Reports.Abstractions;
using Application.JsonImportPromptSettings.Abstractions;
using Infrastructure.Persistence.Readers;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Reports;
using Infrastructure.OpenAI;
using Application.ExpenseDocuments.RecognizeReceipt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddSqlitePersistence(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
		services.AddScoped<IUnitOfWork, EfUnitOfWork>();
		services.AddScoped<IExpenseDocumentRepository, ExpenseDocumentRepositorySQLite>();
		services.AddScoped<IExpenseReportsReader, SQLiteExpenseReportsReader>();
		services.AddScoped<IBudgetLineReader, EfBudgetLineReader>();
		services.AddScoped<GetBudgetLinesUseCase>();
		services.AddScoped<IBudgetLineRepository, EfBudgetLineRepository>();
		services.AddScoped<IBudgetTagReader, EfBudgetTagReader>();
		services.AddScoped<GetBudgetTagsUseCase>();
		services.AddScoped<IBudgetTagRepository, EfBudgetTagRepository>();
		services.AddScoped<IExpenseDocumentReader, ExpenseDocumentReader>();
		services.AddSingleton(new HttpClient());
		services.AddScoped<IReceiptImageRecognizer, OpenAiReceiptImageRecognizer>();
		services.AddScoped<IJsonImportPromptSettingsRepository, EfJsonImportPromptSettingsRepository>();
		return services;
	}
}
