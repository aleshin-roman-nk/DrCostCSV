using Presentation.Screens.Common;

namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

public partial class WithoutCurrencyForm : Form, IWithoutCurrencyView
{
	private readonly BindingSource documentsSource = new();

	public WithoutCurrencyForm()
	{
		InitializeComponent();
		WindowIcon.Apply(this);
		gridDocuments.DataSource = documentsSource;
	}

	public event Action<int>? OpenDocumentRequested;
	public void ShowModal() => ShowDialog();
	public void ShowError(string message) =>
		MessageBox.Show(this, message, "Документы без валюты", MessageBoxButtons.OK, MessageBoxIcon.Error);

	public void SetDocuments(IReadOnlyList<DocumentWithoutCurrencyViewModel> documents)
	{
		documentsSource.DataSource = documents.ToList();
		documentsSource.ResetBindings(false);
		labelCount.Text = $"Документов без валюты: {documents.Count}";
		buttonOpen.Enabled = documentsSource.Current is DocumentWithoutCurrencyViewModel;
	}

	private void OpenSelectedDocument()
	{
		if (documentsSource.Current is DocumentWithoutCurrencyViewModel document)
			OpenDocumentRequested?.Invoke(document.Id);
	}

	private void buttonOpen_Click(object? sender, EventArgs e) => OpenSelectedDocument();
	private void gridDocuments_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0) OpenSelectedDocument();
	}
	private void gridDocuments_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Enter) return;
		e.Handled = true;
		e.SuppressKeyPress = true;
		OpenSelectedDocument();
	}
	private void gridDocuments_SelectionChanged(object? sender, EventArgs e) =>
		buttonOpen.Enabled = documentsSource.Current is DocumentWithoutCurrencyViewModel;
}
