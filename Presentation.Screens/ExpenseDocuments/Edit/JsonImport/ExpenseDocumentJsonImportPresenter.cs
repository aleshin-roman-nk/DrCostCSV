using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;
using System.Text.Json;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport;

public sealed class ExpenseDocumentJsonImportPresenter
{
	private static readonly JsonSerializerOptions jsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private readonly IExpenseDocumentJsonImportView view;
	private readonly JsonImportPromptSettingsFlow promptSettingsFlow;
	private readonly ExpenseDocumentJsonImportActions actions;
	private CancellationTokenSource? recognitionCancellation;

	public ExpenseDocumentJsonImportPresenter(
		IExpenseDocumentJsonImportView view,
		JsonImportPromptSettingsFlow promptSettingsFlow, ExpenseDocumentJsonImportActions actions)
	{
		this.view = view;
		this.promptSettingsFlow = promptSettingsFlow;
		this.actions = actions;
		view.PromptSettingsRequested += View_PromptSettingsRequested;
		view.ReceiptRecognitionRequested += View_ReceiptRecognitionRequested;
		view.ReceiptRecognitionCancellationRequested += () => recognitionCancellation?.Cancel();
	}

	public ScreenResult<ExpenseDocumentFromJson> GetDocument()
	{
		if (view.ShowModal() != ModalResult.Ok)
		{
			return ScreenResult<ExpenseDocumentFromJson>
				.Cancelled();
		}

		try
		{
			var document = JsonSerializer.Deserialize<ExpenseDocumentFromJson>(
				view.GetJson(),
				jsonSerializerOptions);
			if (document is null || document.Items is null || document.Items.Any(item => item is null))
				throw new JsonException();

			return ScreenResult<ExpenseDocumentFromJson>
				.Success(document);
		}
		catch (JsonException)
		{
			return ScreenResult<ExpenseDocumentFromJson>
				.Failure("Неверный формат JSON. Ожидается объект с Date (дата YYYY-MM-DD или null) и Items (массив позиций документа).");
		}
	}

	private void View_PromptSettingsRequested()
	{
		var result = promptSettingsFlow.Run();
		if (!result.IsSuccess && !result.IsCancelled)
			throw new InvalidOperationException(result.Error ?? "Не удалось открыть настройки промпта.");
	}

	private async Task View_ReceiptRecognitionRequested()
	{
		var image = view.GetReceiptImage();
		if (image is null) { view.ShowError("Выберите фотографию чека."); return; }
		using var cancellation = new CancellationTokenSource();
		recognitionCancellation = cancellation;
		view.SetRecognitionInProgress(true);
		try
		{
			var result = await actions.RecognizeAsync(view.GetOpenAiApiKey(), view.GetSelectedModel(), image, view.GetReceiptImageMediaType(), cancellation.Token);
			if (!result.IsSuccess) { view.ShowError(result.Error ?? "Не удалось распознать чек."); return; }
			view.SetJson(result.Json ?? "{\"Date\":null,\"Items\":[]}");
		}
		catch (OperationCanceledException) { }
		finally { recognitionCancellation = null; view.SetRecognitionInProgress(false); }
	}
}
