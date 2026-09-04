using Application.Common;
using Application.DatabasePathSettings.Abstractions;

namespace Application.DatabasePathSettings;

public sealed class SaveDatabasePathUseCase
{
	private readonly IDatabasePathSettingsRepository repository;

	public SaveDatabasePathUseCase(IDatabasePathSettingsRepository repository)
	{
		this.repository = repository;
	}

	public UseCaseResult Execute(SaveDatabasePathCommand command)
	{
		if (string.IsNullOrWhiteSpace(command.DatabasePath))
			return UseCaseResult.Failure(new UseCaseError("database_path_empty", "Не выбрана папка для файла базы данных."));

		try
		{
			repository.SaveDatabasePath(command.DatabasePath);
			return UseCaseResult.Success();
		}
		catch (Exception exception)
		{
			return UseCaseResult.Failure(new UseCaseError("database_path_save_failed", exception.Message));
		}
	}
}
