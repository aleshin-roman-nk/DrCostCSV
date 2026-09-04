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
		builder.AppendLine("Правила:");
		builder.AppendLine("- одна товарная позиция чека — один объект;");
		builder.AppendLine("- не добавляй товары, которых нет на чеке;");
		builder.AppendLine("- Name сохраняй максимально близко к наименованию в чеке;");
		builder.AppendLine("- Price — цена одной единицы товара, не итоговая сумма позиции;");
		builder.AppendLine("- Amount — количество товара; для весового товара может быть дробным;");
		builder.AppendLine("- числа записывай как JSON-числа с точкой в качестве десятичного разделителя;");
		builder.AppendLine("- BudgetLine выбирай строго из списка допустимых значений;");
		builder.AppendLine("- BudgetTag выбирай только среди тегов выбранной BudgetLine;");
		builder.AppendLine("- если ни один тег выбранной BudgetLine не подходит, BudgetTag = null;");
		builder.AppendLine("- не создавай новые BudgetLine и BudgetTag;");
		builder.AppendLine("- если товарные позиции распознать невозможно, верни [].");

		if (!string.IsNullOrWhiteSpace(additionalInstructions))
		{
			builder.AppendLine("Дополнительные правила:");
			builder.AppendLine(additionalInstructions.Trim());
		}

		builder.AppendLine();
		builder.AppendLine("Допустимые значения в формате:");
		builder.AppendLine("BudgetLine => допустимые BudgetTag");
		foreach (var line in lines)
		{
			var lineTags = tags.Where(x => x.BudgetLineId == line.Id).Select(x => x.Name);
			builder.AppendLine($"{line.Name} => {string.Join(", ", lineTags.DefaultIfEmpty("BudgetTag = null"))}");
		}

		return builder.ToString();
	}
}
