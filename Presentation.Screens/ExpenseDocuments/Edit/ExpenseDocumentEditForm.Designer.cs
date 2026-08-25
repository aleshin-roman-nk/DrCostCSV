namespace Presentation.Screens.ExpenseDocuments.Edit
{
	partial class ExpenseDocumentEditForm
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
			buttonSave = new Button();
			buttonCancel = new Button();
			buttonEnterJSON = new Button();
			dataGridViewDocItems = new DataGridView();
			label1 = new Label();
			labelSum = new Label();
			label2 = new Label();
			textBoxSeller = new TextBox();
			dateTimePickerDate = new DateTimePicker();
			buttonAddDocumentItem = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridViewDocItems).BeginInit();
			SuspendLayout();
			// 
			// buttonSave
			// 
			buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonSave.BackColor = Color.Lime;
			buttonSave.FlatStyle = FlatStyle.Flat;
			buttonSave.Font = new Font("Segoe UI", 12F);
			buttonSave.Location = new Point(633, 573);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(74, 37);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += buttonSave_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.BackColor = Color.Red;
			buttonCancel.FlatStyle = FlatStyle.Flat;
			buttonCancel.Font = new Font("Segoe UI", 12F);
			buttonCancel.Location = new Point(713, 573);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(83, 37);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += buttonCancel_Click;
			// 
			// buttonEnterJSON
			// 
			buttonEnterJSON.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonEnterJSON.BackColor = Color.FromArgb(192, 255, 255);
			buttonEnterJSON.FlatStyle = FlatStyle.Flat;
			buttonEnterJSON.Font = new Font("Segoe UI", 12F);
			buttonEnterJSON.Location = new Point(802, 573);
			buttonEnterJSON.Name = "buttonEnterJSON";
			buttonEnterJSON.Size = new Size(89, 37);
			buttonEnterJSON.TabIndex = 2;
			buttonEnterJSON.Text = "JSON";
			buttonEnterJSON.UseVisualStyleBackColor = false;
			buttonEnterJSON.Click += buttonEnterJSON_Click;
			// 
			// dataGridViewDocItems
			// 
			dataGridViewDocItems.AllowUserToAddRows = false;
			dataGridViewDocItems.AllowUserToDeleteRows = false;
			dataGridViewDocItems.AllowUserToResizeRows = false;
			dataGridViewDocItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridViewDocItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewDocItems.Location = new Point(12, 82);
			dataGridViewDocItems.Name = "dataGridViewDocItems";
			dataGridViewDocItems.ReadOnly = true;
			dataGridViewDocItems.RowHeadersVisible = false;
			dataGridViewDocItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridViewDocItems.Size = new Size(879, 485);
			dataGridViewDocItems.TabIndex = 3;
			dataGridViewDocItems.KeyDown += dataGridViewDocItems_KeyDown;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			label1.Location = new Point(12, 573);
			label1.Name = "label1";
			label1.Size = new Size(81, 25);
			label1.TabIndex = 4;
			label1.Text = "СУММА";
			// 
			// labelSum
			// 
			labelSum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			labelSum.AutoSize = true;
			labelSum.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			labelSum.Location = new Point(99, 573);
			labelSum.Name = "labelSum";
			labelSum.Size = new Size(22, 25);
			labelSum.TabIndex = 5;
			labelSum.Text = "0";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			label2.Location = new Point(218, 12);
			label2.Name = "label2";
			label2.Size = new Size(99, 25);
			label2.TabIndex = 6;
			label2.Text = "Продавец";
			// 
			// textBoxSeller
			// 
			textBoxSeller.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			textBoxSeller.Location = new Point(323, 6);
			textBoxSeller.Name = "textBoxSeller";
			textBoxSeller.Size = new Size(271, 33);
			textBoxSeller.TabIndex = 7;
			// 
			// dateTimePickerDate
			// 
			dateTimePickerDate.CalendarFont = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			dateTimePickerDate.Location = new Point(12, 12);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new Size(200, 25);
			dateTimePickerDate.TabIndex = 8;
			// 
			// buttonAddDocumentItem
			// 
			buttonAddDocumentItem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			buttonAddDocumentItem.BackColor = Color.FromArgb(192, 255, 192);
			buttonAddDocumentItem.FlatStyle = FlatStyle.Flat;
			buttonAddDocumentItem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonAddDocumentItem.Location = new Point(860, 6);
			buttonAddDocumentItem.Name = "buttonAddDocumentItem";
			buttonAddDocumentItem.Size = new Size(31, 27);
			buttonAddDocumentItem.TabIndex = 9;
			buttonAddDocumentItem.Text = "+";
			buttonAddDocumentItem.UseVisualStyleBackColor = false;
			buttonAddDocumentItem.Click += buttonAddDocumentItem_Click;
			// 
			// ExpenseDocumentEditForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(903, 622);
			Controls.Add(buttonAddDocumentItem);
			Controls.Add(dateTimePickerDate);
			Controls.Add(textBoxSeller);
			Controls.Add(label2);
			Controls.Add(labelSum);
			Controls.Add(label1);
			Controls.Add(dataGridViewDocItems);
			Controls.Add(buttonEnterJSON);
			Controls.Add(buttonCancel);
			Controls.Add(buttonSave);
			MinimumSize = new Size(693, 507);
			Name = "ExpenseDocumentEditForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Документ";
			((System.ComponentModel.ISupportInitialize)dataGridViewDocItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonSave;
		private Button buttonCancel;
		private Button buttonEnterJSON;
		private DataGridView dataGridViewDocItems;
		private DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;
		private Label label1;
		private Label labelSum;
		private Label label2;
		private TextBox textBoxSeller;
		private DateTimePicker dateTimePickerDate;
		private Button buttonAddDocumentItem;
	}
}