using Presentation.Screens.Common;
using Presentation.Screens.Currencies.Edit.ViewModels;

namespace Presentation.Screens.Currencies.Edit;

public sealed class CurrencyEditPresenter
{
	private readonly ICurrencyEditView view;
	private readonly CurrencyEditActions actions;
	private int? currencyId;
	private int? savedId;

	public CurrencyEditPresenter(ICurrencyEditView view, CurrencyEditActions actions)
	{
		this.view = view;
		this.actions = actions;
		view.SaveRequested += Save;
	}

	public ScreenResult<int> Run(int? id)
	{
		currencyId = id;
		savedId = null;
		var model = new CurrencyEditViewModel();
		if (id.HasValue)
		{
			var result = actions.Load(id.Value);
			if (!result.IsSuccess || result.Data is null)
				return ScreenResult<int>.Failure(result.Error ?? "Не удалось прочитать валюту.");
			model = result.Data;
		}

		view.SetModel(model, !id.HasValue);
		return view.ShowModal() == ModalResult.Ok && savedId.HasValue
			? ScreenResult<int>.Success(savedId.Value)
			: ScreenResult<int>.Cancelled();
	}

	private void Save()
	{
		var result = actions.Save(currencyId, view.GetModel());
		if (!result.IsSuccess)
		{
			view.ShowError(result.Error ?? "Не удалось сохранить валюту.");
			return;
		}

		savedId = result.Data;
		view.CloseSuccessfully();
	}
}
