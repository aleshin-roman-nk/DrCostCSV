using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

public partial class ExpenseDocumentCurrencyEditForm : Form, IExpenseDocumentCurrencyEditView
{
	private ExpenseDocumentCurrencyEditViewModel? model;

	public ExpenseDocumentCurrencyEditForm()
	{
		InitializeComponent();
		WindowIcon.Apply(this);
	}

	public event Action? SaveRequested;
	public event Action? AddRequested;
	public event Action? RemoveRequested;
	public int SelectedRowIndex => gridValues.CurrentRow?.Index ?? -1;
	public ModalResult ShowModal() => ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel;
	public void CloseSuccessfully() => DialogResult = DialogResult.OK;
	public void ShowError(string message) =>
		MessageBox.Show(this, message, "Валюты документа", MessageBoxButtons.OK, MessageBoxIcon.Error);

	public void SetModel(ExpenseDocumentCurrencyEditViewModel model, IReadOnlyList<CurrencyOptionViewModel> currencies)
	{
		this.model = model;
		comboDocumentCurrency.DataSource = new[] { new CurrencyOptionViewModel(0, "Не указана") }
			.Concat(currencies).ToList();
		comboDocumentCurrency.SelectedValue = model.CurrencyId ?? 0;
		columnCurrency.DataSource = currencies.ToList();
		gridValues.DataSource = model.CurrencyValues;
		buttonAdd.Enabled = currencies.Count > 0;
		buttonRemove.Enabled = SelectedRowIndex >= 0;
	}

	public bool ApplyInput()
	{
		if (model is null) return false;
		if (!gridValues.EndEdit())
		{
			ShowError("Проверьте выбранную валюту в таблице.");
			return false;
		}
		BindingContext?[model.CurrencyValues]?.EndCurrentEdit();
		model.CurrencyId = comboDocumentCurrency.SelectedValue is int id && id > 0 ? id : null;
		return true;
	}

	public void SelectRow(int index)
	{
		if (index >= 0 && index < gridValues.Rows.Count)
			gridValues.CurrentCell = gridValues.Rows[index].Cells[1];
	}

	private void buttonSave_Click(object? sender, EventArgs e) => SaveRequested?.Invoke();
	private void buttonAdd_Click(object? sender, EventArgs e) => AddRequested?.Invoke();
	private void buttonRemove_Click(object? sender, EventArgs e) => RemoveRequested?.Invoke();
	private void gridValues_SelectionChanged(object? sender, EventArgs e) =>
		buttonRemove.Enabled = SelectedRowIndex >= 0;
	private void gridValues_DataError(object? sender, DataGridViewDataErrorEventArgs e)
	{
		e.ThrowException = false;
		e.Cancel = true;
		ShowError("Не удалось прочитать валюту в строке. Проверьте выбор.");
	}
}
