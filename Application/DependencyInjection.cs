using Application.BudgetLines.Abstractions;
using Application.BudgetLines.Services;
using Application.BudgetLines.ManageBudgetLine;
using Application.BudgetTags.Abstractions;
using Application.BudgetTags.Services;
using Application.BudgetTags.ManageBudgetTag;
using Application.ExpenseDocuments.CreateExpenseDocument;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.ExpenseDocuments.UpdateExpenseDocument;
using Application.ExpenseDocuments.RecognizeReceipt;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetDocumentTitlesByDay;
using Application.Reports.GetBudgetLineExpensesByMonth;
using Application.JsonImportPromptSettings;
using Application.DatabasePathSettings;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<GetDailyExpensesByMonthUseCase>();
		services.AddScoped<GetBudgetLineExpensesByMonthUseCase>();
		services.AddScoped<CreateExpenseDocumentUseCase>();
		services.AddScoped<IBudgetLineEnsurer, BudgetLineEnsurer>();
		services.AddScoped<IBudgetTagEnsurer, BudgetTagEnsurer>();
		services.AddScoped<GetDocumentTitlesByDayUseCase>();
		services.AddScoped<GetExpenseDocumentForEditUseCase>();
		services.AddScoped<UpdateExpenseDocumentUseCase>();
		services.AddScoped<BuildReceiptPromptUseCase>();
		services.AddScoped<RecognizeReceiptUseCase>();
		services.AddScoped<AddBudgetLineUseCase>();
		services.AddScoped<DeleteBudgetLineUseCase>();
		services.AddScoped<AddBudgetTagUseCase>();
		services.AddScoped<DeleteBudgetTagUseCase>();
		services.AddScoped<GetJsonImportPromptSettingsUseCase>();
		services.AddScoped<SaveJsonImportPromptSettingsUseCase>();
		services.AddScoped<GetDatabasePathUseCase>();
		services.AddScoped<SaveDatabasePathUseCase>();
		return services;
	}
}
