using Application.Currencies.GetCurrencyForEdit;
using Application.Currencies.SaveCurrency;
using Microsoft.Extensions.Logging;
using Presentation.Screens.Common;
using Presentation.Screens.Currencies.Edit.ViewModels;

namespace Presentation.Screens.Currencies.Edit;

public sealed class CurrencyEditActions(IUseCaseScopedExecutor executor, ILogger<CurrencyEditActions> logger)
{
	public ActionResult<CurrencyEditViewModel> Load(int id)
	{
		try
		{
			return executor.Execute<GetCurrencyForEditUseCase, ActionResult<CurrencyEditViewModel>>(useCase =>
			{
				var result = useCase.Execute(id);
				return result.IsSuccess && result.Value is not null
					? ActionResult<CurrencyEditViewModel>.Success(new CurrencyEditViewModel
						{ Code = result.Value.Code, Name = result.Value.Name })
					: ActionResult<CurrencyEditViewModel>.Failure(result.Error?.Message ?? "Не удалось прочитать валюту.");
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to load currency {CurrencyId}", id);
			return ActionResult<CurrencyEditViewModel>.Failure("Не удалось прочитать валюту.");
		}
	}

	public ActionResult<int> Save(int? id, CurrencyEditViewModel model)
	{
		try
		{
			return executor.Execute<SaveCurrencyUseCase, ActionResult<int>>(useCase =>
			{
				var result = useCase.Execute(new SaveCurrencyCommand(id, model.Code, model.Name));
				return result.IsSuccess ? ActionResult<int>.Success(result.Value)
					: ActionResult<int>.Failure(result.Error?.Message ?? "Не удалось сохранить валюту.");
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to save currency {CurrencyId}", id);
			return ActionResult<int>.Failure("Не удалось сохранить валюту. Проверьте, что код не занят, и повторите попытку.");
		}
	}
}
