namespace Presentation.Screens.ExpenseDocuments.List
{
	partial class ExpenseDocumentListForm
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
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			panel1 = new Panel();
			labelDate = new Label();
			buttonNewDocument = new Button();
			labelSum = new Label();
			label1 = new Label();
			panel2 = new Panel();
			dataGridView1 = new DataGridView();
			Date = new DataGridViewTextBoxColumn();
			Column3Sum = new DataGridViewTextBoxColumn();
			ColumnCurrencyCode = new DataGridViewTextBoxColumn();
			Seller = new DataGridViewTextBoxColumn();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = Color.FromArgb(234, 234, 224);
			panel1.Controls.Add(labelDate);
			panel1.Controls.Add(buttonNewDocument);
			panel1.Controls.Add(labelSum);
			panel1.Controls.Add(label1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(800, 86);
			panel1.TabIndex = 0;
			// 
			// labelDate
			// 
			labelDate.AutoSize = true;
			labelDate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			labelDate.ForeColor = Color.FromArgb(24, 38, 36);
			labelDate.Location = new Point(12, 9);
			labelDate.Name = "labelDate";
			labelDate.Size = new Size(22, 25);
			labelDate.TabIndex = 3;
			labelDate.Text = "0";
			// 
			// buttonNewDocument
			// 
			buttonNewDocument.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonNewDocument.BackColor = Color.FromArgb(220, 220, 220);
			buttonNewDocument.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
			buttonNewDocument.FlatStyle = FlatStyle.Flat;
			buttonNewDocument.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonNewDocument.ForeColor = Color.FromArgb(24, 38, 36);
			buttonNewDocument.Location = new Point(744, 3);
			buttonNewDocument.Name = "buttonNewDocument";
			buttonNewDocument.Size = new Size(53, 41);
			buttonNewDocument.TabIndex = 2;
			buttonNewDocument.Text = "+";
			buttonNewDocument.UseVisualStyleBackColor = false;
			buttonNewDocument.Click += buttonNewDocument_Click;
			// 
			// labelSum
			// 
			labelSum.AutoSize = true;
			labelSum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			labelSum.ForeColor = Color.FromArgb(24, 38, 36);
			labelSum.Location = new Point(172, 40);
			labelSum.Name = "labelSum";
			labelSum.Size = new Size(22, 25);
			labelSum.TabIndex = 1;
			labelSum.Text = "0";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			label1.ForeColor = Color.FromArgb(24, 38, 36);
			label1.Location = new Point(12, 40);
			label1.Name = "label1";
			label1.Size = new Size(154, 25);
			label1.TabIndex = 0;
			label1.Text = "Дневной расход";
			// 
			// panel2
			// 
			panel2.BackColor = Color.FromArgb(234, 234, 224);
			panel2.Controls.Add(dataGridView1);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(0, 86);
			panel2.Name = "panel2";
			panel2.Size = new Size(800, 364);
			panel2.TabIndex = 1;
			// 
			// dataGridView1
			// 
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.AutoGenerateColumns = false;
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
			dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Date, Column3Sum, ColumnCurrencyCode, Seller });
			dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = Color.FromArgb(234, 234, 224);
			dataGridViewCellStyle3.Font = new Font("Segoe UI", 11F);
			dataGridViewCellStyle3.ForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle3.Padding = new Padding(4, 0, 4, 0);
			dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(177, 212, 224);
			dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(24, 38, 36);
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
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
			dataGridView1.Size = new Size(800, 364);
			dataGridView1.TabIndex = 0;
			dataGridView1.KeyDown += dataGridView1_KeyDown;
			// 
			// Date
			// 
			Date.DataPropertyName = "Date";
			dataGridViewCellStyle4.Format = "dd.MM.yyyy";
			Date.DefaultCellStyle = dataGridViewCellStyle4;
			Date.HeaderText = "Date";
			Date.Name = "Date";
			Date.ReadOnly = true;
			Date.Width = 150;
			// 
			// Column3Sum
			// 
			Column3Sum.DataPropertyName = "Sum";
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			Column3Sum.DefaultCellStyle = dataGridViewCellStyle2;
			Column3Sum.HeaderText = "Sum";
			Column3Sum.Name = "Column3Sum";
			Column3Sum.ReadOnly = true;
			Column3Sum.Width = 250;
			// 
			// ColumnCurrencyCode
			//
			ColumnCurrencyCode.DataPropertyName = "CurrencyCode";
			ColumnCurrencyCode.HeaderText = "Код валюты";
			ColumnCurrencyCode.Name = "ColumnCurrencyCode";
			ColumnCurrencyCode.ReadOnly = true;
			ColumnCurrencyCode.Width = 150;
			//
			// Seller
			// 
			Seller.DataPropertyName = "Seller";
			Seller.HeaderText = "Seller";
			Seller.Name = "Seller";
			Seller.ReadOnly = true;
			Seller.Width = 150;
			// 
			// ExpenseDocumentListForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(234, 234, 224);
			ClientSize = new Size(800, 450);
			Controls.Add(panel2);
			Controls.Add(panel1);
			ForeColor = Color.FromArgb(24, 38, 36);
			Name = "ExpenseDocumentListForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Documents";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Panel panel2;
		private Label label1;
		private Label labelSum;
		private DataGridView dataGridView1;
		private Button buttonNewDocument;
		private Label labelDate;
		private DataGridViewTextBoxColumn Date;
		private DataGridViewTextBoxColumn Column3Sum;
		private DataGridViewTextBoxColumn ColumnCurrencyCode;
		private DataGridViewTextBoxColumn Seller;
	}
}
