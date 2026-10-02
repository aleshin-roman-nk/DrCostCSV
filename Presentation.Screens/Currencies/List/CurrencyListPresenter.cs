using Presentation.Screens.Common;
using Presentation.Screens.Currencies.Edit;

namespace Presentation.Screens.Currencies.List;

public sealed class CurrencyListPresenter
{
	private readonly ICurrencyListView view;
	private readonly CurrencyListActions actions;
	private readonly CurrencyEditFlow editFlow;

	public CurrencyListPresenter(ICurrencyListView view, CurrencyListActions actions, CurrencyEditFlow editFlow)
	{
		this.view = view;
		this.actions = actions;
		this.editFlow = editFlow;
		view.AddRequested += () => Edit(null);
		view.EditRequested += () =>
		{
			if (view.SelectedCurrency is { } currency)
				Edit(currency.Id);
		};
		view.DeleteRequested += Delete;
	}

	public ScreenResult Run()
	{
		var result = actions.Load();
		if (!result.IsSuccess || result.Data is null)
			return ScreenResult.Failure(result.Error ?? "Не удалось загрузить валюты.");
		view.SetRows(result.Data, null);
		view.ShowModal();
		return ScreenResult.Success();
	}

	private void Edit(int? id)
	{
		var result = editFlow.Run(id);
		if (result.IsSuccess)
			Reload(result.Data);
		else if (!result.IsCancelled)
			view.ShowError(result.Error ?? "Не удалось открыть валюту.");
	}

	private void Delete()
	{
		var currency = view.SelectedCurrency;
		if (currency is null || !view.ConfirmDelete(currency.Code))
			return;
		var result = actions.Delete(currency.Id);
		if (!result.IsSuccess)
		{
			view.ShowError(result.Error ?? "Не удалось удалить валюту.");
			return;
		}
		Reload(null);
	}

	private void Reload(int? id)
	{
		var result = actions.Load();
		if (!result.IsSuccess || result.Data is null)
		{
			view.ShowError(result.Error ?? "Не удалось загрузить валюты.");
			return;
		}
		view.SetRows(result.Data, id);
	}
}
