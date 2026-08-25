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

	public ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>> GetList()
	{
		if (view.ShowModal() != ModalResult.Ok)
		{
			return ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>>
				.Cancelled();
		}

		try
		{
			var items = JsonSerializer.Deserialize<List<ExpenseDocumentItemFromJson>>(
				view.GetJson(),
				jsonSerializerOptions);

			return ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>>
				.Success(items ?? []);
		}
		catch (JsonException)
		{
			return ScreenResult<IReadOnlyList<ExpenseDocumentItemFromJson>>
				.Failure("Неверный формат JSON. Ожидается массив позиций документа.");
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
			view.SetJson(result.Json ?? "[]");
		}
		catch (OperationCanceledException) { }
		finally { recognitionCancellation = null; view.SetRecognitionInProgress(false); }
	}
}
