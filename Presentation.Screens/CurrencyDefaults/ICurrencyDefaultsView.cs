using Presentation.Screens.Common;
using Presentation.Screens.CurrencyDefaults.ViewModels;

namespace Presentation.Screens.CurrencyDefaults;

public interface ICurrencyDefaultsView
{
	event Action? SaveRequested;
	event Action? AddRequested;
	event Action? RemoveRequested;
	int SelectedRowIndex { get; }
	void SetModel(CurrencyDefaultsScreenModel model);
	bool ApplyInput();
	void SelectRow(int index);
	void ShowError(string message);
	void CloseSuccessfully();
	ModalResult ShowModal();
}
