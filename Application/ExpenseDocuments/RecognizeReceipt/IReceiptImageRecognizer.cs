namespace Application.ExpenseDocuments.RecognizeReceipt;

public interface IReceiptImageRecognizer
{
	Task<ReceiptRecognitionResult> RecognizeAsync(ReceiptRecognitionRequest request, CancellationToken cancellationToken);
}
