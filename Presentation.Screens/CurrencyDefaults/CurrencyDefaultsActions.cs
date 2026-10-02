using System.ComponentModel;
using System.Globalization;
using Application.Currencies.GetCurrencies;
using Application.CurrencyDefaults;
using Microsoft.Extensions.Logging;
using Presentation.Screens.Common;
using Presentation.Screens.CurrencyDefaults.ViewModels;

namespace Presentation.Screens.CurrencyDefaults;

public sealed class CurrencyDefaultsActions(IUseCaseScopedExecutor executor, ILogger<CurrencyDefaultsActions> logger)
{
	public ActionResult<CurrencyDefaultsScreenModel> Load()
	{
		try
		{
			var currencies = executor.Execute<GetCurrenciesUseCase, ActionResult<IReadOnlyList<CurrencyOptionViewModel>>>(useCase =>
			{
				var result = useCase.Execute();
				return result.IsSuccess && result.Value is not null
					? ActionResult<IReadOnlyList<CurrencyOptionViewModel>>.Success(result.Value
						.Select(currency => new CurrencyOptionViewModel(currency.Id, currency.Code)).ToList())
					: ActionResult<IReadOnlyList<CurrencyOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить валюты.");
			});
			if (!currencies.IsSuccess || currencies.Data is null)
				return ActionResult<CurrencyDefaultsScreenModel>.Failure(currencies.Error ?? "Не удалось загрузить валюты.");

			return executor.Execute<GetCurrencyDefaultsUseCase, ActionResult<CurrencyDefaultsScreenModel>>(useCase =>
			{
				var result = useCase.Execute();
				if (!result.IsSuccess || result.Value is null)
					return ActionResult<CurrencyDefaultsScreenModel>.Failure(result.Error?.Message ?? "Не удалось загрузить настройки.");
				var settings = new CurrencyDefaultsViewModel
				{
					DocumentCurrencyId = result.Value.DocumentCurrencyId,
					ReportCurrencyId = result.Value.ReportCurrencyId,
					CurrencyValues = new BindingList<CurrencyValueRowViewModel>(result.Value.CurrencyValues
						.Select(value => new CurrencyValueRowViewModel
						{
							CurrencyId = value.CurrencyId,
							Value = value.Value.ToString(CultureInfo.GetCultureInfo("ru-RU"))
						}).ToList())
				};
				return ActionResult<CurrencyDefaultsScreenModel>.Success(new(settings, currencies.Data));
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to load currency defaults");
			return ActionResult<CurrencyDefaultsScreenModel>.Failure("Не удалось загрузить настройки валют.");
		}
	}

	public ActionResult Save(CurrencyDefaultsViewModel model)
	{
		var values = new List<CurrencyValueDto>();
		for (var index = 0; index < model.CurrencyValues.Count; index++)
		{
			var row = model.CurrencyValues[index];
			if (!decimal.TryParse((row.Value ?? "").Trim().Replace(',', '.'),
				NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
				CultureInfo.InvariantCulture, out var amount) || amount <= 0)
				return ActionResult.Failure($"Строка {index + 1}: укажите положительное число, например 28,5.");
			values.Add(new(row.CurrencyId, amount));
		}

		try
		{
			return executor.Execute<SaveCurrencyDefaultsUseCase, ActionResult>(useCase =>
			{
				var result = useCase.Execute(new(model.DocumentCurrencyId, values, model.ReportCurrencyId));
				return result.IsSuccess ? ActionResult.Success()
					: ActionResult.Failure(result.Error?.Message ?? "Не удалось сохранить настройки.");
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to save currency defaults");
			return ActionResult.Failure("Не удалось сохранить настройки валют. Откройте окно заново и повторите попытку.");
		}
	}
}
