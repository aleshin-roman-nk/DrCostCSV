namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

partial class JsonImportPromptSettingsForm
{
	private System.ComponentModel.IContainer components = null!;
	private Label labelDescription = null!;
	private GroupBox groupBoxBudgetLines = null!;
	private DataGridView dataGridViewBudgetLines = null!;
	private DataGridViewTextBoxColumn columnBudgetLine = null!;
	private TextBox textBoxNewBudgetLine = null!;
	private Button buttonAddBudgetLine = null!;
	private Button buttonDeleteBudgetLine = null!;
	private GroupBox groupBoxBudgetTags = null!;
	private DataGridView dataGridViewBudgetTags = null!;
	private DataGridViewTextBoxColumn columnBudgetTag = null!;
	private TextBox textBoxNewBudgetTag = null!;
	private Button buttonAddBudgetTag = null!;
	private Button buttonDeleteBudgetTag = null!;
	private GroupBox groupBoxInstructions = null!;
	private TextBox textBoxAdditionalInstructions = null!;
	private GroupBox groupBoxPrompt = null!;
	private TextBox textBoxPrompt = null!;
	private Button buttonCopy = null!;
	private Button buttonSave = null!;
	private Button buttonCancel = null!;
	private Label labelCopyStatus = null!;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components is not null) components.Dispose();
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		labelDescription = new Label();
		groupBoxBudgetLines = new GroupBox();
		dataGridViewBudgetLines = new DataGridView();
		columnBudgetLine = new DataGridViewTextBoxColumn();
		textBoxNewBudgetLine = new TextBox();
		buttonAddBudgetLine = new Button();
		buttonDeleteBudgetLine = new Button();
		groupBoxBudgetTags = new GroupBox();
		dataGridViewBudgetTags = new DataGridView();
		columnBudgetTag = new DataGridViewTextBoxColumn();
		textBoxNewBudgetTag = new TextBox();
		buttonAddBudgetTag = new Button();
		buttonDeleteBudgetTag = new Button();
		groupBoxInstructions = new GroupBox();
		textBoxAdditionalInstructions = new TextBox();
		groupBoxPrompt = new GroupBox();
		textBoxPrompt = new TextBox();
		buttonCopy = new Button();
		buttonSave = new Button();
		buttonCancel = new Button();
		labelCopyStatus = new Label();
		groupBoxBudgetLines.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dataGridViewBudgetLines).BeginInit();
		groupBoxBudgetTags.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dataGridViewBudgetTags).BeginInit();
		groupBoxInstructions.SuspendLayout();
		groupBoxPrompt.SuspendLayout();
		SuspendLayout();
		// labelDescription
		labelDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		labelDescription.Font = new Font("Segoe UI", 10F);
		labelDescription.Location = new Point(12, 9);
		labelDescription.Name = "labelDescription";
		labelDescription.Size = new Size(876, 40);
		labelDescription.TabIndex = 0;
		labelDescription.Text = "Выберите строку бюджета слева, чтобы увидеть и изменить её уточняющие теги. Удаление доступно, только если значение не используется в позициях документов.";
		// groupBoxBudgetLines
		groupBoxBudgetLines.Controls.Add(buttonDeleteBudgetLine);
		groupBoxBudgetLines.Controls.Add(buttonAddBudgetLine);
		groupBoxBudgetLines.Controls.Add(textBoxNewBudgetLine);
		groupBoxBudgetLines.Controls.Add(dataGridViewBudgetLines);
		groupBoxBudgetLines.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		groupBoxBudgetLines.Location = new Point(12, 53);
		groupBoxBudgetLines.Name = "groupBoxBudgetLines";
		groupBoxBudgetLines.Size = new Size(430, 253);
		groupBoxBudgetLines.TabIndex = 1;
		groupBoxBudgetLines.TabStop = false;
		groupBoxBudgetLines.Text = "Строки бюджета";
		// dataGridViewBudgetLines
		dataGridViewBudgetLines.AllowUserToAddRows = false;
		dataGridViewBudgetLines.AllowUserToDeleteRows = false;
		dataGridViewBudgetLines.AllowUserToResizeRows = false;
		dataGridViewBudgetLines.AutoGenerateColumns = false;
		dataGridViewBudgetLines.BackgroundColor = SystemColors.Window;
		dataGridViewBudgetLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewBudgetLines.Columns.AddRange(new DataGridViewColumn[] { columnBudgetLine });
		dataGridViewBudgetLines.Location = new Point(8, 22);
		dataGridViewBudgetLines.MultiSelect = false;
		dataGridViewBudgetLines.Name = "dataGridViewBudgetLines";
		dataGridViewBudgetLines.ReadOnly = true;
		dataGridViewBudgetLines.RowHeadersVisible = false;
		dataGridViewBudgetLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dataGridViewBudgetLines.Size = new Size(414, 153);
		dataGridViewBudgetLines.TabIndex = 0;
		// columnBudgetLine
		columnBudgetLine.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		columnBudgetLine.DataPropertyName = "Name";
		columnBudgetLine.HeaderText = "Название";
		columnBudgetLine.Name = "columnBudgetLine";
		columnBudgetLine.ReadOnly = true;
		// textBoxNewBudgetLine
		textBoxNewBudgetLine.Font = new Font("Segoe UI", 10F);
		textBoxNewBudgetLine.Location = new Point(8, 184);
		textBoxNewBudgetLine.MaxLength = 100;
		textBoxNewBudgetLine.Name = "textBoxNewBudgetLine";
		textBoxNewBudgetLine.PlaceholderText = "Новая строка бюджета";
		textBoxNewBudgetLine.Size = new Size(260, 25);
		textBoxNewBudgetLine.TabIndex = 1;
		// buttonAddBudgetLine
		buttonAddBudgetLine.Font = new Font("Segoe UI", 9F);
		buttonAddBudgetLine.Location = new Point(274, 183);
		buttonAddBudgetLine.Name = "buttonAddBudgetLine";
		buttonAddBudgetLine.Size = new Size(148, 28);
		buttonAddBudgetLine.TabIndex = 2;
		buttonAddBudgetLine.Text = "Добавить";
		buttonAddBudgetLine.UseVisualStyleBackColor = true;
		buttonAddBudgetLine.Click += buttonAddBudgetLine_Click;
		// buttonDeleteBudgetLine
		buttonDeleteBudgetLine.Font = new Font("Segoe UI", 9F);
		buttonDeleteBudgetLine.Location = new Point(274, 216);
		buttonDeleteBudgetLine.Name = "buttonDeleteBudgetLine";
		buttonDeleteBudgetLine.Size = new Size(148, 28);
		buttonDeleteBudgetLine.TabIndex = 3;
		buttonDeleteBudgetLine.Text = "Удалить выбранную";
		buttonDeleteBudgetLine.UseVisualStyleBackColor = true;
		buttonDeleteBudgetLine.Click += buttonDeleteBudgetLine_Click;
		// groupBoxBudgetTags
		groupBoxBudgetTags.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		groupBoxBudgetTags.Controls.Add(buttonDeleteBudgetTag);
		groupBoxBudgetTags.Controls.Add(buttonAddBudgetTag);
		groupBoxBudgetTags.Controls.Add(textBoxNewBudgetTag);
		groupBoxBudgetTags.Controls.Add(dataGridViewBudgetTags);
		groupBoxBudgetTags.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		groupBoxBudgetTags.Location = new Point(458, 53);
		groupBoxBudgetTags.Name = "groupBoxBudgetTags";
		groupBoxBudgetTags.Size = new Size(430, 253);
		groupBoxBudgetTags.TabIndex = 2;
		groupBoxBudgetTags.TabStop = false;
		groupBoxBudgetTags.Text = "Уточняющие теги выбранной строки";
		// dataGridViewBudgetTags
		dataGridViewBudgetTags.AllowUserToAddRows = false;
		dataGridViewBudgetTags.AllowUserToDeleteRows = false;
		dataGridViewBudgetTags.AllowUserToResizeRows = false;
		dataGridViewBudgetTags.AutoGenerateColumns = false;
		dataGridViewBudgetTags.BackgroundColor = SystemColors.Window;
		dataGridViewBudgetTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewBudgetTags.Columns.AddRange(new DataGridViewColumn[] { columnBudgetTag });
		dataGridViewBudgetTags.Location = new Point(8, 22);
		dataGridViewBudgetTags.MultiSelect = false;
		dataGridViewBudgetTags.Name = "dataGridViewBudgetTags";
		dataGridViewBudgetTags.ReadOnly = true;
		dataGridViewBudgetTags.RowHeadersVisible = false;
		dataGridViewBudgetTags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dataGridViewBudgetTags.Size = new Size(414, 153);
		dataGridViewBudgetTags.TabIndex = 0;
		// columnBudgetTag
		columnBudgetTag.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		columnBudgetTag.DataPropertyName = "Name";
		columnBudgetTag.HeaderText = "Название";
		columnBudgetTag.Name = "columnBudgetTag";
		columnBudgetTag.ReadOnly = true;
		// textBoxNewBudgetTag
		textBoxNewBudgetTag.Font = new Font("Segoe UI", 10F);
		textBoxNewBudgetTag.Location = new Point(8, 184);
		textBoxNewBudgetTag.MaxLength = 100;
		textBoxNewBudgetTag.Name = "textBoxNewBudgetTag";
		textBoxNewBudgetTag.PlaceholderText = "Новый уточняющий тег";
		textBoxNewBudgetTag.Size = new Size(260, 25);
		textBoxNewBudgetTag.TabIndex = 1;
		// buttonAddBudgetTag
		buttonAddBudgetTag.Font = new Font("Segoe UI", 9F);
		buttonAddBudgetTag.Location = new Point(274, 183);
		buttonAddBudgetTag.Name = "buttonAddBudgetTag";
		buttonAddBudgetTag.Size = new Size(148, 28);
		buttonAddBudgetTag.TabIndex = 2;
		buttonAddBudgetTag.Text = "Добавить";
		buttonAddBudgetTag.UseVisualStyleBackColor = true;
		buttonAddBudgetTag.Click += buttonAddBudgetTag_Click;
		// buttonDeleteBudgetTag
		buttonDeleteBudgetTag.Font = new Font("Segoe UI", 9F);
		buttonDeleteBudgetTag.Location = new Point(274, 216);
		buttonDeleteBudgetTag.Name = "buttonDeleteBudgetTag";
		buttonDeleteBudgetTag.Size = new Size(148, 28);
		buttonDeleteBudgetTag.TabIndex = 3;
		buttonDeleteBudgetTag.Text = "Удалить выбранный";
		buttonDeleteBudgetTag.UseVisualStyleBackColor = true;
		buttonDeleteBudgetTag.Click += buttonDeleteBudgetTag_Click;
		// groupBoxInstructions
		groupBoxInstructions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		groupBoxInstructions.Controls.Add(textBoxAdditionalInstructions);
		groupBoxInstructions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		groupBoxInstructions.Location = new Point(12, 316);
		groupBoxInstructions.Name = "groupBoxInstructions";
		groupBoxInstructions.Size = new Size(876, 100);
		groupBoxInstructions.TabIndex = 3;
		groupBoxInstructions.TabStop = false;
		groupBoxInstructions.Text = "Дополнительные правила для ИИ (необязательно)";
		// textBoxAdditionalInstructions
		textBoxAdditionalInstructions.Dock = DockStyle.Fill;
		textBoxAdditionalInstructions.Font = new Font("Segoe UI", 10F);
		textBoxAdditionalInstructions.Location = new Point(3, 21);
		textBoxAdditionalInstructions.MaxLength = 2000;
		textBoxAdditionalInstructions.Multiline = true;
		textBoxAdditionalInstructions.Name = "textBoxAdditionalInstructions";
		textBoxAdditionalInstructions.ScrollBars = ScrollBars.Vertical;
		textBoxAdditionalInstructions.Size = new Size(870, 76);
		textBoxAdditionalInstructions.TabIndex = 0;
		textBoxAdditionalInstructions.TextChanged += textBoxAdditionalInstructions_TextChanged;
		// groupBoxPrompt
		groupBoxPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		groupBoxPrompt.Controls.Add(textBoxPrompt);
		groupBoxPrompt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		groupBoxPrompt.Location = new Point(12, 424);
		groupBoxPrompt.Name = "groupBoxPrompt";
		groupBoxPrompt.Size = new Size(876, 265);
		groupBoxPrompt.TabIndex = 4;
		groupBoxPrompt.TabStop = false;
		groupBoxPrompt.Text = "Сгенерированный промпт";
		// textBoxPrompt
		textBoxPrompt.Dock = DockStyle.Fill;
		textBoxPrompt.Font = new Font("Consolas", 10F);
		textBoxPrompt.Location = new Point(3, 21);
		textBoxPrompt.Multiline = true;
		textBoxPrompt.Name = "textBoxPrompt";
		textBoxPrompt.ReadOnly = true;
		textBoxPrompt.ScrollBars = ScrollBars.Both;
		textBoxPrompt.Size = new Size(870, 241);
		textBoxPrompt.TabIndex = 0;
		textBoxPrompt.WordWrap = false;
		// buttons and status
		buttonCopy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		buttonCopy.Font = new Font("Segoe UI", 10F);
		buttonCopy.Location = new Point(12, 702);
		buttonCopy.Name = "buttonCopy";
		buttonCopy.Size = new Size(150, 38);
		buttonCopy.TabIndex = 5;
		buttonCopy.Text = "Копировать промпт";
		buttonCopy.UseVisualStyleBackColor = true;
		buttonCopy.Click += buttonCopy_Click;
		labelCopyStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		labelCopyStatus.AutoSize = true;
		labelCopyStatus.ForeColor = Color.ForestGreen;
		labelCopyStatus.Location = new Point(174, 712);
		labelCopyStatus.Name = "labelCopyStatus";
		labelCopyStatus.Size = new Size(0, 17);
		labelCopyStatus.TabIndex = 6;
		buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonSave.BackColor = Color.LawnGreen;
		buttonSave.FlatStyle = FlatStyle.Flat;
		buttonSave.Font = new Font("Segoe UI", 10F);
		buttonSave.Location = new Point(662, 702);
		buttonSave.Name = "buttonSave";
		buttonSave.Size = new Size(109, 38);
		buttonSave.TabIndex = 7;
		buttonSave.Text = "Сохранить";
		buttonSave.UseVisualStyleBackColor = false;
		buttonSave.Click += buttonSave_Click;
		buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonCancel.DialogResult = DialogResult.Cancel;
		buttonCancel.Font = new Font("Segoe UI", 10F);
		buttonCancel.Location = new Point(777, 702);
		buttonCancel.Name = "buttonCancel";
		buttonCancel.Size = new Size(111, 38);
		buttonCancel.TabIndex = 8;
		buttonCancel.Text = "Закрыть";
		buttonCancel.UseVisualStyleBackColor = true;
		// form
		AcceptButton = buttonSave;
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		CancelButton = buttonCancel;
		ClientSize = new Size(900, 752);
		Controls.Add(buttonCancel);
		Controls.Add(buttonSave);
		Controls.Add(labelCopyStatus);
		Controls.Add(buttonCopy);
		Controls.Add(groupBoxPrompt);
		Controls.Add(groupBoxInstructions);
		Controls.Add(groupBoxBudgetTags);
		Controls.Add(groupBoxBudgetLines);
		Controls.Add(labelDescription);
		MinimumSize = new Size(720, 650);
		Name = "JsonImportPromptSettingsForm";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Настройка промпта и справочника бюджета";
		groupBoxBudgetLines.ResumeLayout(false);
		groupBoxBudgetLines.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dataGridViewBudgetLines).EndInit();
		groupBoxBudgetTags.ResumeLayout(false);
		groupBoxBudgetTags.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dataGridViewBudgetTags).EndInit();
		groupBoxInstructions.ResumeLayout(false);
		groupBoxInstructions.PerformLayout();
		groupBoxPrompt.ResumeLayout(false);
		groupBoxPrompt.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}
}
