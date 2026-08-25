using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport;

public interface IExpenseDocumentJsonImportView
{
	event Action? PromptSettingsRequested;
	event Func<Task>? ReceiptRecognitionRequested;
	event Action? ReceiptRecognitionCancellationRequested;

	ModalResult ShowModal();

	string GetJson();
	string GetOpenAiApiKey();
	string GetSelectedModel();
	byte[]? GetReceiptImage();
	string GetReceiptImageMediaType();
	void SetJson(string json);
	void SetRecognitionInProgress(bool isInProgress);
	void ShowError(string message);
}
