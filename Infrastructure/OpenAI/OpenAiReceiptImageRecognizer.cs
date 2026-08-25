using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Application.ExpenseDocuments.RecognizeReceipt;

namespace Infrastructure.OpenAI;

public sealed class OpenAiReceiptImageRecognizer : IReceiptImageRecognizer
{
	private readonly HttpClient httpClient;
	public OpenAiReceiptImageRecognizer(HttpClient httpClient) => this.httpClient = httpClient;
	public async Task<ReceiptRecognitionResult> RecognizeAsync(ReceiptRecognitionRequest request, CancellationToken cancellationToken)
	{
		using var message = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/responses");
		message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", request.ApiKey);
		var image = $"data:{request.ImageMediaType};base64,{Convert.ToBase64String(request.ImageBytes)}";
		var input = new object[]
		{
			new { role = "user", content = new object[] { new { type = "input_text", text = request.Prompt }, new { type = "input_image", image_url = image, detail = "high" } } }
		};
		var body = new { model = request.Model, store = false, input };
		message.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
		using var response = await httpClient.SendAsync(message, cancellationToken);
		var text = await response.Content.ReadAsStringAsync(cancellationToken);
		if (!response.IsSuccessStatusCode) return ReceiptRecognitionResult.Failure($"OpenAI API: {(int)response.StatusCode}. {text}");
		using var document = JsonDocument.Parse(text);
		var output = document.RootElement.GetProperty("output").EnumerateArray().SelectMany(x => x.GetProperty("content").EnumerateArray()).FirstOrDefault(x => x.GetProperty("type").GetString() == "output_text");
		if (output.ValueKind == JsonValueKind.Undefined || !output.TryGetProperty("text", out var json)) return ReceiptRecognitionResult.Failure("OpenAI не вернул текст распознавания.");
		return ReceiptRecognitionResult.Success(json.GetString() ?? "[]");
	}
}
