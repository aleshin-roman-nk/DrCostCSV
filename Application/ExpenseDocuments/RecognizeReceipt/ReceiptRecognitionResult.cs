namespace Application.ExpenseDocuments.RecognizeReceipt;

public sealed class ReceiptRecognitionResult
{
	public bool IsSuccess { get; init; }
	public string? Json { get; init; }
	public string? Error { get; init; }
	public static ReceiptRecognitionResult Success(string json) => new() { IsSuccess = true, Json = json };
	public static ReceiptRecognitionResult Failure(string error) => new() { IsSuccess = false, Error = error };
}
