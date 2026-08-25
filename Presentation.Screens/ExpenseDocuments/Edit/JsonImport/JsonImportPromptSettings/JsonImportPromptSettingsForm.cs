using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

public partial class JsonImportPromptSettingsForm : Form, IJsonImportPromptSettingsView
{
	public event Action? CopyRequested;
	public event Action? AdditionalInstructionsChanged;
	public event Action? SaveRequested;
	public event Action? AddBudgetLineRequested;
	public event Action? DeleteBudgetLineRequested;
	public event Action? AddBudgetTagRequested;
	public event Action? DeleteBudgetTagRequested;

	private IReadOnlyList<BudgetTagOptionViewModel> budgetTags = Array.Empty<BudgetTagOptionViewModel>();

	public JsonImportPromptSettingsForm()
	{
		InitializeComponent();
		dataGridViewBudgetLines.SelectionChanged += (_, _) => BindBudgetTags();
	}

	public ModalResult ShowModal() => ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel;

	public void SetBudgetClassifications(IReadOnlyList<BudgetLineOptionViewModel> budgetLines, IReadOnlyList<BudgetTagOptionViewModel> budgetTags)
	{
		this.budgetTags = budgetTags;
		dataGridViewBudgetLines.DataSource = budgetLines.ToList();
		BindBudgetTags();
	}

	public string GetNewBudgetLineName() => textBoxNewBudgetLine.Text;
	public string GetNewBudgetTagName() => textBoxNewBudgetTag.Text;
	public int? GetSelectedBudgetLineId() => (dataGridViewBudgetLines.CurrentRow?.DataBoundItem as BudgetLineOptionViewModel)?.Id;
	public int? GetSelectedBudgetTagId() => (dataGridViewBudgetTags.CurrentRow?.DataBoundItem as BudgetTagOptionViewModel)?.Id;
	public void ClearBudgetLineInput() => textBoxNewBudgetLine.Clear();
	public void ClearBudgetTagInput() => textBoxNewBudgetTag.Clear();

	public void SetAdditionalInstructions(string instructions) => textBoxAdditionalInstructions.Text = instructions;
	public string GetAdditionalInstructions() => textBoxAdditionalInstructions.Text;
	public void SetPrompt(string prompt) => textBoxPrompt.Text = prompt;

	public void CopyPromptToClipboard()
	{
		Clipboard.SetText(textBoxPrompt.Text);
		labelCopyStatus.Text = "Промпт скопирован в буфер обмена.";
	}

	public void CloseSuccessfully() => DialogResult = DialogResult.OK;

	public void ShowError(string message) => MessageBox.Show(this, message, "Настройки промпта", MessageBoxButtons.OK, MessageBoxIcon.Error);
	private void buttonCopy_Click(object sender, EventArgs e) => CopyRequested?.Invoke();
	private void buttonSave_Click(object sender, EventArgs e) => SaveRequested?.Invoke();
	private void buttonAddBudgetLine_Click(object sender, EventArgs e) => AddBudgetLineRequested?.Invoke();
	private void buttonDeleteBudgetLine_Click(object sender, EventArgs e) => DeleteBudgetLineRequested?.Invoke();
	private void buttonAddBudgetTag_Click(object sender, EventArgs e) => AddBudgetTagRequested?.Invoke();
	private void buttonDeleteBudgetTag_Click(object sender, EventArgs e) => DeleteBudgetTagRequested?.Invoke();
	private void textBoxAdditionalInstructions_TextChanged(object sender, EventArgs e)
	{
		labelCopyStatus.Text = string.Empty;
		AdditionalInstructionsChanged?.Invoke();
	}

	private void BindBudgetTags()
	{
		var lineId = GetSelectedBudgetLineId();
		dataGridViewBudgetTags.DataSource = lineId.HasValue
			? budgetTags.Where(x => x.BudgetLineId == lineId.Value).ToList()
			: new List<BudgetTagOptionViewModel>();
	}
}
