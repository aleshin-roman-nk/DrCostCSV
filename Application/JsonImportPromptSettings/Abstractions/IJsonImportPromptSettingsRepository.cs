using PromptSettings = Domain.JsonImportPromptSettings;

namespace Application.JsonImportPromptSettings.Abstractions;

public interface IJsonImportPromptSettingsRepository
{
	PromptSettings? Get();
	void Add(PromptSettings settings);
}
