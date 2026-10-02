using Application.ExpenseDocuments.GetDocumentsWithoutCurrency;
using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

public sealed class WithoutCurrencyActions(IUseCaseScopedExecutor useCaseScopedExecutor)
{
	public ActionResult<IReadOnlyList<DocumentWithoutCurrencyViewModel>> Load() =>
		useCaseScopedExecutor.Execute<GetDocumentsWithoutCurrencyUseCase,
			ActionResult<IReadOnlyList<DocumentWithoutCurrencyViewModel>>>(useCase =>
		{
			var result = useCase.Execute();
			if (!result.IsSuccess)
				return ActionResult<IReadOnlyList<DocumentWithoutCurrencyViewModel>>.Failure(
					result.Error?.Message ?? "Не удалось загрузить документы без валюты.");
			return ActionResult<IReadOnlyList<DocumentWithoutCurrencyViewModel>>.Success(
				(result.Value ?? []).Select(document => new DocumentWithoutCurrencyViewModel(
					document.Id, document.Date, document.Seller, document.ItemCount)).ToList());
		});
}
