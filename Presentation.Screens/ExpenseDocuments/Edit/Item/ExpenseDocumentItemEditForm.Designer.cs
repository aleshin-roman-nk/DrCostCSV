namespace Presentation.Screens.ExpenseDocuments.Edit.Item
{
	partial class ExpenseDocumentItemEditForm
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
			label1 = new Label();
			textBoxItemName = new TextBox();
			label2 = new Label();
			numericUpDownPrice = new NumericUpDown();
			numericUpDownAmount = new NumericUpDown();
			label3 = new Label();
			label4 = new Label();
			textBoxSum = new TextBox();
			label5 = new Label();
			comboBoxCategory = new ComboBox();
			label6 = new Label();
			comboBoxTag = new ComboBox();
			buttonSave = new Button();
			buttonCancel = new Button();
			((System.ComponentModel.ISupportInitialize)numericUpDownPrice).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownAmount).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 14.25F);
			label1.Location = new Point(12, 37);
			label1.Name = "label1";
			label1.Size = new Size(143, 25);
			label1.TabIndex = 0;
			label1.Text = "Наименование";
			// 
			// textBoxItemName
			// 
			textBoxItemName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxItemName.Font = new Font("Segoe UI", 14.25F);
			textBoxItemName.Location = new Point(174, 34);
			textBoxItemName.Name = "textBoxItemName";
			textBoxItemName.Size = new Size(422, 33);
			textBoxItemName.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 14.25F);
			label2.Location = new Point(12, 92);
			label2.Name = "label2";
			label2.Size = new Size(57, 25);
			label2.TabIndex = 2;
			label2.Text = "Цена";
			// 
			// numericUpDownPrice
			// 
			numericUpDownPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			numericUpDownPrice.DecimalPlaces = 2;
			numericUpDownPrice.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			numericUpDownPrice.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
			numericUpDownPrice.Location = new Point(174, 90);
			numericUpDownPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
			numericUpDownPrice.Minimum = new decimal(new int[] { 100000000, 0, 0, int.MinValue });
			numericUpDownPrice.Name = "numericUpDownPrice";
			numericUpDownPrice.Size = new Size(422, 33);
			numericUpDownPrice.TabIndex = 3;
			numericUpDownPrice.ValueChanged += numericUpDownPrice_ValueChanged;
			// 
			// numericUpDownAmount
			// 
			numericUpDownAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			numericUpDownAmount.DecimalPlaces = 2;
			numericUpDownAmount.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			numericUpDownAmount.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
			numericUpDownAmount.Location = new Point(174, 129);
			numericUpDownAmount.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
			numericUpDownAmount.Minimum = new decimal(new int[] { 100000000, 0, 0, int.MinValue });
			numericUpDownAmount.Name = "numericUpDownAmount";
			numericUpDownAmount.Size = new Size(422, 33);
			numericUpDownAmount.TabIndex = 5;
			numericUpDownAmount.ValueChanged += numericUpDownAmount_ValueChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 14.25F);
			label3.Location = new Point(12, 131);
			label3.Name = "label3";
			label3.Size = new Size(114, 25);
			label3.TabIndex = 4;
			label3.Text = "Количество";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 14.25F);
			label4.Location = new Point(12, 189);
			label4.Name = "label4";
			label4.Size = new Size(69, 25);
			label4.TabIndex = 6;
			label4.Text = "Сумма";
			// 
			// textBoxSum
			// 
			textBoxSum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxSum.Font = new Font("Segoe UI", 14.25F);
			textBoxSum.Location = new Point(174, 186);
			textBoxSum.Name = "textBoxSum";
			textBoxSum.ReadOnly = true;
			textBoxSum.Size = new Size(422, 33);
			textBoxSum.TabIndex = 7;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 14.25F);
			label5.Location = new Point(12, 263);
			label5.Name = "label5";
			label5.Size = new Size(156, 25);
			label5.TabIndex = 8;
			label5.Text = "Строка бюджета";
			// 
			// comboBoxCategory
			// 
			comboBoxCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			comboBoxCategory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			comboBoxCategory.FormattingEnabled = true;
			comboBoxCategory.Location = new Point(174, 260);
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.Size = new Size(422, 33);
			comboBoxCategory.TabIndex = 9;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 14.25F);
			label6.Location = new Point(12, 307);
			label6.Name = "label6";
			label6.Size = new Size(39, 25);
			label6.TabIndex = 10;
			label6.Text = "Тег";
			// 
			// comboBoxTag
			// 
			comboBoxTag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			comboBoxTag.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
			comboBoxTag.FormattingEnabled = true;
			comboBoxTag.Location = new Point(174, 304);
			comboBoxTag.Name = "comboBoxTag";
			comboBoxTag.Size = new Size(422, 33);
			comboBoxTag.TabIndex = 11;
			// 
			// buttonSave
			// 
			buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonSave.BackColor = Color.LawnGreen;
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.FlatStyle = FlatStyle.Flat;
			buttonSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonSave.Location = new Point(400, 367);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(106, 46);
			buttonSave.TabIndex = 12;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = false;
			// 
			// buttonCancel
			// 
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.BackColor = Color.LightCoral;
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.FlatStyle = FlatStyle.Flat;
			buttonCancel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonCancel.Location = new Point(512, 367);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(84, 46);
			buttonCancel.TabIndex = 13;
			buttonCancel.Text = "Отмена";
			buttonCancel.UseVisualStyleBackColor = false;
			// 
			// ExpenseDocumentItemEditForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(614, 439);
			Controls.Add(comboBoxTag);
			Controls.Add(label6);
			Controls.Add(buttonCancel);
			Controls.Add(buttonSave);
			Controls.Add(comboBoxCategory);
			Controls.Add(label5);
			Controls.Add(textBoxSum);
			Controls.Add(label4);
			Controls.Add(numericUpDownAmount);
			Controls.Add(label3);
			Controls.Add(numericUpDownPrice);
			Controls.Add(label2);
			Controls.Add(textBoxItemName);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Name = "ExpenseDocumentItemEditForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Пункт документа";
			((System.ComponentModel.ISupportInitialize)numericUpDownPrice).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownAmount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox textBoxItemName;
		private Label label2;
		private NumericUpDown numericUpDownPrice;
		private NumericUpDown numericUpDownAmount;
		private Label label3;
		private Label label4;
		private TextBox textBoxSum;
		private Label label5;
		private ComboBox comboBoxCategory;
		private Label label6;
		private ComboBox comboBoxTag;
		private Button buttonSave;
		private Button buttonCancel;
	}
}
