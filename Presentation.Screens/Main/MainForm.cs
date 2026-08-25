using Presentation.Screens.Main;
using Presentation.Screens.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Presentation.Screens.Main;

public partial class MainForm : Form, IMainView
{
	BindingSource bs = new BindingSource();

	public DateTime CurrentDate => dateTimePicker1.Value;

	public MainForm()
	{
		InitializeComponent();

		dataGridView1.ShowCellToolTips = false;

		dataGridView1.DataSource = bs;

		// Настраиваем только месяц и год
		dateTimePicker1.Format = DateTimePickerFormat.Custom;
		dateTimePicker1.CustomFormat = "MMMM yyyy"; // Отобразит: "Июнь 2026"

		// Отключаем выпадающий календарь, заменяя его на стрелки управления
		dateTimePicker1.ShowUpDown = true;
	}

	public event Action<DateTime>? OpenDocumentList;
	public event Action<DateTime>? DateChanged;
	public event Action? CreateDocument;

	public void SetDailyExpenses(IReadOnlyList<DailyExpenseRowViewModel> list)
	{
		bs.DataSource = new BindingList<DailyExpenseRowViewModel>(list.ToList());
		bs.ResetBindings(false);
	}

	public void SetMonthlyReport(string report)
	{
		richTextBoxReport.Text = report;
		richTextBoxReport.SelectAll();
		richTextBoxReport.SelectionFont = new Font(richTextBoxReport.Font, FontStyle.Regular);

		for (var lineIndex = 0; lineIndex < richTextBoxReport.Lines.Length; lineIndex++)
		{
			var line = richTextBoxReport.Lines[lineIndex];
			var position = richTextBoxReport.GetFirstCharIndexFromLine(lineIndex);

			if (line.StartsWith("ОБЩИЕ РАСХОДЫ ЗА МЕСЯЦ", StringComparison.Ordinal))
			{
				richTextBoxReport.Select(position, line.Length);
				richTextBoxReport.SelectionFont = new Font(richTextBoxReport.Font, FontStyle.Bold);
			}
		}

		richTextBoxReport.Select(0, 0);
	}

	private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Enter)
			return;

		var row = GetCurrentDailyExpenseRow();

		if (row == null)
			return;

		e.SuppressKeyPress = true;

		OpenDocumentList?.Invoke(row.Date);
	}

	private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
	{
		DateChanged?.Invoke(dateTimePicker1.Value);
	}

	private DailyExpenseRowViewModel? GetCurrentDailyExpenseRow()
	{
		return bs.Current as DailyExpenseRowViewModel;
	}

	private void buttonQuickDocumentAdd_Click(object sender, EventArgs e)
	{
		CreateDocument?.Invoke();
	}

	public void ShowMsg(string msg)
	{
		MessageBox.Show(msg);
	}
}
