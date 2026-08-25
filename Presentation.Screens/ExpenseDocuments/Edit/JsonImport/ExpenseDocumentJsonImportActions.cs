using Application.ExpenseDocuments.RecognizeReceipt;
using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport;

public sealed class ExpenseDocumentJsonImportActions
{
	private readonly IUseCaseScopedExecutor executor;
	public ExpenseDocumentJsonImportActions(IUseCaseScopedExecutor executor) => this.executor = executor;
	public Task<ReceiptRecognitionResult> RecognizeAsync(string key, string model, byte[] image, string mediaType, CancellationToken cancellationToken) =>
		executor.ExecuteAsync<RecognizeReceiptUseCase, ReceiptRecognitionResult>(useCase => useCase.ExecuteAsync(key, model, image, mediaType, cancellationToken));
}
