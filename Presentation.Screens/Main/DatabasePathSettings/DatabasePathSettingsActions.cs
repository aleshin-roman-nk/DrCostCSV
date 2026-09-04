using Application.DatabasePathSettings;
using Presentation.Screens.Common;

namespace Presentation.Screens.Main.DatabasePathSettings;

public sealed class DatabasePathSettingsActions
{
	private readonly IUseCaseScopedExecutor useCaseScopedExecutor;

	public DatabasePathSettingsActions(IUseCaseScopedExecutor useCaseScopedExecutor)
	{
		this.useCaseScopedExecutor = useCaseScopedExecutor;
	}

	public ActionResult<string> GetDatabasePath() => useCaseScopedExecutor.Execute<GetDatabasePathUseCase, ActionResult<string>>(useCase =>
	{
		var result = useCase.Execute();

		return result.IsSuccess && result.Value is not null
			? ActionResult<string>.Success(result.Value)
			: ActionResult<string>.Failure(result.Error?.Message ?? "Не удалось прочитать путь к файлу базы данных.");
	});

	public ActionResult SaveDatabasePath(string databasePath) => useCaseScopedExecutor.Execute<SaveDatabasePathUseCase, ActionResult>(useCase =>
	{
		var result = useCase.Execute(new SaveDatabasePathCommand { DatabasePath = databasePath });

		return result.IsSuccess
			? ActionResult.Success()
			: ActionResult.Failure(result.Error?.Message ?? "Не удалось сохранить путь к файлу базы данных.");
	});
}
