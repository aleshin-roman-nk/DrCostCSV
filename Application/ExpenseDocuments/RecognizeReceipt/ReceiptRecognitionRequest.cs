namespace Application.ExpenseDocuments.RecognizeReceipt;

public sealed class ReceiptRecognitionRequest
{
	public required string ApiKey { get; init; }
	public required string Model { get; init; }
	public required string Prompt { get; init; }
	public required byte[] ImageBytes { get; init; }
	public required string ImageMediaType { get; init; }
}
