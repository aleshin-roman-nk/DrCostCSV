namespace Presentation.Screens.ExpenseDocuments.WithoutCurrency;

partial class WithoutCurrencyForm
{
	private System.ComponentModel.IContainer components = null;
	protected override void Dispose(bool disposing)
	{
		if (disposing) components?.Dispose();
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		labelCount = new Label();
		gridDocuments = new DataGridView();
		columnDate = new DataGridViewTextBoxColumn();
		columnSeller = new DataGridViewTextBoxColumn();
		columnItemCount = new DataGridViewTextBoxColumn();
		columnId = new DataGridViewTextBoxColumn();
		buttonOpen = new Button();
		buttonClose = new Button();
		((System.ComponentModel.ISupportInitialize)gridDocuments).BeginInit();
		SuspendLayout();
		//
		// labelCount
		//
		labelCount.AutoSize = true;
		labelCount.Location = new Point(16, 16);
		labelCount.Name = "labelCount";
		labelCount.Size = new Size(201, 19);
		labelCount.TabIndex = 0;
		labelCount.Text = "Документов без валюты: 0";
		//
		// gridDocuments
		//
		gridDocuments.AllowUserToAddRows = false;
		gridDocuments.AllowUserToDeleteRows = false;
		gridDocuments.AllowUserToResizeRows = false;
		gridDocuments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		gridDocuments.AutoGenerateColumns = false;
		gridDocuments.BackgroundColor = Color.FromArgb(234, 234, 224);
		gridDocuments.Columns.AddRange(new DataGridViewColumn[] { columnDate, columnSeller, columnItemCount, columnId });
		gridDocuments.Location = new Point(16, 49);
		gridDocuments.MultiSelect = false;
		gridDocuments.Name = "gridDocuments";
		gridDocuments.ReadOnly = true;
		gridDocuments.RowHeadersVisible = false;
		gridDocuments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gridDocuments.Size = new Size(768, 382);
		gridDocuments.TabIndex = 1;
		gridDocuments.CellDoubleClick += gridDocuments_CellDoubleClick;
		gridDocuments.KeyDown += gridDocuments_KeyDown;
		gridDocuments.SelectionChanged += gridDocuments_SelectionChanged;
		//
		// columnDate
		//
		columnDate.DataPropertyName = "Date";
		columnDate.DefaultCellStyle.Format = "dd.MM.yyyy";
		columnDate.HeaderText = "Дата";
		columnDate.Name = "columnDate";
		columnDate.ReadOnly = true;
		columnDate.Width = 130;
		//
		// columnSeller
		//
		columnSeller.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		columnSeller.DataPropertyName = "Seller";
		columnSeller.HeaderText = "Продавец";
		columnSeller.Name = "columnSeller";
		columnSeller.ReadOnly = true;
		//
		// columnItemCount
		//
		columnItemCount.DataPropertyName = "ItemCount";
		columnItemCount.HeaderText = "Позиций";
		columnItemCount.Name = "columnItemCount";
		columnItemCount.ReadOnly = true;
		columnItemCount.Width = 100;
		//
		// columnId
		//
		columnId.DataPropertyName = "Id";
		columnId.HeaderText = "№";
		columnId.Name = "columnId";
		columnId.ReadOnly = true;
		columnId.Width = 80;
		//
		// buttonOpen
		//
		buttonOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonOpen.Enabled = false;
		buttonOpen.Location = new Point(556, 446);
		buttonOpen.Name = "buttonOpen";
		buttonOpen.Size = new Size(108, 32);
		buttonOpen.TabIndex = 2;
		buttonOpen.Text = "Открыть";
		buttonOpen.UseVisualStyleBackColor = true;
		buttonOpen.Click += buttonOpen_Click;
		//
		// buttonClose
		//
		buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonClose.DialogResult = DialogResult.Cancel;
		buttonClose.Location = new Point(676, 446);
		buttonClose.Name = "buttonClose";
		buttonClose.Size = new Size(108, 32);
		buttonClose.TabIndex = 3;
		buttonClose.Text = "Закрыть";
		buttonClose.UseVisualStyleBackColor = true;
		//
		// WithoutCurrencyForm
		//
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.FromArgb(234, 234, 224);
		CancelButton = buttonClose;
		ClientSize = new Size(800, 494);
		Controls.Add(labelCount);
		Controls.Add(gridDocuments);
		Controls.Add(buttonOpen);
		Controls.Add(buttonClose);
		Font = new Font("Segoe UI", 10F);
		ForeColor = Color.FromArgb(24, 38, 36);
		MinimumSize = new Size(600, 400);
		Name = "WithoutCurrencyForm";
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Документы без валюты";
		((System.ComponentModel.ISupportInitialize)gridDocuments).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	private Label labelCount;
	private DataGridView gridDocuments;
	private DataGridViewTextBoxColumn columnDate;
	private DataGridViewTextBoxColumn columnSeller;
	private DataGridViewTextBoxColumn columnItemCount;
	private DataGridViewTextBoxColumn columnId;
	private Button buttonOpen;
	private Button buttonClose;
}
