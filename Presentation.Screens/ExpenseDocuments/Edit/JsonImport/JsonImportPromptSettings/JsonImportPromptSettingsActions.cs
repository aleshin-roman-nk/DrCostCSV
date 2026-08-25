using Application.BudgetLines;
using Application.BudgetLines.GetBudgetLines;
using Application.BudgetLines.ManageBudgetLine;
using Application.BudgetTags;
using Application.BudgetTags.GetBudgetTags;
using Application.BudgetTags.ManageBudgetTag;
using Application.JsonImportPromptSettings;
using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

public sealed class JsonImportPromptSettingsActions
{
	private readonly IUseCaseScopedExecutor useCaseScopedExecutor;

	public JsonImportPromptSettingsActions(IUseCaseScopedExecutor useCaseScopedExecutor)
	{
		this.useCaseScopedExecutor = useCaseScopedExecutor;
	}

	public ActionResult<IReadOnlyList<BudgetLineOptionViewModel>> GetBudgetLines() => useCaseScopedExecutor.Execute<GetBudgetLinesUseCase, ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>>(useCase =>
	{
		var result = useCase.Execute();
		return result.IsSuccess
			? ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>.Success((result.Value ?? Array.Empty<BudgetLineDto>()).Select(x => new BudgetLineOptionViewModel { Id = x.Id, Name = x.Name }).ToList())
			: ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить строки бюджета.");
	});

	public ActionResult<IReadOnlyList<BudgetTagOptionViewModel>> GetBudgetTags() => useCaseScopedExecutor.Execute<GetBudgetTagsUseCase, ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>>(useCase =>
	{
		var result = useCase.Execute();
		return result.IsSuccess
			? ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>.Success((result.Value ?? Array.Empty<BudgetTagDto>()).Select(x => new BudgetTagOptionViewModel { Id = x.Id, BudgetLineId = x.BudgetLineId, Name = x.Name }).ToList())
			: ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить уточняющие теги.");
	});

	public ActionResult<string> GetAdditionalInstructions() => useCaseScopedExecutor.Execute<GetJsonImportPromptSettingsUseCase, ActionResult<string>>(useCase =>
	{
		var result = useCase.Execute();
		return result.IsSuccess
			? ActionResult<string>.Success(result.Value?.AdditionalInstructions ?? string.Empty)
			: ActionResult<string>.Failure(result.Error?.Message ?? "Не удалось загрузить настройки промпта.");
	});

	public ActionResult SaveAdditionalInstructions(string instructions) => useCaseScopedExecutor.Execute<SaveJsonImportPromptSettingsUseCase, ActionResult>(useCase =>
	{
		var result = useCase.Execute(new SaveJsonImportPromptSettingsCommand { AdditionalInstructions = instructions });
		return result.IsSuccess ? ActionResult.Success() : ActionResult.Failure(result.Error?.Message ?? "Не удалось сохранить настройки промпта.");
	});

	public ActionResult AddBudgetLine(string name) => Execute<AddBudgetLineUseCase>(useCase => useCase.Execute(name));
	public ActionResult DeleteBudgetLine(int id) => Execute<DeleteBudgetLineUseCase>(useCase => useCase.Execute(id));
	public ActionResult AddBudgetTag(int budgetLineId, string name) => Execute<AddBudgetTagUseCase>(useCase => useCase.Execute(budgetLineId, name));
	public ActionResult DeleteBudgetTag(int id) => Execute<DeleteBudgetTagUseCase>(useCase => useCase.Execute(id));

	private ActionResult Execute<TUseCase>(Func<TUseCase, Application.Common.UseCaseResult> execute)
		where TUseCase : notnull => useCaseScopedExecutor.Execute<TUseCase, ActionResult>(useCase =>
		{
			var result = execute(useCase);
			return result.IsSuccess ? ActionResult.Success() : ActionResult.Failure(result.Error?.Message ?? "Операция не выполнена.");
		});
}
