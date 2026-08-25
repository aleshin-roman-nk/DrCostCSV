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
			panel1 = new Panel();
			labelDate = new Label();
			buttonNewDocument = new Button();
			labelSum = new Label();
			label1 = new Label();
			panel2 = new Panel();
			dataGridView1 = new DataGridView();
			Date = new DataGridViewTextBoxColumn();
			Column3Sum = new DataGridViewTextBoxColumn();
			Seller = new DataGridViewTextBoxColumn();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = Color.LightSkyBlue;
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
			labelDate.Location = new Point(12, 9);
			labelDate.Name = "labelDate";
			labelDate.Size = new Size(22, 25);
			labelDate.TabIndex = 3;
			labelDate.Text = "0";
			// 
			// buttonNewDocument
			// 
			buttonNewDocument.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonNewDocument.FlatStyle = FlatStyle.Flat;
			buttonNewDocument.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonNewDocument.Location = new Point(744, 3);
			buttonNewDocument.Name = "buttonNewDocument";
			buttonNewDocument.Size = new Size(53, 41);
			buttonNewDocument.TabIndex = 2;
			buttonNewDocument.Text = "+";
			buttonNewDocument.UseVisualStyleBackColor = true;
			buttonNewDocument.Click += buttonNewDocument_Click;
			// 
			// labelSum
			// 
			labelSum.AutoSize = true;
			labelSum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
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
			label1.Location = new Point(12, 40);
			label1.Name = "label1";
			label1.Size = new Size(154, 25);
			label1.TabIndex = 0;
			label1.Text = "Дневной расход";
			// 
			// panel2
			// 
			panel2.BackColor = Color.FromArgb(192, 255, 192);
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
			dataGridView1.BackgroundColor = Color.Moccasin;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = SystemColors.Control;
			dataGridViewCellStyle1.Font = new Font("Segoe UI", 14F);
			dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Date, Column3Sum, Seller });
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
			dataGridView1.Dock = DockStyle.Fill;
			dataGridView1.GridColor = Color.Black;
			dataGridView1.Location = new Point(0, 0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView1.Size = new Size(800, 364);
			dataGridView1.TabIndex = 0;
			dataGridView1.KeyDown += dataGridView1_KeyDown;
			// 
			// Date
			// 
			Date.DataPropertyName = "Date";
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
			ClientSize = new Size(800, 450);
			Controls.Add(panel2);
			Controls.Add(panel1);
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
		private DataGridViewTextBoxColumn Seller;
	}
}