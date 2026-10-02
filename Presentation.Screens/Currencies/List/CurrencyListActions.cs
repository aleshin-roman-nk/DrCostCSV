using Application.Currencies.GetCurrencies;
using Application.Currencies.DeleteCurrency;
using Microsoft.Extensions.Logging;
using Presentation.Screens.Common;
using Presentation.Screens.Currencies.List.ViewModels;

namespace Presentation.Screens.Currencies.List;

public sealed class CurrencyListActions(IUseCaseScopedExecutor executor, ILogger<CurrencyListActions> logger)
{
	public ActionResult<IReadOnlyList<CurrencyRowViewModel>> Load()
	{
		try
		{
			return executor.Execute<GetCurrenciesUseCase, ActionResult<IReadOnlyList<CurrencyRowViewModel>>>(useCase =>
			{
				var result = useCase.Execute();
				return result.IsSuccess && result.Value is not null
					? ActionResult<IReadOnlyList<CurrencyRowViewModel>>.Success(result.Value
						.Select(currency => new CurrencyRowViewModel(currency.Id, currency.Code, currency.Name)).ToList())
					: ActionResult<IReadOnlyList<CurrencyRowViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить валюты.");
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to load currencies");
			return ActionResult<IReadOnlyList<CurrencyRowViewModel>>.Failure("Не удалось загрузить валюты.");
		}
	}

	public ActionResult Delete(int id)
	{
		try
		{
			return executor.Execute<DeleteCurrencyUseCase, ActionResult>(useCase =>
			{
				var result = useCase.Execute(id);
				return result.IsSuccess ? ActionResult.Success()
					: ActionResult.Failure(result.Error?.Message ?? "Не удалось удалить валюту.");
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to delete currency {CurrencyId}", id);
			return ActionResult.Failure("Не удалось удалить валюту. Обновите справочник и повторите попытку.");
		}
	}
}
