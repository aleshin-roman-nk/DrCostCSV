namespace Presentation.Screens.CurrencyDefaults;

partial class CurrencyDefaultsForm
{
	private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing)
	{
		if (disposing)
			components?.Dispose();
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		labelDocumentCurrency = new Label();
		comboDocumentCurrency = new ComboBox();
		labelReportCurrency = new Label();
		comboReportCurrency = new ComboBox();
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
		labelDocumentCurrency.Text = "Валюта цен нового документа";
		labelDocumentCurrency.TabIndex = 0;
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
		// labelReportCurrency
		//
		labelReportCurrency.AutoSize = true;
		labelReportCurrency.Location = new Point(290, 16);
		labelReportCurrency.Name = "labelReportCurrency";
		labelReportCurrency.Text = "Валюта месячного отчета";
		labelReportCurrency.TabIndex = 8;
		//
		// comboReportCurrency
		//
		comboReportCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
		comboReportCurrency.DisplayMember = "Code";
		comboReportCurrency.ValueMember = "Id";
		comboReportCurrency.Location = new Point(290, 42);
		comboReportCurrency.Name = "comboReportCurrency";
		comboReportCurrency.Size = new Size(240, 27);
		comboReportCurrency.TabIndex = 9;
		//
		// labelDescription
		//
		labelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		labelDescription.Location = new Point(16, 84);
		labelDescription.Name = "labelDescription";
		labelDescription.Size = new Size(628, 76);
		labelDescription.Text = "Все значения в таблице — эквивалентные суммы: BYN = 1, RUB = 28,5.\r\nВ непустой таблице должна быть валюта цен документа.\r\nНастройки копируются при сохранении нового документа. Старые документы не изменяются.";
		labelDescription.TabIndex = 2;
		//
		// gridValues
		//
		gridValues.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		gridValues.Location = new Point(16, 170);
		gridValues.Name = "gridValues";
		gridValues.Size = new Size(628, 218);
		gridValues.TabIndex = 3;
		gridValues.AutoGenerateColumns = false;
		gridValues.AllowUserToAddRows = false;
		gridValues.AllowUserToDeleteRows = false;
		gridValues.AllowUserToResizeRows = false;
		gridValues.MultiSelect = false;
		gridValues.RowHeadersVisible = false;
		gridValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gridValues.BackgroundColor = Color.FromArgb(234, 234, 224);
		gridValues.Columns.AddRange(new DataGridViewColumn[] { columnCurrency, columnValue });
		gridValues.SelectionChanged += gridValues_SelectionChanged;
		gridValues.DataError += gridValues_DataError;
		//
		// columnCurrency
		//
		columnCurrency.Name = "columnCurrency";
		columnCurrency.HeaderText = "Валюта";
		columnCurrency.DataPropertyName = "CurrencyId";
		columnCurrency.DisplayMember = "Code";
		columnCurrency.ValueMember = "Id";
		columnCurrency.Width = 220;
		columnCurrency.SortMode = DataGridViewColumnSortMode.NotSortable;
		//
		// columnValue
		//
		columnValue.Name = "columnValue";
		columnValue.HeaderText = "Значение";
		columnValue.DataPropertyName = "Value";
		columnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
		buttonRemove.Location = new Point(174, 402);
		buttonRemove.Name = "buttonRemove";
		buttonRemove.Size = new Size(150, 32);
		buttonRemove.TabIndex = 5;
		buttonRemove.Text = "Удалить строку";
		buttonRemove.Enabled = false;
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
		buttonSave.Text = "Сохранить";
		buttonSave.UseVisualStyleBackColor = true;
		buttonSave.Click += buttonSave_Click;
		//
		// buttonCancel
		//
		buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonCancel.Location = new Point(546, 458);
		buttonCancel.Name = "buttonCancel";
		buttonCancel.Size = new Size(98, 32);
		buttonCancel.TabIndex = 7;
		buttonCancel.Text = "Отмена";
		buttonCancel.DialogResult = DialogResult.Cancel;
		buttonCancel.UseVisualStyleBackColor = true;
		//
		// CurrencyDefaultsForm
		//
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		Font = new Font("Segoe UI", 10F);
		BackColor = Color.FromArgb(234, 234, 224);
		ForeColor = Color.FromArgb(24, 38, 36);
		CancelButton = buttonCancel;
		ClientSize = new Size(660, 506);
		MinimumSize = new Size(676, 545);
		Controls.Add(labelDocumentCurrency);
		Controls.Add(comboDocumentCurrency);
		Controls.Add(labelReportCurrency);
		Controls.Add(comboReportCurrency);
		Controls.Add(labelDescription);
		Controls.Add(gridValues);
		Controls.Add(buttonAdd);
		Controls.Add(buttonRemove);
		Controls.Add(buttonSave);
		Controls.Add(buttonCancel);
		Name = "CurrencyDefaultsForm";
		Text = "Настройка валют";
		StartPosition = FormStartPosition.CenterParent;
		ShowInTaskbar = false;
		MinimizeBox = false;
		MaximizeBox = false;
		((System.ComponentModel.ISupportInitialize)gridValues).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	private Label labelDocumentCurrency;
	private ComboBox comboDocumentCurrency;
	private Label labelReportCurrency;
	private ComboBox comboReportCurrency;
	private Label labelDescription;
	private DataGridView gridValues;
	private DataGridViewComboBoxColumn columnCurrency;
	private DataGridViewTextBoxColumn columnValue;
	private Button buttonAdd;
	private Button buttonRemove;
	private Button buttonSave;
	private Button buttonCancel;
}
