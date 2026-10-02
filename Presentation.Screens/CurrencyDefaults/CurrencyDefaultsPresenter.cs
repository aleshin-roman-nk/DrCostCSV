using Presentation.Screens.Common;
using Presentation.Screens.CurrencyDefaults.ViewModels;

namespace Presentation.Screens.CurrencyDefaults;

public sealed class CurrencyDefaultsPresenter
{
	private readonly ICurrencyDefaultsView view;
	private readonly CurrencyDefaultsActions actions;
	private CurrencyDefaultsScreenModel? model;

	public CurrencyDefaultsPresenter(ICurrencyDefaultsView view, CurrencyDefaultsActions actions)
	{
		this.view = view;
		this.actions = actions;
		view.SaveRequested += Save;
		view.AddRequested += Add;
		view.RemoveRequested += Remove;
	}

	public ScreenResult Run()
	{
		var result = actions.Load();
		if (!result.IsSuccess || result.Data is null)
			return ScreenResult.Failure(result.Error ?? "Не удалось загрузить настройки валют.");
		model = result.Data;
		view.SetModel(model);
		return view.ShowModal() == ModalResult.Ok ? ScreenResult.Success() : ScreenResult.Cancelled();
	}

	private void Add()
	{
		if (model is null || !view.ApplyInput())
			return;
		var available = model.Currencies.FirstOrDefault(currency =>
			model.Settings.CurrencyValues.All(value => value.CurrencyId != currency.Id));
		if (available is null)
		{
			view.ShowError("Нет свободных валют. При необходимости добавьте валюту через меню «Настройки → Валюты».");
			return;
		}
		model.Settings.CurrencyValues.Add(new() { CurrencyId = available.Id });
		view.SelectRow(model.Settings.CurrencyValues.Count - 1);
	}

	private void Remove()
	{
		if (model is null || !view.ApplyInput())
			return;
		var index = view.SelectedRowIndex;
		if (index >= 0 && index < model.Settings.CurrencyValues.Count)
			model.Settings.CurrencyValues.RemoveAt(index);
	}

	private void Save()
	{
		if (model is null || !view.ApplyInput())
			return;
		var result = actions.Save(model.Settings);
		if (!result.IsSuccess)
		{
			view.ShowError(result.Error ?? "Не удалось сохранить настройки.");
			return;
		}
		view.CloseSuccessfully();
	}
}
