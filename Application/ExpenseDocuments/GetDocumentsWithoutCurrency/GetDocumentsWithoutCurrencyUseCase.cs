using Application.Common;
using Application.ExpenseDocuments.Abstractions;

namespace Application.ExpenseDocuments.GetDocumentsWithoutCurrency;

public sealed class GetDocumentsWithoutCurrencyUseCase(IExpenseDocumentReader reader)
{
	public UseCaseResult<IReadOnlyList<DocumentWithoutCurrencyDto>> Execute() =>
		UseCaseResult<IReadOnlyList<DocumentWithoutCurrencyDto>>.Success(reader.GetWithoutCurrency());
}
