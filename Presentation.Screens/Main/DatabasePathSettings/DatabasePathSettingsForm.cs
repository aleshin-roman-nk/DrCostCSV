using Presentation.Screens.Common;

namespace Presentation.Screens.Main.DatabasePathSettings;

public partial class DatabasePathSettingsForm : Form, IDatabasePathSettingsView
{
	public DatabasePathSettingsForm()
	{
		InitializeComponent();
	}

	public event Action? SaveRequested;

	public ModalResult ShowModal() => ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel;

	public string GetDatabasePath() => textBoxDatabasePath.Text;

	public void SetDatabasePath(string databasePath)
	{
		textBoxDatabasePath.Text = databasePath;
	}

	public void CloseSuccessfully() => DialogResult = DialogResult.OK;

	public void ShowError(string message)
	{
		MessageBox.Show(this, message, "Путь к базе данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	private void buttonSelectPath_Click(object sender, EventArgs e)
	{
		using var dialog = new FolderBrowserDialog
		{
			Description = "Выберите папку для файла базы данных drcost.sqlite",
			SelectedPath = Path.GetDirectoryName(textBoxDatabasePath.Text) ?? AppContext.BaseDirectory,
			ShowNewFolderButton = true
		};

		if (dialog.ShowDialog(this) == DialogResult.OK)
			textBoxDatabasePath.Text = Path.Combine(dialog.SelectedPath, "drcost.sqlite");
	}

	private void buttonSave_Click(object sender, EventArgs e) => SaveRequested?.Invoke();
}
