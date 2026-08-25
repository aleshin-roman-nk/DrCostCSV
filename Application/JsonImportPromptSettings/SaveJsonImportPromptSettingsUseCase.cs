using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.JsonImportPromptSettings.Abstractions;
using PromptSettings = Domain.JsonImportPromptSettings;

namespace Application.JsonImportPromptSettings;

public sealed class SaveJsonImportPromptSettingsUseCase
{
	private readonly IJsonImportPromptSettingsRepository repository;
	private readonly IUnitOfWork unitOfWork;

	public SaveJsonImportPromptSettingsUseCase(
		IJsonImportPromptSettingsRepository repository,
		IUnitOfWork unitOfWork)
	{
		this.repository = repository;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult Execute(SaveJsonImportPromptSettingsCommand command)
	{
		var instructions = command.AdditionalInstructions.Trim();
		if (instructions.Length > 2_000)
			return UseCaseResult.Failure(new UseCaseError("additional_instructions_too_long", "Дополнительная инструкция не должна быть длиннее 2000 символов."));

		var settings = repository.Get();
		if (settings is null)
		{
			settings = new PromptSettings();
			repository.Add(settings);
		}

		settings.AdditionalInstructions = instructions;
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
