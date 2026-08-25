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
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			dataGridView1 = new DataGridView();
			ColumnDate = new DataGridViewTextBoxColumn();
			ColumnSum = new DataGridViewTextBoxColumn();
			dateTimePicker1 = new DateTimePicker();
			panel1 = new Panel();
			buttonQuickDocumentAdd = new Button();
			panel2 = new Panel();
			splitContainer1 = new SplitContainer();
			richTextBoxReport = new RichTextBox();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			panel1.SuspendLayout();
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
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = SystemColors.Control;
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 14F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ColumnDate, ColumnSum });
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			dataGridView1.Dock = DockStyle.Fill;
			dataGridView1.Location = new Point(0, 0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView1.Size = new Size(511, 463);
			dataGridView1.TabIndex = 1;
			dataGridView1.KeyDown += dataGridView1_KeyDown;
			// 
			// ColumnDate
			// 
			ColumnDate.DataPropertyName = "Date";
			ColumnDate.HeaderText = "Date";
			ColumnDate.Name = "ColumnDate";
			ColumnDate.ReadOnly = true;
			ColumnDate.Width = 200;
			// 
			// ColumnSum
			// 
			ColumnSum.DataPropertyName = "TotalSum";
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			ColumnSum.DefaultCellStyle = dataGridViewCellStyle2;
			ColumnSum.HeaderText = "Sum";
			ColumnSum.Name = "ColumnSum";
			ColumnSum.ReadOnly = true;
			ColumnSum.Width = 300;
			// 
			// dateTimePicker1
			// 
			dateTimePicker1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			dateTimePicker1.Location = new Point(3, 3);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.Size = new Size(200, 35);
			dateTimePicker1.TabIndex = 0;
			dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
			// 
			// panel1
			// 
			panel1.BackColor = Color.FromArgb(192, 192, 255);
			panel1.Controls.Add(buttonQuickDocumentAdd);
			panel1.Controls.Add(dateTimePicker1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(4, 4);
			panel1.Name = "panel1";
			panel1.Size = new Size(1022, 94);
			panel1.TabIndex = 3;
			// 
			// buttonQuickDocumentAdd
			// 
			buttonQuickDocumentAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonQuickDocumentAdd.BackColor = Color.FromArgb(128, 255, 128);
			buttonQuickDocumentAdd.FlatStyle = FlatStyle.Flat;
			buttonQuickDocumentAdd.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonQuickDocumentAdd.Location = new Point(974, 3);
			buttonQuickDocumentAdd.Name = "buttonQuickDocumentAdd";
			buttonQuickDocumentAdd.Size = new Size(45, 35);
			buttonQuickDocumentAdd.TabIndex = 1;
			buttonQuickDocumentAdd.Text = "+";
			buttonQuickDocumentAdd.UseVisualStyleBackColor = false;
			buttonQuickDocumentAdd.Click += buttonQuickDocumentAdd_Click;
			// 
			// panel2
			// 
			panel2.BackColor = Color.FromArgb(192, 255, 255);
			panel2.Controls.Add(splitContainer1);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(4, 98);
			panel2.Name = "panel2";
			panel2.Size = new Size(1022, 463);
			panel2.TabIndex = 4;
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(dataGridView1);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(richTextBoxReport);
			splitContainer1.Size = new Size(1022, 463);
			splitContainer1.SplitterDistance = 511;
			splitContainer1.TabIndex = 2;
			// 
			// richTextBoxReport
			// 
			richTextBoxReport.BorderStyle = BorderStyle.FixedSingle;
			richTextBoxReport.Dock = DockStyle.Fill;
			richTextBoxReport.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			richTextBoxReport.Location = new Point(0, 0);
			richTextBoxReport.Name = "richTextBoxReport";
			richTextBoxReport.ReadOnly = true;
			richTextBoxReport.Size = new Size(507, 463);
			richTextBoxReport.TabIndex = 0;
			richTextBoxReport.Text = "";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1030, 565);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Name = "MainForm";
			Padding = new Padding(4);
			StartPosition = FormStartPosition.CenterScreen;
			Text = "RootForm";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		private DataGridView dataGridView1;
		private DateTimePicker dateTimePicker1;
		private Panel panel1;
		private Panel panel2;
		private Button buttonQuickDocumentAdd;
		private DataGridViewTextBoxColumn ColumnDate;
		private DataGridViewTextBoxColumn ColumnSum;
		private SplitContainer splitContainer1;
		private RichTextBox richTextBoxReport;
	}
}
