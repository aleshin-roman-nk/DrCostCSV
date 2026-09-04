namespace Presentation.Screens.Main.DatabasePathSettings;

partial class DatabasePathSettingsForm
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
		labelDescription = new Label();
		labelDatabasePath = new Label();
		textBoxDatabasePath = new TextBox();
		buttonSelectPath = new Button();
		buttonSave = new Button();
		buttonCancel = new Button();
		SuspendLayout();
		// 
		// labelDescription
		// 
		labelDescription.AutoSize = true;
		labelDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		labelDescription.ForeColor = Color.FromArgb(24, 38, 36);
		labelDescription.Location = new Point(12, 12);
		labelDescription.Name = "labelDescription";
		labelDescription.Size = new Size(410, 19);
		labelDescription.TabIndex = 0;
		labelDescription.Text = "Укажите, где должен храниться файл базы данных drcost.sqlite.";
		// 
		// labelDatabasePath
		// 
		labelDatabasePath.AutoSize = true;
		labelDatabasePath.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		labelDatabasePath.ForeColor = Color.FromArgb(24, 38, 36);
		labelDatabasePath.Location = new Point(12, 46);
		labelDatabasePath.Name = "labelDatabasePath";
		labelDatabasePath.Size = new Size(44, 19);
		labelDatabasePath.TabIndex = 1;
		labelDatabasePath.Text = "Файл:";
		// 
		// textBoxDatabasePath
		// 
		textBoxDatabasePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		textBoxDatabasePath.BackColor = Color.FromArgb(234, 234, 224);
		textBoxDatabasePath.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		textBoxDatabasePath.ForeColor = Color.FromArgb(24, 38, 36);
		textBoxDatabasePath.Location = new Point(12, 67);
		textBoxDatabasePath.Name = "textBoxDatabasePath";
		textBoxDatabasePath.ReadOnly = true;
		textBoxDatabasePath.Size = new Size(436, 25);
		textBoxDatabasePath.TabIndex = 2;
		// 
		// buttonSelectPath
		// 
		buttonSelectPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		buttonSelectPath.BackColor = Color.FromArgb(220, 220, 220);
		buttonSelectPath.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
		buttonSelectPath.FlatStyle = FlatStyle.Flat;
		buttonSelectPath.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		buttonSelectPath.ForeColor = Color.FromArgb(24, 38, 36);
		buttonSelectPath.Location = new Point(454, 66);
		buttonSelectPath.Name = "buttonSelectPath";
		buttonSelectPath.Size = new Size(118, 25);
		buttonSelectPath.TabIndex = 3;
		buttonSelectPath.Text = "Выбрать папку...";
		buttonSelectPath.UseVisualStyleBackColor = true;
		buttonSelectPath.Click += buttonSelectPath_Click;
		// 
		// buttonSave
		// 
		buttonSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonSave.BackColor = Color.FromArgb(220, 220, 220);
		buttonSave.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
		buttonSave.FlatStyle = FlatStyle.Flat;
		buttonSave.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		buttonSave.ForeColor = Color.FromArgb(24, 38, 36);
		buttonSave.Location = new Point(404, 119);
		buttonSave.Name = "buttonSave";
		buttonSave.Size = new Size(75, 25);
		buttonSave.TabIndex = 4;
		buttonSave.Text = "Сохранить";
		buttonSave.UseVisualStyleBackColor = true;
		buttonSave.Click += buttonSave_Click;
		// 
		// buttonCancel
		// 
		buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		buttonCancel.BackColor = Color.FromArgb(220, 220, 220);
		buttonCancel.DialogResult = DialogResult.Cancel;
		buttonCancel.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
		buttonCancel.FlatStyle = FlatStyle.Flat;
		buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
		buttonCancel.ForeColor = Color.FromArgb(24, 38, 36);
		buttonCancel.Location = new Point(497, 119);
		buttonCancel.Name = "buttonCancel";
		buttonCancel.Size = new Size(75, 25);
		buttonCancel.TabIndex = 5;
		buttonCancel.Text = "Отмена";
		buttonCancel.UseVisualStyleBackColor = true;
		// 
		// DatabasePathSettingsForm
		// 
		AcceptButton = buttonSave;
		AutoScaleDimensions = new SizeF(7F, 17F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.FromArgb(234, 234, 224);
		CancelButton = buttonCancel;
		ClientSize = new Size(584, 156);
		Controls.Add(buttonCancel);
		Controls.Add(buttonSave);
		Controls.Add(buttonSelectPath);
		Controls.Add(textBoxDatabasePath);
		Controls.Add(labelDatabasePath);
		Controls.Add(labelDescription);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "DatabasePathSettingsForm";
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Путь к базе данных";
		ResumeLayout(false);
		PerformLayout();
	}

	private Label labelDescription;
	private Label labelDatabasePath;
	private TextBox textBoxDatabasePath;
	private Button buttonSelectPath;
	private Button buttonSave;
	private Button buttonCancel;
}
