namespace Presentation.Screens.ExpenseDocuments.Edit
{
	partial class ExpenseDocumentJsonImportForm
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
			textBoxJsonDocument = new TextBox();
			button1 = new Button();
			buttonPromptSettings = new Button();
			textBoxOpenAiApiKey = new TextBox(); comboBoxModel = new ComboBox(); buttonSelectImage = new Button(); buttonRecognize = new Button(); buttonCancelRecognition = new Button(); labelImageName = new Label(); labelRecognitionStatus = new Label(); progressBarRecognition = new ProgressBar();
			SuspendLayout();
			// 
			// textBoxJsonDocument
			// 
			textBoxJsonDocument.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			textBoxJsonDocument.BackColor = Color.FromArgb(234, 234, 224);
			textBoxJsonDocument.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			textBoxJsonDocument.ForeColor = Color.FromArgb(24, 38, 36);
			textBoxJsonDocument.Location = new Point(12, 112);
			textBoxJsonDocument.Multiline = true;
			textBoxJsonDocument.Name = "textBoxJsonDocument";
			textBoxJsonDocument.Size = new Size(613, 337);
			textBoxJsonDocument.TabIndex = 0;
			// 
			// button1
			// 
			button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button1.BackColor = Color.FromArgb(220, 220, 220);
			button1.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
			button1.FlatStyle = FlatStyle.Flat;
			button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
			button1.ForeColor = Color.FromArgb(24, 38, 36);
			button1.Location = new Point(550, 502);
			button1.Name = "button1";
			button1.Size = new Size(75, 37);
			button1.TabIndex = 1;
			button1.Text = "OK";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// buttonPromptSettings
			// 
			buttonPromptSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			buttonPromptSettings.BackColor = Color.FromArgb(220, 220, 220);
			buttonPromptSettings.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144);
			buttonPromptSettings.FlatStyle = FlatStyle.Flat;
			buttonPromptSettings.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
			buttonPromptSettings.ForeColor = Color.FromArgb(24, 38, 36);
			buttonPromptSettings.Location = new Point(12, 461);
			buttonPromptSettings.Name = "buttonPromptSettings";
			buttonPromptSettings.Size = new Size(190, 28);
			buttonPromptSettings.TabIndex = 1;
			buttonPromptSettings.Text = "Настроить промпт для ИИ";
			buttonPromptSettings.UseVisualStyleBackColor = false;
			buttonPromptSettings.Click += buttonPromptSettings_Click;
			// API controls
			textBoxOpenAiApiKey.BackColor = Color.FromArgb(234, 234, 224); textBoxOpenAiApiKey.ForeColor = Color.FromArgb(24, 38, 36); textBoxOpenAiApiKey.Location = new Point(12, 12); textBoxOpenAiApiKey.Name = "textBoxOpenAiApiKey"; textBoxOpenAiApiKey.PasswordChar = '•'; textBoxOpenAiApiKey.PlaceholderText = "OpenAI API key (не сохраняется)"; textBoxOpenAiApiKey.Size = new Size(360, 23);
			comboBoxModel.BackColor = Color.FromArgb(234, 234, 224); comboBoxModel.DropDownStyle = ComboBoxStyle.DropDownList; comboBoxModel.ForeColor = Color.FromArgb(24, 38, 36); comboBoxModel.Items.AddRange(new object[] { "gpt-5-nano — самый дешёвый", "gpt-5.4-nano — простой", "gpt-5-mini — баланс цены и качества", "gpt-5.4-mini — сложнее", "gpt-5.6-luna — современный экономичный", "gpt-5.4 — самый сложный" }); comboBoxModel.Location = new Point(378, 12); comboBoxModel.Name = "comboBoxModel"; comboBoxModel.Size = new Size(247, 23); comboBoxModel.SelectedIndex = 0;
			buttonSelectImage.BackColor = Color.FromArgb(220, 220, 220); buttonSelectImage.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144); buttonSelectImage.FlatStyle = FlatStyle.Flat; buttonSelectImage.ForeColor = Color.FromArgb(24, 38, 36); buttonSelectImage.Location = new Point(12, 43); buttonSelectImage.Name = "buttonSelectImage"; buttonSelectImage.Size = new Size(150, 28); buttonSelectImage.Text = "Выбрать фото"; buttonSelectImage.UseVisualStyleBackColor = false; buttonSelectImage.Click += buttonSelectImage_Click;
			labelImageName.ForeColor = Color.FromArgb(24, 38, 36); labelImageName.Location = new Point(170, 48); labelImageName.Name = "labelImageName"; labelImageName.Size = new Size(250, 20); labelImageName.Text = "Файл не выбран";
			buttonRecognize.BackColor = Color.FromArgb(220, 220, 220); buttonRecognize.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144); buttonRecognize.FlatStyle = FlatStyle.Flat; buttonRecognize.ForeColor = Color.FromArgb(24, 38, 36); buttonRecognize.Location = new Point(430, 43); buttonRecognize.Name = "buttonRecognize"; buttonRecognize.Size = new Size(195, 28); buttonRecognize.Text = "Распознать через OpenAI"; buttonRecognize.UseVisualStyleBackColor = false; buttonRecognize.Click += buttonRecognize_Click;
			buttonCancelRecognition.BackColor = Color.FromArgb(220, 220, 220); buttonCancelRecognition.FlatAppearance.BorderColor = Color.FromArgb(29, 198, 144); buttonCancelRecognition.FlatStyle = FlatStyle.Flat; buttonCancelRecognition.ForeColor = Color.FromArgb(24, 38, 36); buttonCancelRecognition.Location = new Point(525, 76); buttonCancelRecognition.Name = "buttonCancelRecognition"; buttonCancelRecognition.Size = new Size(100, 24); buttonCancelRecognition.Text = "Отменить"; buttonCancelRecognition.UseVisualStyleBackColor = false; buttonCancelRecognition.Visible = false; buttonCancelRecognition.Click += buttonCancelRecognition_Click;
			progressBarRecognition.BackColor = Color.FromArgb(234, 234, 224); progressBarRecognition.ForeColor = Color.FromArgb(29, 198, 144); progressBarRecognition.Location = new Point(12, 78); progressBarRecognition.MarqueeAnimationSpeed = 30; progressBarRecognition.Name = "progressBarRecognition"; progressBarRecognition.Size = new Size(250, 16); progressBarRecognition.Style = ProgressBarStyle.Marquee; progressBarRecognition.Visible = false;
			labelRecognitionStatus.ForeColor = Color.FromArgb(24, 38, 36); labelRecognitionStatus.Location = new Point(270, 78); labelRecognitionStatus.Name = "labelRecognitionStatus"; labelRecognitionStatus.Size = new Size(250, 16);
			// 
			// EnterJsonDocumentForm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(234, 234, 224);
			ClientSize = new Size(637, 551);
			Controls.Add(buttonPromptSettings);
			Controls.Add(labelRecognitionStatus); Controls.Add(buttonCancelRecognition); Controls.Add(progressBarRecognition); Controls.Add(buttonRecognize); Controls.Add(labelImageName); Controls.Add(buttonSelectImage); Controls.Add(comboBoxModel); Controls.Add(textBoxOpenAiApiKey);
			Controls.Add(button1);
			Controls.Add(textBoxJsonDocument);
			ForeColor = Color.FromArgb(24, 38, 36);
			Name = "EnterJsonDocumentForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "EnterJsonDocumentForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBoxJsonDocument;
		private Button button1;
		private Button buttonPromptSettings;
		private TextBox textBoxOpenAiApiKey; private ComboBox comboBoxModel; private Button buttonSelectImage; private Button buttonRecognize; private Button buttonCancelRecognition; private Label labelImageName; private Label labelRecognitionStatus; private ProgressBar progressBarRecognition;
	}
}
