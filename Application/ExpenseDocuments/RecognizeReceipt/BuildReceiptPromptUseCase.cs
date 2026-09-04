using Application.Common;

namespace Application.ExpenseDocuments.RecognizeReceipt;

public sealed class BuildReceiptPromptUseCase
{
	public UseCaseResult<string> Execute(BuildReceiptPromptCommand command)
	{
		var prompt = ReceiptPromptBuilder.Build(
			command.BudgetLines,
			command.BudgetTags,
			command.AdditionalInstructions);

		return UseCaseResult<string>.Success(prompt);
	}
}
