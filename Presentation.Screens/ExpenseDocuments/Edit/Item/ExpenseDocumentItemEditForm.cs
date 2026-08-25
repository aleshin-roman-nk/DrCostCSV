using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System.Windows.Forms;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item;

public partial class ExpenseDocumentItemEditForm : Form, IExpenseDocumentItemEditView
{
	private int? itemId;
	private IReadOnlyList<BudgetLineOptionViewModel> budgetLines = Array.Empty<BudgetLineOptionViewModel>();
	private IReadOnlyList<BudgetTagOptionViewModel> budgetTags = Array.Empty<BudgetTagOptionViewModel>();
	public ExpenseDocumentItemEditForm()
	{
		InitializeComponent();
		comboBoxCategory.DisplayMember = nameof(BudgetLineOptionViewModel.Name); comboBoxCategory.ValueMember = nameof(BudgetLineOptionViewModel.Id); comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDown;
		comboBoxCategory.SelectedValueChanged += (_, _) => BindTags((comboBoxCategory.SelectedItem as BudgetLineOptionViewModel)?.Id, null, string.Empty);
		comboBoxTag.DisplayMember = nameof(BudgetTagOptionViewModel.Name); comboBoxTag.ValueMember = nameof(BudgetTagOptionViewModel.Id); comboBoxTag.DropDownStyle = ComboBoxStyle.DropDown;
	}
	public void SetBudgetLines(IReadOnlyList<BudgetLineOptionViewModel> budgetLines) { this.budgetLines = budgetLines; comboBoxCategory.DataSource = budgetLines.ToList(); }
	public void SetBudgetTags(IReadOnlyList<BudgetTagOptionViewModel> budgetTags) => this.budgetTags = budgetTags;
	public void SetItem(ExpenseDocumentItemViewModel item)
	{
		itemId = item.Id; textBoxItemName.Text = item.Name; numericUpDownPrice.Value = item.Price; numericUpDownAmount.Value = item.Amount;
		var line = budgetLines.FirstOrDefault(x => x.Id == item.BudgetLineId); comboBoxCategory.SelectedItem = line;
		if (line is null) { comboBoxCategory.SelectedIndex = -1; comboBoxCategory.Text = item.BudgetLineName; }
		BindTags(item.BudgetLineId, item.BudgetTagId, item.BudgetTagName);
	}
	public ExpenseDocumentItemViewModel GetItem()
	{
		var line = comboBoxCategory.SelectedItem as BudgetLineOptionViewModel; var tag = comboBoxTag.SelectedItem as BudgetTagOptionViewModel;
		return new ExpenseDocumentItemViewModel { Id = itemId, Name = textBoxItemName.Text.Trim(), Price = numericUpDownPrice.Value, Amount = numericUpDownAmount.Value, BudgetLineId = line?.Id, BudgetLineName = comboBoxCategory.Text.Trim(), BudgetTagId = tag?.Id, BudgetTagName = comboBoxTag.Text.Trim() };
	}
	public ModalResult ShowModal() { DialogResult = DialogResult.None; return ShowDialog() == DialogResult.OK ? ModalResult.Ok : ModalResult.Cancel; }
	private void BindTags(int? lineId, int? tagId, string tagName)
	{
		var values = lineId.HasValue ? budgetTags.Where(x => x.BudgetLineId == lineId).ToList() : new List<BudgetTagOptionViewModel>(); comboBoxTag.DataSource = values;
		var tag = values.FirstOrDefault(x => x.Id == tagId); comboBoxTag.SelectedItem = tag;
		if (tag is null) { comboBoxTag.SelectedIndex = -1; comboBoxTag.Text = tagName; }
	}
	private void numericUpDownPrice_ValueChanged(object sender, EventArgs e) => UpdateSum(numericUpDownPrice.Value, numericUpDownAmount.Value);
	private void numericUpDownAmount_ValueChanged(object sender, EventArgs e) => UpdateSum(numericUpDownPrice.Value, numericUpDownAmount.Value);
	private void UpdateSum(decimal price, decimal amount) => textBoxSum.Text = (price * amount).ToString("0.00");
}
