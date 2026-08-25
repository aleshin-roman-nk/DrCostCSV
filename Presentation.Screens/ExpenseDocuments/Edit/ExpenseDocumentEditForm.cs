using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.ExpenseDocuments.List.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Presentation.Screens.ExpenseDocuments.Edit;

using ExpenseRows = IReadOnlyList<ExpenseDocumentItemViewModel>;

public partial class ExpenseDocumentEditForm : Form, IExpenseDocumentEditView
{
	private ExpenseDocumentViewModel? document;

	public event Action? JsonItemsRequested;
	public event Action? SaveRequested;
	public event Action<ExpenseDocumentItemViewModel>? EditDocumentItemRequested;
	public event Action<ExpenseDocumentItemViewModel>? DeleteDocumentItemRequested;
	public event Action? NewDocumentItemRequested;

	private BindingSource bs = new();

	public ExpenseDocumentEditForm()
	{
		InitializeComponent();

		dataGridViewDocItems.ShowCellToolTips = false;
		dataGridViewDocItems.AutoGenerateColumns = false;

		ExpenseDocumentItemsGridSetup.Apply(dataGridViewDocItems);

		dataGridViewDocItems.DataSource = bs;
	}

	public ModalResult ShowModal()
	{
		var result = ShowDialog();

		return result == DialogResult.OK
			? ModalResult.Ok
			: ModalResult.Cancel;
	}

	public void SetDocument(ExpenseDocumentViewModel document)
	{
		this.document = document ?? throw new ArgumentNullException(nameof(document));

		textBoxSeller.Text = document.Seller;
		dateTimePickerDate.Value = document.Date;

		BindItems(document.Items);
	}

	public void ApplyInputToDocument()
	{
		var currentDocument = RequireDocument();

		currentDocument.Seller = textBoxSeller.Text.Trim();
		currentDocument.Date = dateTimePickerDate.Value.Date;
	}

	public void ShowError(string message)
	{
		MessageBox.Show(
			this,
			message,
			"Ошибка",
			MessageBoxButtons.OK,
			MessageBoxIcon.Error);
	}

	private void buttonSave_Click(object sender, EventArgs e)
	{
		SaveRequested?.Invoke();
	}

	private void buttonCancel_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void buttonEnterJSON_Click(object sender, EventArgs e)
	{
		JsonItemsRequested?.Invoke();
	}

	public void SetItemsList(IReadOnlyList<ExpenseDocumentItemViewModel> rows)
	{
		var currentDocument = RequireDocument();

		currentDocument.Items = new BindingList<ExpenseDocumentItemViewModel>(
			rows.ToList());

		BindItems(currentDocument.Items);
	}

	private void BindItems(BindingList<ExpenseDocumentItemViewModel> items)
	{
		bs.DataSource = items;
		bs.ResetBindings(false);

		UpdateSum();
	}

	private void UpdateSum()
	{
		if (bs.DataSource
			is not IEnumerable<ExpenseDocumentItemViewModel> items)
		{
			labelSum.Text = 0m.ToString("N2");
			return;
		}

		var sum = items.Sum(x => x.Sum);

		labelSum.Text = sum.ToString("N2");
	}

	private ExpenseDocumentViewModel RequireDocument()
	{
		return document
			?? throw new InvalidOperationException("Document was not set.");
	}

	public void CloseWithOk()
	{
		DialogResult = DialogResult.OK;
		Close();
	}

	private void dataGridViewDocItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;

			var row = GetCurrentDocumentItem();

			if (row is null)
				return;

			EditDocumentItemRequested?.Invoke(row);
		}
		else if (e.KeyCode == Keys.Delete)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;

			var row = GetCurrentDocumentItem();

			if (row is null)
				return;

			DeleteDocumentItemRequested?.Invoke(row);

			UpdateSum();
		}
	}

	private ExpenseDocumentItemViewModel? GetCurrentDocumentItem()
	{
		return bs.Current as ExpenseDocumentItemViewModel;
	}

	public void RefreshItems()
	{
		bs.ResetBindings(false);
		UpdateSum();
	}

	private void buttonAddDocumentItem_Click(object sender, EventArgs e)
	{
		NewDocumentItemRequested?.Invoke();
	}
}
