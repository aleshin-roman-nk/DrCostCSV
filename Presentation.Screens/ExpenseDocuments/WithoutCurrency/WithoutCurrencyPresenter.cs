using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit;

namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

public sealed class WithoutCurrencyPresenter
{
	private readonly IWithoutCurrencyView view;
	private readonly WithoutCurrencyActions actions;
	private readonly ExpenseDocumentEditFlow editFlow;
	private bool hasChanges;

	public WithoutCurrencyPresenter(IWithoutCurrencyView view, WithoutCurrencyActions actions,
		ExpenseDocumentEditFlow editFlow)
	{
		this.view = view;
		this.actions = actions;
		this.editFlow = editFlow;
		view.OpenDocumentRequested += OpenDocument;
	}

	public ScreenResult Run()
	{
		var load = Load();
		if (!load.IsSuccess) return load;
		hasChanges = false;
		view.ShowModal();
		return hasChanges ? ScreenResult.Success() : ScreenResult.Cancelled();
	}

	private ScreenResult Load()
	{
		var result = actions.Load();
		if (!result.IsSuccess || result.Data is null)
			return ScreenResult.Failure(result.Error ?? "Не удалось загрузить документы без валюты.");
		view.SetDocuments(result.Data);
		return ScreenResult.Success();
	}

	private void OpenDocument(int id)
	{
		var result = editFlow.RunEdit(id);
		if (result.IsCancelled) return;
		if (!result.IsSuccess)
		{
			view.ShowError(result.Error ?? "Не удалось открыть документ.");
			return;
		}
		hasChanges = true;
		var load = Load();
		if (!load.IsSuccess)
			view.ShowError(load.Error ?? "Не удалось обновить список документов.");
	}
}
