using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.ExpenseDocuments.List.ViewModels;
using Presentation.Screens.Main.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Presentation.Screens.ExpenseDocuments.List;

public partial class ExpenseDocumentListForm : Form, IExpenseDocumentListView
{
	BindingSource bindingSource = new BindingSource();

	private BindingList<ExpenseDocumentTitleViewModel> documentTitleRowViewModels = new();

	public ExpenseDocumentListForm()
	{
		InitializeComponent();
		WindowIcon.Apply(this);

		dataGridView1.AutoGenerateColumns = false;
		dataGridView1.ShowCellToolTips = false;
		dataGridView1.DataSource = bindingSource;
		dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
	}

	public event Action? CreateExpenseDocument;
	public event Action<int>? OpenExpenseDocument;

	public void SetDocumentTitles(IReadOnlyList<ExpenseDocumentTitleViewModel> rows)
	{
		documentTitleRowViewModels = new BindingList<ExpenseDocumentTitleViewModel>(
			rows.ToList());

		BindItems(documentTitleRowViewModels);
	}

	public void ShowModal()
	{
		ShowDialog();
	}

	private void buttonNewDocument_Click(object sender, EventArgs e)
	{
		CreateExpenseDocument?.Invoke();
	}

	private void BindItems(BindingList<ExpenseDocumentTitleViewModel> items)
	{
		bindingSource.DataSource = items;
		bindingSource.ResetBindings(false);
	}

	public void SetDailyTotal(string total) => labelSum.Text = total;

	public void SetDate(DateTime dt)
	{
		labelDate.Text = dt.ToString("dd.MM.yyyy | dddd");
	}

	public void ShowError(string msg)
	{
		MessageBox.Show(msg);
	}

	private void dataGridView1_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 &&
			dataGridView1.Rows[e.RowIndex].DataBoundItem is ExpenseDocumentTitleViewModel row)
			OpenExpenseDocument?.Invoke(row.Id);
	}

	private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Enter)
			return;

		var row = GetCurrentDocument();

		if (row == null)
			return;

		e.SuppressKeyPress = true;

		OpenExpenseDocument?.Invoke(row.Id);
	}

	private ExpenseDocumentTitleViewModel? GetCurrentDocument()
	{
		return bindingSource.Current as ExpenseDocumentTitleViewModel;
	}
}
