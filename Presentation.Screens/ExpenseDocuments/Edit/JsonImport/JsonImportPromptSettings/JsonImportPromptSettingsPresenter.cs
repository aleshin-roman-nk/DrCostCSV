using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

public sealed class JsonImportPromptSettingsPresenter
{
	private readonly IJsonImportPromptSettingsView view;
	private readonly JsonImportPromptSettingsActions actions;
	private IReadOnlyList<BudgetLineOptionViewModel> budgetLines = Array.Empty<BudgetLineOptionViewModel>();
	private IReadOnlyList<BudgetTagOptionViewModel> budgetTags = Array.Empty<BudgetTagOptionViewModel>();

	public JsonImportPromptSettingsPresenter(IJsonImportPromptSettingsView view, JsonImportPromptSettingsActions actions)
	{
		this.view = view;
		this.actions = actions;
		view.CopyRequested += View_CopyRequested;
		view.AdditionalInstructionsChanged += View_AdditionalInstructionsChanged;
		view.SaveRequested += View_SaveRequested;
		view.AddBudgetLineRequested += View_AddBudgetLineRequested;
		view.DeleteBudgetLineRequested += View_DeleteBudgetLineRequested;
		view.AddBudgetTagRequested += View_AddBudgetTagRequested;
		view.DeleteBudgetTagRequested += View_DeleteBudgetTagRequested;
	}

	public ScreenResult Run()
	{
		var setup = Load();
		if (!setup.IsSuccess)
			return setup;

		if (view.ShowModal() != ModalResult.Ok)
			return ScreenResult.Cancelled();

		return ScreenResult.Success();
	}

	private ScreenResult Load()
	{
		var lines = actions.GetBudgetLines();
		if (!lines.IsSuccess)
			return ScreenResult.Failure(lines.Error ?? "Не удалось загрузить строки бюджета.");
		var tags = actions.GetBudgetTags();
		if (!tags.IsSuccess)
			return ScreenResult.Failure(tags.Error ?? "Не удалось загрузить уточняющие теги.");
		var settings = actions.GetAdditionalInstructions();
		if (!settings.IsSuccess)
			return ScreenResult.Failure(settings.Error ?? "Не удалось загрузить настройки промпта.");

		budgetLines = lines.Data ?? Array.Empty<BudgetLineOptionViewModel>();
		budgetTags = tags.Data ?? Array.Empty<BudgetTagOptionViewModel>();
		view.SetBudgetClassifications(budgetLines, budgetTags);
		view.SetAdditionalInstructions(settings.Data ?? string.Empty);
		var prompt = SetPrompt(settings.Data ?? string.Empty);
		if (!prompt.IsSuccess)
			return ScreenResult.Failure(prompt.Error ?? "Не удалось сформировать промпт.");
		return ScreenResult.Success();
	}

	private void View_CopyRequested()
	{
		view.CopyPromptToClipboard();
	}

	private void View_AdditionalInstructionsChanged()
	{
		var result = SetPrompt(view.GetAdditionalInstructions());
		if (!result.IsSuccess)
			view.ShowError(result.Error ?? "Не удалось сформировать промпт.");
	}

	private void View_SaveRequested()
	{
		var save = actions.SaveAdditionalInstructions(view.GetAdditionalInstructions());
		if (!save.IsSuccess)
		{
			view.ShowError(save.Error ?? "Не удалось сохранить настройки промпта.");
			return;
		}

		view.CloseSuccessfully();
	}

	private void View_AddBudgetLineRequested()
	{
		var result = actions.AddBudgetLine(view.GetNewBudgetLineName());
		if (!result.IsSuccess) { view.ShowError(result.Error ?? "Не удалось добавить строку бюджета."); return; }
		view.ClearBudgetLineInput();
		ReloadClassifications();
	}

	private void View_DeleteBudgetLineRequested()
	{
		var id = view.GetSelectedBudgetLineId();
		if (!id.HasValue) { view.ShowError("Выберите строку бюджета для удаления."); return; }
		var result = actions.DeleteBudgetLine(id.Value);
		if (!result.IsSuccess) { view.ShowError(result.Error ?? "Не удалось удалить строку бюджета."); return; }
		ReloadClassifications();
	}

	private void View_AddBudgetTagRequested()
	{
		var lineId = view.GetSelectedBudgetLineId();
		if (!lineId.HasValue) { view.ShowError("Сначала выберите строку бюджета."); return; }
		var result = actions.AddBudgetTag(lineId.Value, view.GetNewBudgetTagName());
		if (!result.IsSuccess) { view.ShowError(result.Error ?? "Не удалось добавить уточняющий тег."); return; }
		view.ClearBudgetTagInput();
		ReloadClassifications();
	}

	private void View_DeleteBudgetTagRequested()
	{
		var id = view.GetSelectedBudgetTagId();
		if (!id.HasValue) { view.ShowError("Выберите уточняющий тег для удаления."); return; }
		var result = actions.DeleteBudgetTag(id.Value);
		if (!result.IsSuccess) { view.ShowError(result.Error ?? "Не удалось удалить уточняющий тег."); return; }
		ReloadClassifications();
	}

	private void ReloadClassifications()
	{
		var lines = actions.GetBudgetLines();
		var tags = actions.GetBudgetTags();
		if (!lines.IsSuccess || !tags.IsSuccess)
		{
			view.ShowError(lines.Error ?? tags.Error ?? "Не удалось обновить справочник.");
			return;
		}
		budgetLines = lines.Data ?? Array.Empty<BudgetLineOptionViewModel>();
		budgetTags = tags.Data ?? Array.Empty<BudgetTagOptionViewModel>();
		view.SetBudgetClassifications(budgetLines, budgetTags);
		var prompt = SetPrompt(view.GetAdditionalInstructions());
		if (!prompt.IsSuccess)
			view.ShowError(prompt.Error ?? "Не удалось сформировать промпт.");
	}

	private ActionResult<string> SetPrompt(string additionalInstructions)
	{
		var result = actions.BuildPrompt(budgetLines, budgetTags, additionalInstructions);
		if (result.IsSuccess)
			view.SetPrompt(result.Data ?? string.Empty);

		return result;
	}
}
