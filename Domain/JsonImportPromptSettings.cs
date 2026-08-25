namespace Domain;

public sealed class JsonImportPromptSettings
{
	public const int SingletonId = 1;

	public int Id { get; set; } = SingletonId;
	public string AdditionalInstructions { get; set; } = string.Empty;
}
