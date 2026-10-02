using Presentation.Screens.Common;
using Presentation.Screens.CurrencyDefaults.ViewModels;

namespace Presentation.Screens.CurrencyDefaults;

public partial class CurrencyDefaultsForm : Form, ICurrencyDefaultsView
{
	private CurrencyDefaultsViewModel? model;

	public CurrencyDefaultsForm()
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
		MessageBox.Show(this, message, "Настройка валют", MessageBoxButtons.OK, MessageBoxIcon.Error);

	public void SetModel(CurrencyDefaultsScreenModel screenModel)
	{
		model = screenModel.Settings;
		comboDocumentCurrency.DataSource = new[] { new CurrencyOptionViewModel(0, "Не указана") }
			.Concat(screenModel.Currencies).ToList();
		comboDocumentCurrency.SelectedValue = model.DocumentCurrencyId ?? 0;
		comboReportCurrency.DataSource = new[] { new CurrencyOptionViewModel(0, "Не указана") }
			.Concat(screenModel.Currencies).ToList();
		comboReportCurrency.SelectedValue = model.ReportCurrencyId ?? 0;
		columnCurrency.DataSource = screenModel.Currencies.ToList();
		gridValues.DataSource = model.CurrencyValues;
		buttonAdd.Enabled = screenModel.Currencies.Count > 0;
		buttonRemove.Enabled = SelectedRowIndex >= 0;
	}

	public bool ApplyInput()
	{
		if (model is null)
			return false;
		if (!gridValues.EndEdit())
			return false;
		BindingContext?[model.CurrencyValues]?.EndCurrentEdit();
		model.DocumentCurrencyId = comboDocumentCurrency.SelectedValue is int id && id > 0 ? id : null;
		model.ReportCurrencyId = comboReportCurrency.SelectedValue is int reportId && reportId > 0
			? reportId : null;
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
		ShowError("Не удалось прочитать значение строки. Проверьте выбранную валюту.");
	}
}
