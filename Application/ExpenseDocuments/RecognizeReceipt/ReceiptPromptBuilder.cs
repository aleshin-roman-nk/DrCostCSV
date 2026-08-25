using System.Text;
using Application.BudgetLines;
using Application.BudgetTags;

namespace Application.ExpenseDocuments.RecognizeReceipt;

internal static class ReceiptPromptBuilder
{
	public static string Build(IReadOnlyList<BudgetLineDto> lines, IReadOnlyList<BudgetTagDto> tags, string additionalInstructions)
	{
		var builder = new StringBuilder();
		builder.AppendLine("Распознай все товарные позиции на фотографии кассового чека.");
		builder.AppendLine("Верни только корректный JSON без Markdown, комментариев и пояснений:");
		builder.AppendLine("[{\"Name\":\"наименование товара\",\"Price\":0.00,\"Amount\":1.000,\"BudgetLine\":\"строка бюджета\",\"BudgetTag\":null}]");
		builder.AppendLine("Правила: одна товарная позиция чека — один объект; не добавляй товары, которых нет на чеке; Price — цена одной единицы; Amount может быть дробным; числа — JSON-числа с точкой; если позиции не распознаны, верни [].");
		if (!string.IsNullOrWhiteSpace(additionalInstructions)) { builder.AppendLine("Дополнительные правила:"); builder.AppendLine(additionalInstructions.Trim()); }
		builder.AppendLine("Допустимые значения в формате:"); builder.AppendLine("BudgetLine => допустимые BudgetTag");
		foreach (var line in lines) builder.AppendLine($"{line.Name} => {string.Join(", ", tags.Where(x => x.BudgetLineId == line.Id).Select(x => x.Name).DefaultIfEmpty("BudgetTag = null"))}");
		return builder.ToString();
	}
}
