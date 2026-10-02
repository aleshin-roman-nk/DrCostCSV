using Presentation.Screens.Common;
using Presentation.Screens.Currencies.List.ViewModels;

namespace Presentation.Screens.Currencies.List;

public interface ICurrencyListView
{
	event Action? AddRequested;
	event Action? EditRequested;
	event Action? DeleteRequested;
	CurrencyRowViewModel? SelectedCurrency { get; }
	void SetRows(IReadOnlyList<CurrencyRowViewModel> rows, int? selectedId);
	bool ConfirmDelete(string code);
	void ShowError(string message);
	ModalResult ShowModal();
}
