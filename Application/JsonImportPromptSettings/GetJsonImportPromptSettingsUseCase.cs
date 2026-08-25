using Application.Common;
using Application.JsonImportPromptSettings.Abstractions;

namespace Application.JsonImportPromptSettings;

public sealed class GetJsonImportPromptSettingsUseCase
{
	private readonly IJsonImportPromptSettingsRepository repository;

	public GetJsonImportPromptSettingsUseCase(IJsonImportPromptSettingsRepository repository)
	{
		this.repository = repository;
	}

	public UseCaseResult<JsonImportPromptSettingsDto> Execute()
	{
		var settings = repository.Get();
		return UseCaseResult<JsonImportPromptSettingsDto>.Success(new JsonImportPromptSettingsDto
		{
			AdditionalInstructions = settings?.AdditionalInstructions ?? string.Empty
		});
	}
}
