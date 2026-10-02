using Presentation.Screens.Common;
using Presentation.Screens.Currencies.Edit.ViewModels;

namespace Presentation.Screens.Currencies.Edit;

public interface ICurrencyEditView
{
	event Action? SaveRequested;
	void SetModel(CurrencyEditViewModel model, bool isNew);
	CurrencyEditViewModel GetModel();
	void ShowError(string message);
	void CloseSuccessfully();
	ModalResult ShowModal();
}
