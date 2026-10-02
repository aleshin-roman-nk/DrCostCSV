namespace Presentation.Screens.Currencies.List;

partial class CurrencyListForm
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
		gridCurrencies = new DataGridView();
		columnCode = new DataGridViewTextBoxColumn();
		columnName = new DataGridViewTextBoxColumn();
		buttonAdd = new Button();
		buttonEdit = new Button();
		buttonDelete = new Button();
		buttonClose = new Button();
		((System.ComponentModel.ISupportInitialize)gridCurrencies).BeginInit();
		SuspendLayout();
		//
		// gridCurrencies
		//
		gridCurrencies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		gridCurrencies.Location = new Point(12, 12);
		gridCurrencies.Name = "gridCurrencies";
		gridCurrencies.Size = new Size(640, 348);
		gridCurrencies.TabIndex = 0;
		gridCurrencies.AllowUserToAddRows = false;
		gridCurrencies.AllowUserToDeleteRows = false;
		gridCurrencies.AllowUserToResizeRows = false;
		gridCurrencies.AutoGenerateColumns = false;
		gridCurrencies.ReadOnly = true;
		gridCurrencies.MultiSelect = false;
		gridCurrencies.RowHeadersVisible = false;
		gridCurrencies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gridCurrencies.BackgroundColor = Color.FromArgb(234, 234, 224);
		gridCurrencies.Columns.AddRange(new DataGridViewColumn[] { columnCode, columnName });
		gridCurrencies.KeyDown += gridCurrencies_KeyDown;
		gridCurrencies.SelectionChanged += gridCurrencies_SelectionChanged;
		//
		// columnCode
		//
		columnCode.Name = "columnCode";
		columnCode.HeaderText = "Код";
		columnCode.DataPropertyName = "Code";
		columnCode.Width = 120;
		columnCode.ReadOnly = true;
		columnCode.SortMode = DataGridViewColumnSortMode.NotSortable;
		//
		// columnName
		//
		columnName.Name = "columnName";
		columnName.HeaderText = "Наименование";
		columnName.DataPropertyName = "Name";
		columnName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		columnName.ReadOnly = true;
		columnName.SortMode = DataGridViewColumnSortMode.NotSortable;
		//
		// buttonAdd
		//
		buttonAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonAdd.Location = new Point(12, 376);
		buttonAdd.Size = new Size(112, 32);
		buttonAdd.Name = "buttonAdd";
		buttonAdd.Text = "Добавить";
		buttonAdd.TabIndex = 1;
		buttonAdd.UseVisualStyleBackColor = true;
		buttonAdd.Click += buttonAdd_Click;
		//
		// buttonEdit
		//
		buttonEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonEdit.Location = new Point(132, 376);
		buttonEdit.Size = new Size(144, 32);
		buttonEdit.Name = "buttonEdit";
		buttonEdit.Text = "Редактировать";
		buttonEdit.TabIndex = 2;
		buttonEdit.Enabled = false;
		buttonEdit.UseVisualStyleBackColor = true;
		buttonEdit.Click += buttonEdit_Click;
		//
		// buttonDelete
		//
		buttonDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonDelete.Location = new Point(284, 376);
		buttonDelete.Size = new Size(112, 32);
		buttonDelete.Name = "buttonDelete";
		buttonDelete.Text = "Удалить";
		buttonDelete.TabIndex = 3;
		buttonDelete.Enabled = false;
		buttonDelete.UseVisualStyleBackColor = true;
		buttonDelete.Click += buttonDelete_Click;
		//
		// buttonClose
		//
		buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonClose.Location = new Point(540, 376);
		buttonClose.Size = new Size(112, 32);
		buttonClose.Name = "buttonClose";
		buttonClose.Text = "Закрыть";
		buttonClose.TabIndex = 4;
		buttonClose.DialogResult = DialogResult.Cancel;
		buttonClose.UseVisualStyleBackColor = true;
		//
		// CurrencyListForm
		//
		CancelButton = buttonClose;
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		Font = new Font("Segoe UI", 10F);
		BackColor = Color.FromArgb(234, 234, 224);
		ForeColor = Color.FromArgb(24, 38, 36);
		ClientSize = new Size(664, 424);
		MinimumSize = new Size(650, 350);
		Controls.Add(gridCurrencies);
		Controls.Add(buttonAdd);
		Controls.Add(buttonEdit);
		Controls.Add(buttonDelete);
		Controls.Add(buttonClose);
		MaximizeBox = false;
		MinimizeBox = false;
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Name = "CurrencyListForm";
		Text = "Справочник валют";
		((System.ComponentModel.ISupportInitialize)gridCurrencies).EndInit();
		ResumeLayout(false);
	}

	private DataGridView gridCurrencies;
	private DataGridViewTextBoxColumn columnCode;
	private DataGridViewTextBoxColumn columnName;
	private Button buttonAdd;
	private Button buttonEdit;
	private Button buttonDelete;
	private Button buttonClose;
}
