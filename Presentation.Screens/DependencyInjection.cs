using Microsoft.Extensions.DependencyInjection;
using Presentation.Screens.ExpenseDocuments.Edit;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.ExpenseDocuments.Edit.Item;
using Presentation.Screens.ExpenseDocuments.Edit.JsonImport;
using Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;
using Presentation.Screens.ExpenseDocuments.List;
using Presentation.Screens.Common;
using Presentation.Screens.Main;
using Presentation.Screens.Main.DatabasePathSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens;

public static class DependencyInjection
{
	public static IServiceCollection AddWinFormsPresentation(
		this IServiceCollection services)
	{
		//Bucause we register only IRootView, sp.GetRequiredService<RootForm>() will throw an exception
		//services.AddSingleton<IRootView, RootForm>();

		// Register RootForm as a singleton and map IRootView to the same instance.
		// This ensures that resolving either RootForm or IRootView gives the same object.
		services
			.AddSingleton<IScreenScopedExecutor, ScreenScopedExecutor>()
			.AddSingleton<IUseCaseScopedExecutor, UseCaseScopedExecutor>()
			.AddSingleton<IValidationErrorDialog, ValidationErrorDialog>()

			.AddSingleton<MainForm>()
			.AddSingleton<IMainView>(sp => sp.GetRequiredService<MainForm>())
			.AddSingleton<MainPresenter>()
			.AddSingleton<MainActions>()
			.AddSingleton<DatabasePathSettingsFlow>()
			.AddScoped<IDatabasePathSettingsView, DatabasePathSettingsForm>()
			.AddScoped<DatabasePathSettingsPresenter>()
			.AddScoped<DatabasePathSettingsActions>()

			.AddSingleton<ExpenseDocumentEditFlow>()
			.AddScoped<IExpenseDocumentEditView, ExpenseDocumentEditForm>()
			.AddScoped<ExpenseDocumentEditPresenter>()
			.AddScoped<ExpenseDocumentEditActions>()
			.AddScoped<IViewModelVerifier<ExpenseDocumentViewModel>, ExpenseDocumentViewModelVerifier>()
			.AddSingleton<IExpenseDocumentJsonImportFlow, ExpenseDocumentJsonImportFlow>()
			.AddScoped<IExpenseDocumentJsonImportView, ExpenseDocumentJsonImportForm>()
			.AddScoped<ExpenseDocumentJsonImportPresenter>()
			.AddScoped<ExpenseDocumentJsonImportActions>()
			.AddSingleton<JsonImportPromptSettingsFlow>()
			.AddScoped<IJsonImportPromptSettingsView, JsonImportPromptSettingsForm>()
			.AddScoped<JsonImportPromptSettingsPresenter>()
			.AddScoped<JsonImportPromptSettingsActions>()
			.AddSingleton<ExpenseDocumentItemEditFlow>()
			.AddScoped<ExpenseDocumentItemEditPresenter>()
			.AddScoped<IExpenseDocumentItemEditView, ExpenseDocumentItemEditForm>()

			.AddSingleton<ExpenseDocumentListFlow>()
			.AddScoped<IExpenseDocumentListView, ExpenseDocumentListForm>()
			.AddScoped<ExpenseDocumentListPresenter>()
			.AddScoped<ExpenseDocumentListActions>()
			;


		return services;
	}
}
