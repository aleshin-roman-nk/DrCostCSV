namespace Presentation.Screens.Main
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			dataGridView1 = new DataGridView();
			ColumnDate = new DataGridViewTextBoxColumn();
			ColumnSum = new DataGridViewTextBoxColumn();
			panel2 = new Panel();
			splitContainer1 = new SplitContainer();
			richTextBoxReport = new RichTextBox();
			menuStrip1 = new MenuStrip();
			labelPeriod = new ToolStripLabel();
			comboBoxMonth = new ToolStripComboBox();
			comboBoxYear = new ToolStripComboBox();
			settingsToolStripMenuItem = new ToolStripMenuItem();
			databasePathToolStripMenuItem = new ToolStripMenuItem();
			currenciesToolStripMenuItem = new ToolStripMenuItem();
			currencyDefaultsToolStripMenuItem = new ToolStripMenuItem();
			toolsToolStripMenuItem = new ToolStripMenuItem();
			documentsWithoutCurrencyToolStripMenuItem = new ToolStripMenuItem();
			buttonQuickDocumentAdd = new ToolStripButton();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = Color.FromArgb(234, 234, 224);
			dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(220, 220, 220);
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F);
			dataGridViewCellStyle1.ForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle1.Padding = new Padding(4, 0, 4, 0);
			dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 220, 220);
			dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ColumnDate, ColumnSum });
			dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(234, 234, 224);
			dataGridViewCellStyle4.Font = new Font("Segoe UI", 11F);
			dataGridViewCellStyle4.ForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle4.Padding = new Padding(4, 0, 4, 0);
			dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(177, 212, 224);
			dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
			dataGridView1.Dock = DockStyle.Fill;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.GridColor = Color.FromArgb(29, 198, 144);
			dataGridView1.Location = new Point(0, 0);
			dataGridView1.MultiSelect = false;
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView1.ShowCellToolTips = false;
			dataGridView1.Size = new Size(558, 566);
			dataGridView1.TabIndex = 1;
			dataGridView1.KeyDown += dataGridView1_KeyDown;
			// 
			// ColumnDate
			// 
			ColumnDate.DataPropertyName = "Date";
			dataGridViewCellStyle2.Format = "dd.MM.yyyy";
			ColumnDate.DefaultCellStyle = dataGridViewCellStyle2;
			ColumnDate.HeaderText = "Date";
			ColumnDate.Name = "ColumnDate";
			ColumnDate.ReadOnly = true;
			ColumnDate.Width = 200;
			// 
			// ColumnSum
			// 
			ColumnSum.DataPropertyName = "TotalSum";
			dataGridViewCellStyle3.Format = "N2";
			dataGridViewCellStyle3.NullValue = null;
			ColumnSum.DefaultCellStyle = dataGridViewCellStyle3;
			ColumnSum.HeaderText = "Сумма";
			ColumnSum.Name = "ColumnSum";
			ColumnSum.ReadOnly = true;
			ColumnSum.Width = 300;
			// 
			// panel2
			// 
			panel2.BackColor = Color.FromArgb(234, 234, 224);
			panel2.Controls.Add(splitContainer1);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(4, 28);
			panel2.Name = "panel2";
			panel2.Size = new Size(1116, 584);
			panel2.TabIndex = 4;
			// 
			// splitContainer1
			// 
			splitContainer1.BackColor = Color.FromArgb(220, 220, 220);
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.BackColor = Color.FromArgb(234, 234, 224);
			splitContainer1.Panel1.Controls.Add(dataGridView1);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.BackColor = Color.FromArgb(234, 234, 224);
			splitContainer1.Panel2.Controls.Add(richTextBoxReport);
			splitContainer1.Size = new Size(1116, 584);
			splitContainer1.SplitterDistance = 558;
			splitContainer1.TabIndex = 2;
			// 
			// richTextBoxReport
			// 
			richTextBoxReport.BackColor = Color.FromArgb(234, 234, 224);
			richTextBoxReport.BorderStyle = BorderStyle.FixedSingle;
			richTextBoxReport.Dock = DockStyle.Fill;
			richTextBoxReport.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			richTextBoxReport.ForeColor = Color.FromArgb(24, 38, 36);
			richTextBoxReport.Location = new Point(0, 0);
			richTextBoxReport.Name = "richTextBoxReport";
			richTextBoxReport.ReadOnly = true;
			richTextBoxReport.Size = new Size(554, 566);
			richTextBoxReport.TabIndex = 0;
			richTextBoxReport.Text = "";
			// 
			// menuStrip1
			//
			menuStrip1.BackColor = Color.FromArgb(234, 234, 224);
			menuStrip1.Items.AddRange(new ToolStripItem[] { labelPeriod, comboBoxMonth, comboBoxYear, settingsToolStripMenuItem, toolsToolStripMenuItem, buttonQuickDocumentAdd });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(1124, 24);
			menuStrip1.TabIndex = 2;
			menuStrip1.Text = "menuStrip1";
			//
			// labelPeriod
			//
			labelPeriod.Name = "labelPeriod";
			labelPeriod.Text = "Период:";
			//
			// comboBoxMonth
			//
			comboBoxMonth.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new Size(130, 24);
			comboBoxMonth.SelectedIndexChanged += comboBoxPeriod_SelectedIndexChanged;
			//
			// comboBoxYear
			//
			comboBoxYear.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new Size(75, 24);
			comboBoxYear.SelectedIndexChanged += comboBoxPeriod_SelectedIndexChanged;
			//
			// settingsToolStripMenuItem
			//
			settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { currenciesToolStripMenuItem, currencyDefaultsToolStripMenuItem, databasePathToolStripMenuItem });
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Text = "Настройки";
			//
			// currenciesToolStripMenuItem
			//
			currenciesToolStripMenuItem.Name = "currenciesToolStripMenuItem";
			currenciesToolStripMenuItem.Text = "Валюты...";
			currenciesToolStripMenuItem.Click += currenciesToolStripMenuItem_Click;
			//
			// currencyDefaultsToolStripMenuItem
			//
			currencyDefaultsToolStripMenuItem.Name = "currencyDefaultsToolStripMenuItem";
			currencyDefaultsToolStripMenuItem.Text = "Настройка валют...";
			currencyDefaultsToolStripMenuItem.Click += currencyDefaultsToolStripMenuItem_Click;
			//
			// databasePathToolStripMenuItem
			//
			databasePathToolStripMenuItem.Name = "databasePathToolStripMenuItem";
			databasePathToolStripMenuItem.Text = "Папка базы данных...";
			databasePathToolStripMenuItem.Click += databasePathToolStripMenuItem_Click;
			// toolsToolStripMenuItem
			//
			toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { documentsWithoutCurrencyToolStripMenuItem });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Text = "Инструменты";
			//
			// documentsWithoutCurrencyToolStripMenuItem
			//
			documentsWithoutCurrencyToolStripMenuItem.Name = "documentsWithoutCurrencyToolStripMenuItem";
			documentsWithoutCurrencyToolStripMenuItem.Text = "Документы без валюты...";
			documentsWithoutCurrencyToolStripMenuItem.Click += documentsWithoutCurrencyToolStripMenuItem_Click;
			//
			// buttonQuickDocumentAdd
			//
			buttonQuickDocumentAdd.Alignment = ToolStripItemAlignment.Right;
			buttonQuickDocumentAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
			buttonQuickDocumentAdd.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonQuickDocumentAdd.Name = "buttonQuickDocumentAdd";
			buttonQuickDocumentAdd.Text = "+";
			buttonQuickDocumentAdd.ToolTipText = "Добавить документ";
			buttonQuickDocumentAdd.Click += buttonQuickDocumentAdd_Click;
			//
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(234, 234, 224);
			ClientSize = new Size(1124, 616);
			Controls.Add(panel2);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "MainForm";
			Padding = new Padding(4);
			StartPosition = FormStartPosition.CenterScreen;
			Text = "RootForm";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			panel2.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private DataGridView dataGridView1;
		private Panel panel2;
		private DataGridViewTextBoxColumn ColumnDate;
		private DataGridViewTextBoxColumn ColumnSum;
		private SplitContainer splitContainer1;
		private RichTextBox richTextBoxReport;
		private MenuStrip menuStrip1;
		private ToolStripLabel labelPeriod;
		private ToolStripComboBox comboBoxMonth;
		private ToolStripComboBox comboBoxYear;
		private ToolStripMenuItem settingsToolStripMenuItem;
		private ToolStripMenuItem databasePathToolStripMenuItem;
		private ToolStripMenuItem currenciesToolStripMenuItem;
		private ToolStripMenuItem currencyDefaultsToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem documentsWithoutCurrencyToolStripMenuItem;
		private ToolStripButton buttonQuickDocumentAdd;
	}
}
