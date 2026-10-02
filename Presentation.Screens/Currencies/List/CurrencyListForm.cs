using Presentation.Screens.Common;
using Presentation.Screens.Currencies.List.ViewModels;

namespace Presentation.Screens.Currencies.List;

public partial class CurrencyListForm : Form, ICurrencyListView
{
	public CurrencyListForm()
	{
		InitializeComponent();
		WindowIcon.Apply(this);
	}

	public event Action? AddRequested;
	public event Action? EditRequested;
	public event Action? DeleteRequested;
	public CurrencyRowViewModel? SelectedCurrency => gridCurrencies.CurrentRow?.DataBoundItem as CurrencyRowViewModel;
	public ModalResult ShowModal() => ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel;
	public void ShowError(string message) =>
		MessageBox.Show(this, message, "Валюты", MessageBoxButtons.OK, MessageBoxIcon.Error);
	public bool ConfirmDelete(string code) =>
		MessageBox.Show(this, $"Удалить валюту {code}?", "Удаление валюты",
			MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

	public void SetRows(IReadOnlyList<CurrencyRowViewModel> rows, int? selectedId)
	{
		gridCurrencies.DataSource = rows.ToList();
		if (selectedId.HasValue)
		{
			foreach (DataGridViewRow row in gridCurrencies.Rows)
			{
				if (row.DataBoundItem is CurrencyRowViewModel currency && currency.Id == selectedId.Value)
				{
					gridCurrencies.CurrentCell = row.Cells[0];
					break;
				}
			}
		}
		UpdateSelection();
	}
	private void UpdateSelection()
	{
		buttonEdit.Enabled = SelectedCurrency is not null;
		buttonDelete.Enabled = SelectedCurrency is not null;
	}
	private void gridCurrencies_SelectionChanged(object? sender, EventArgs e) => UpdateSelection();
	private void buttonAdd_Click(object? sender, EventArgs e) => AddRequested?.Invoke();
	private void buttonEdit_Click(object? sender, EventArgs e) => EditRequested?.Invoke();
	private void buttonDelete_Click(object? sender, EventArgs e) => DeleteRequested?.Invoke();
	private void gridCurrencies_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			if (SelectedCurrency is not null)
				EditRequested?.Invoke();
		}
	}
}
