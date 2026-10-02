using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

public sealed class ExpenseDocumentCurrencyEditPresenter
{
	private readonly IExpenseDocumentCurrencyEditView view;
	private ExpenseDocumentCurrencyEditViewModel? model;
	private IReadOnlyList<CurrencyOptionViewModel> currencies = [];

	public ExpenseDocumentCurrencyEditPresenter(IExpenseDocumentCurrencyEditView view)
	{
		this.view = view;
		view.AddRequested += Add;
		view.RemoveRequested += Remove;
		view.SaveRequested += Save;
	}

	public ScreenResult<ExpenseDocumentCurrencyEditViewModel> Edit(
		int? currencyId,
		IReadOnlyList<ExpenseDocumentCurrencyValueViewModel> values,
		IReadOnlyList<CurrencyOptionViewModel> currencies)
	{
		this.currencies = currencies;
		model = new ExpenseDocumentCurrencyEditViewModel
		{
			CurrencyId = currencyId,
			CurrencyValues = new BindingList<ExpenseDocumentCurrencyValueViewModel>(values.Select(value => new ExpenseDocumentCurrencyValueViewModel
			{
				CurrencyId = value.CurrencyId,
				Value = value.Value
			}).ToList())
		};
		view.SetModel(model, currencies);
		return view.ShowModal() == ModalResult.Ok
			? ScreenResult<ExpenseDocumentCurrencyEditViewModel>.Success(model)
			: ScreenResult<ExpenseDocumentCurrencyEditViewModel>.Cancelled();
	}

	private void Add()
	{
		if (model is null || !view.ApplyInput()) return;
		var available = currencies.FirstOrDefault(currency =>
			model.CurrencyValues.All(value => value.CurrencyId != currency.Id));
		if (available is null)
		{
			view.ShowError("Нет свободных валют. Добавьте валюту через меню «Настройки → Валюты».");
			return;
		}
		model.CurrencyValues.Add(new ExpenseDocumentCurrencyValueViewModel { CurrencyId = available.Id });
		view.SelectRow(model.CurrencyValues.Count - 1);
	}

	private void Remove()
	{
		if (model is null || !view.ApplyInput()) return;
		var index = view.SelectedRowIndex;
		if (index >= 0 && index < model.CurrencyValues.Count)
			model.CurrencyValues.RemoveAt(index);
	}

	private void Save()
	{
		if (model is null || !view.ApplyInput()) return;
		var ids = currencies.Select(currency => currency.Id).ToHashSet();
		if (model.CurrencyId.HasValue && !ids.Contains(model.CurrencyId.Value))
		{
			view.ShowError("Выберите существующую валюту цен документа.");
			return;
		}
		var used = new HashSet<int>();
		for (var index = 0; index < model.CurrencyValues.Count; index++)
		{
			var value = model.CurrencyValues[index];
			if (!ids.Contains(value.CurrencyId) || !used.Add(value.CurrencyId))
			{
				view.ShowError($"Строка {index + 1}: выберите валюту, которая еще не используется.");
				return;
			}
			if (!decimal.TryParse((value.Value ?? "").Trim().Replace(',', '.'),
				NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
				CultureInfo.InvariantCulture, out var amount) || amount <= 0)
			{
				view.ShowError($"Строка {index + 1}: укажите положительное число, например 28,5.");
				return;
			}
		}
		if (model.CurrencyValues.Count > 0 && !used.Contains(model.CurrencyId ?? 0))
		{
			view.ShowError("Шкала должна содержать валюту цен документа.");
			return;
		}
		view.CloseSuccessfully();
	}
}
