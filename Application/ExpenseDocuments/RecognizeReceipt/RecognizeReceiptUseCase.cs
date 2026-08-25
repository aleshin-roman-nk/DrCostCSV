using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.JsonImportPromptSettings;

namespace Application.ExpenseDocuments.RecognizeReceipt;

public sealed class RecognizeReceiptUseCase
{
	private readonly IBudgetLineReader budgetLineReader;
	private readonly IBudgetTagReader budgetTagReader;
	private readonly GetJsonImportPromptSettingsUseCase settingsUseCase;
	private readonly IReceiptImageRecognizer recognizer;

	public RecognizeReceiptUseCase(IBudgetLineReader budgetLineReader, IBudgetTagReader budgetTagReader, GetJsonImportPromptSettingsUseCase settingsUseCase, IReceiptImageRecognizer recognizer)
	{
		this.budgetLineReader = budgetLineReader; this.budgetTagReader = budgetTagReader; this.settingsUseCase = settingsUseCase; this.recognizer = recognizer;
	}

	public async Task<ReceiptRecognitionResult> ExecuteAsync(string apiKey, string model, byte[] imageBytes, string imageMediaType, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(apiKey)) return ReceiptRecognitionResult.Failure("Введите API-ключ OpenAI.");
		if (imageBytes.Length == 0) return ReceiptRecognitionResult.Failure("Выберите фотографию чека.");
		var settings = settingsUseCase.Execute();
		if (!settings.IsSuccess) return ReceiptRecognitionResult.Failure(settings.Error?.Message ?? "Не удалось загрузить настройки промпта.");
		var prompt = ReceiptPromptBuilder.Build(budgetLineReader.GetAll(), budgetTagReader.GetAll(), settings.Value?.AdditionalInstructions ?? string.Empty);
		return await recognizer.RecognizeAsync(new ReceiptRecognitionRequest { ApiKey = apiKey.Trim(), Model = model, Prompt = prompt, ImageBytes = imageBytes, ImageMediaType = imageMediaType }, cancellationToken);
	}
}
