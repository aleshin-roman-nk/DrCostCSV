using Application.BudgetLines;
using Application.BudgetTags;

namespace Application.ExpenseDocuments.RecognizeReceipt;

public sealed class BuildReceiptPromptCommand
{
	public required IReadOnlyList<BudgetLineDto> BudgetLines { get; init; }
	public required IReadOnlyList<BudgetTagDto> BudgetTags { get; init; }
	public string AdditionalInstructions { get; init; } = string.Empty;
}
