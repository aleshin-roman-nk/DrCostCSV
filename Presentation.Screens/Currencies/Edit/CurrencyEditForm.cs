using Presentation.Screens.Common;
using Presentation.Screens.Currencies.Edit.ViewModels;

namespace Presentation.Screens.Currencies.Edit;

public partial class CurrencyEditForm : Form, ICurrencyEditView
{
	public CurrencyEditForm()
	{
		InitializeComponent();
		WindowIcon.Apply(this);
	}

	public event Action? SaveRequested;
	public void SetModel(CurrencyEditViewModel model, bool isNew)
	{
		Text = isNew ? "Добавление валюты" : "Редактирование валюты";
		textBoxCode.Text = model.Code;
		textBoxName.Text = model.Name;
	}
	public CurrencyEditViewModel GetModel() => new() { Code = textBoxCode.Text, Name = textBoxName.Text };
	public ModalResult ShowModal() => ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel;
	public void CloseSuccessfully() => DialogResult = DialogResult.OK;
	public void ShowError(string message) =>
		MessageBox.Show(this, message, "Валюта", MessageBoxButtons.OK, MessageBoxIcon.Error);
	private void buttonSave_Click(object? sender, EventArgs e) => SaveRequested?.Invoke();
}
