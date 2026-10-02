namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

public interface IWithoutCurrencyView
{
	event Action<int>? OpenDocumentRequested;
	void ShowModal();
	void SetDocuments(IReadOnlyList<DocumentWithoutCurrencyViewModel> documents);
	void ShowError(string message);
}
