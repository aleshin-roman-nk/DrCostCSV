namespace Presentation.Screens.ExpenseDocuments.Edit.Currency;

partial class ExpenseDocumentCurrencyEditForm
{
	private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing)
	{
		if (disposing) components?.Dispose();
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		labelDocumentCurrency = new Label();
		comboDocumentCurrency = new ComboBox();
		labelDescription = new Label();
		gridValues = new DataGridView();
		columnCurrency = new DataGridViewComboBoxColumn();
		columnValue = new DataGridViewTextBoxColumn();
		buttonAdd = new Button();
		buttonRemove = new Button();
		buttonSave = new Button();
		buttonCancel = new Button();
		((System.ComponentModel.ISupportInitialize)gridValues).BeginInit();
		SuspendLayout();
		//
		// labelDocumentCurrency
		//
		labelDocumentCurrency.AutoSize = true;
		labelDocumentCurrency.Location = new Point(16, 16);
		labelDocumentCurrency.Name = "labelDocumentCurrency";
		labelDocumentCurrency.Size = new Size(224, 19);
		labelDocumentCurrency.TabIndex = 0;
		labelDocumentCurrency.Text = "Валюта цен позиций документа";
		//
		// comboDocumentCurrency
		//
		comboDocumentCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
		comboDocumentCurrency.DisplayMember = "Code";
		comboDocumentCurrency.ValueMember = "Id";
		comboDocumentCurrency.Location = new Point(16, 42);
		comboDocumentCurrency.Name = "comboDocumentCurrency";
		comboDocumentCurrency.Size = new Size(240, 27);
		comboDocumentCurrency.TabIndex = 1;
		//
		// labelDescription
		//
		labelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		labelDescription.Location = new Point(16, 84);
		labelDescription.Name = "labelDescription";
		labelDescription.Size = new Size(628, 58);
		labelDescription.TabIndex = 2;
		labelDescription.Text = "Значения валют — эквивалентные суммы, например BYN = 1; RUB = 28,5.\r\nЕсли таблица заполнена, она должна содержать валюту цен документа.";
		//
		// gridValues
		//
		gridValues.AllowUserToAddRows = false;
		gridValues.AllowUserToDeleteRows = false;
		gridValues.AllowUserToResizeRows = false;
		gridValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		gridValues.AutoGenerateColumns = false;
		gridValues.BackgroundColor = Color.FromArgb(234, 234, 224);
		gridValues.Columns.AddRange(new DataGridViewColumn[] { columnCurrency, columnValue });
		gridValues.Location = new Point(16, 150);
		gridValues.MultiSelect = false;
		gridValues.Name = "gridValues";
		gridValues.RowHeadersVisible = false;
		gridValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gridValues.Size = new Size(628, 238);
		gridValues.TabIndex = 3;
		gridValues.SelectionChanged += gridValues_SelectionChanged;
		gridValues.DataError += gridValues_DataError;
		//
		// columnCurrency
		//
		columnCurrency.DataPropertyName = "CurrencyId";
		columnCurrency.DisplayMember = "Code";
		columnCurrency.HeaderText = "Валюта";
		columnCurrency.Name = "columnCurrency";
		columnCurrency.SortMode = DataGridViewColumnSortMode.NotSortable;
		columnCurrency.ValueMember = "Id";
		columnCurrency.Width = 220;
		//
		// columnValue
		//
		columnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		columnValue.DataPropertyName = "Value";
		columnValue.HeaderText = "Значение";
		columnValue.Name = "columnValue";
		columnValue.SortMode = DataGridViewColumnSortMode.NotSortable;
		//
		// buttonAdd
		//
		buttonAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonAdd.Location = new Point(16, 402);
		buttonAdd.Name = "buttonAdd";
		buttonAdd.Size = new Size(150, 32);
		buttonAdd.TabIndex = 4;
		buttonAdd.Text = "Добавить валюту";
		buttonAdd.UseVisualStyleBackColor = true;
		buttonAdd.Click += buttonAdd_Click;
		//
		// buttonRemove
		//
		buttonRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonRemove.Enabled = false;
		buttonRemove.Location = new Point(174, 402);
		buttonRemove.Name = "buttonRemove";
		buttonRemove.Size = new Size(150, 32);
		buttonRemove.TabIndex = 5;
		buttonRemove.Text = "Удалить строку";
		buttonRemove.UseVisualStyleBackColor = true;
		buttonRemove.Click += buttonRemove_Click;
		//
		// buttonSave
		//
		buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonSave.Location = new Point(424, 458);
		buttonSave.Name = "buttonSave";
		buttonSave.Size = new Size(108, 32);
		buttonSave.TabIndex = 6;
		buttonSave.Text = "Применить";
		buttonSave.UseVisualStyleBackColor = true;
		buttonSave.Click += buttonSave_Click;
		//
		// buttonCancel
		//
		buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonCancel.DialogResult = DialogResult.Cancel;
		buttonCancel.Location = new Point(546, 458);
		buttonCancel.Name = "buttonCancel";
		buttonCancel.Size = new Size(98, 32);
		buttonCancel.TabIndex = 7;
		buttonCancel.Text = "Отмена";
		buttonCancel.UseVisualStyleBackColor = true;
		//
		// ExpenseDocumentCurrencyEditForm
		//
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.FromArgb(234, 234, 224);
		CancelButton = buttonCancel;
		ClientSize = new Size(660, 506);
		Controls.Add(labelDocumentCurrency);
		Controls.Add(comboDocumentCurrency);
		Controls.Add(labelDescription);
		Controls.Add(gridValues);
		Controls.Add(buttonAdd);
		Controls.Add(buttonRemove);
		Controls.Add(buttonSave);
		Controls.Add(buttonCancel);
		Font = new Font("Segoe UI", 10F);
		ForeColor = Color.FromArgb(24, 38, 36);
		MaximizeBox = false;
		MinimizeBox = false;
		MinimumSize = new Size(676, 545);
		Name = "ExpenseDocumentCurrencyEditForm";
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Валюты документа";
		((System.ComponentModel.ISupportInitialize)gridValues).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	private Label labelDocumentCurrency;
	private ComboBox comboDocumentCurrency;
	private Label labelDescription;
	private DataGridView gridValues;
	private DataGridViewComboBoxColumn columnCurrency;
	private DataGridViewTextBoxColumn columnValue;
	private Button buttonAdd;
	private Button buttonRemove;
	private Button buttonSave;
	private Button buttonCancel;
}
