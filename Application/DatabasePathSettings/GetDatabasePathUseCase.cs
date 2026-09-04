using Application.Common;
using Application.DatabasePathSettings.Abstractions;

namespace Application.DatabasePathSettings;

public sealed class GetDatabasePathUseCase
{
	private readonly IDatabasePathSettingsRepository repository;

	public GetDatabasePathUseCase(IDatabasePathSettingsRepository repository)
	{
		this.repository = repository;
	}

	public UseCaseResult<string> Execute()
	{
		try
		{
			return UseCaseResult<string>.Success(repository.GetDatabasePath());
		}
		catch (Exception exception)
		{
			return UseCaseResult<string>.Failure(new UseCaseError("database_path_read_failed", exception.Message));
		}
	}
}
