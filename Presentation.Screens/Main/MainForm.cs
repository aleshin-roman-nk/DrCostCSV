using Presentation.Screens.Main;
using Presentation.Screens.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Presentation.Screens.Main;

public partial class MainForm : Form, IMainView
{
	BindingSource bs = new BindingSource();

	public DateTime CurrentDate
	{
		get
		{
			if (comboBoxYear.SelectedItem is not int year || comboBoxMonth.SelectedIndex < 0)
				return DateTime.Today;

			return new DateTime(year, comboBoxMonth.SelectedIndex + 1, 1);
		}
	}

	public MainForm()
	{
		InitializeComponent();

		InitializePeriodSelector();
		dataGridView1.DataSource = bs;
	}

	public event Action<DateTime>? OpenDocumentList;
	public event Action<DateTime>? DateChanged;
	public event Action? CreateDocument;
	public event Action? DatabasePathSettingsRequested;

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

	private void comboBoxPeriod_SelectedIndexChanged(object? sender, EventArgs e)
	{
		if (comboBoxMonth.SelectedIndex < 0 || comboBoxYear.SelectedItem is not int)
			return;

		DateChanged?.Invoke(CurrentDate);
	}

	private void InitializePeriodSelector()
	{
		var russianCulture = CultureInfo.GetCultureInfo("ru-RU");

		for (var month = 1; month <= 12; month++)
			comboBoxMonth.Items.Add(russianCulture.DateTimeFormat.GetMonthName(month));

		for (var year = 1900; year <= 2100; year++)
			comboBoxYear.Items.Add(year);

		comboBoxMonth.SelectedIndex = DateTime.Today.Month - 1;
		comboBoxYear.SelectedItem = DateTime.Today.Year;
	}

	private DailyExpenseRowViewModel? GetCurrentDailyExpenseRow()
	{
		return bs.Current as DailyExpenseRowViewModel;
	}

	private void buttonQuickDocumentAdd_Click(object sender, EventArgs e)
	{
		CreateDocument?.Invoke();
	}

	private void databasePathToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DatabasePathSettingsRequested?.Invoke();
	}

	public void ShowMsg(string msg)
	{
		MessageBox.Show(msg);
	}

}
