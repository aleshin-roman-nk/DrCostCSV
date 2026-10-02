namespace Presentation.Screens.Currencies.Edit;

partial class CurrencyEditForm
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
		labelCode = new Label();
		labelName = new Label();
		textBoxCode = new TextBox();
		textBoxName = new TextBox();
		buttonSave = new Button();
		buttonCancel = new Button();
		SuspendLayout();
		//
		// labelCode
		//
		labelCode.AutoSize = true;
		labelCode.Location = new Point(16, 18);
		labelCode.Name = "labelCode";
		labelCode.Text = "Код валюты";
		labelCode.TabIndex = 0;
		//
		// textBoxCode
		//
		textBoxCode.Location = new Point(16, 40);
		textBoxCode.Name = "textBoxCode";
		textBoxCode.Size = new Size(140, 25);
		textBoxCode.MaxLength = 3;
		textBoxCode.CharacterCasing = CharacterCasing.Upper;
		textBoxCode.TabIndex = 1;
		//
		// labelName
		//
		labelName.AutoSize = true;
		labelName.Location = new Point(16, 82);
		labelName.Name = "labelName";
		labelName.Text = "Наименование";
		labelName.TabIndex = 2;
		//
		// textBoxName
		//
		textBoxName.Location = new Point(16, 104);
		textBoxName.Name = "textBoxName";
		textBoxName.Size = new Size(408, 25);
		textBoxName.MaxLength = 100;
		textBoxName.TabIndex = 3;
		//
		// buttonSave
		//
		buttonSave.Location = new Point(200, 158);
		buttonSave.Name = "buttonSave";
		buttonSave.Size = new Size(112, 32);
		buttonSave.Text = "Сохранить";
		buttonSave.TabIndex = 4;
		buttonSave.UseVisualStyleBackColor = true;
		buttonSave.Click += buttonSave_Click;
		//
		// buttonCancel
		//
		buttonCancel.Location = new Point(320, 158);
		buttonCancel.Name = "buttonCancel";
		buttonCancel.Size = new Size(104, 32);
		buttonCancel.Text = "Отмена";
		buttonCancel.TabIndex = 5;
		buttonCancel.DialogResult = DialogResult.Cancel;
		buttonCancel.UseVisualStyleBackColor = true;
		//
		// CurrencyEditForm
		//
		AcceptButton = buttonSave;
		CancelButton = buttonCancel;
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		Font = new Font("Segoe UI", 10F);
		BackColor = Color.FromArgb(234, 234, 224);
		ForeColor = Color.FromArgb(24, 38, 36);
		ClientSize = new Size(440, 206);
		Controls.Add(labelCode);
		Controls.Add(textBoxCode);
		Controls.Add(labelName);
		Controls.Add(textBoxName);
		Controls.Add(buttonSave);
		Controls.Add(buttonCancel);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Name = "CurrencyEditForm";
		Text = "Валюта";
		ResumeLayout(false);
		PerformLayout();
	}

	private Label labelCode;
	private Label labelName;
	private TextBox textBoxCode;
	private TextBox textBoxName;
	private Button buttonSave;
	private Button buttonCancel;
}
