using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Presentation.Screens.ExpenseDocuments.Edit.JsonImport;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public partial class ExpenseDocumentJsonImportForm : Form, IExpenseDocumentJsonImportView
{
	public event Action? PromptSettingsRequested;
	public event Func<Task>? ReceiptRecognitionRequested;
	public event Action? ReceiptRecognitionCancellationRequested;
	private byte[]? receiptImage;
	private string receiptImageMediaType = "image/jpeg";
	public ExpenseDocumentJsonImportForm()
	{
		InitializeComponent();
	}

	public ModalResult ShowModal()
	{
		return ShowDialog() == DialogResult.OK
			? ModalResult.Ok
			: ModalResult.Cancel;
	}

	public string GetJson()
	{
		return textBoxJsonDocument.Text;
	}
	public string GetOpenAiApiKey() => textBoxOpenAiApiKey.Text;
	public string GetSelectedModel() => comboBoxModel.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
	public byte[]? GetReceiptImage() => receiptImage;
	public string GetReceiptImageMediaType() => receiptImageMediaType;
	public void SetJson(string json) => textBoxJsonDocument.Text = json;
	public void SetRecognitionInProgress(bool isInProgress)
	{
		buttonRecognize.Enabled = !isInProgress;
		buttonSelectImage.Enabled = !isInProgress;
		buttonCancelRecognition.Visible = isInProgress;
		progressBarRecognition.Visible = isInProgress;
		labelRecognitionStatus.Text = isInProgress ? "Распознаём чек…" : string.Empty;
	}
	public void ShowError(string message) => MessageBox.Show(this, message, "Распознавание чека", MessageBoxButtons.OK, MessageBoxIcon.Error);

	private void button1_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
	}

	private void buttonPromptSettings_Click(object sender, EventArgs e)
	{
		PromptSettingsRequested?.Invoke();
	}
	private void buttonSelectImage_Click(object sender, EventArgs e)
	{
		using var dialog = new OpenFileDialog { Filter = "Изображения|*.jpg;*.jpeg;*.png;*.webp" };
		if (dialog.ShowDialog(this) != DialogResult.OK) return;
		receiptImage = File.ReadAllBytes(dialog.FileName);
		receiptImageMediaType = Path.GetExtension(dialog.FileName).ToLowerInvariant() switch { ".png" => "image/png", ".webp" => "image/webp", _ => "image/jpeg" };
		labelImageName.Text = Path.GetFileName(dialog.FileName);
	}
	private async void buttonRecognize_Click(object sender, EventArgs e)
	{
		if (ReceiptRecognitionRequested is null) return;
		foreach (Func<Task> handler in ReceiptRecognitionRequested.GetInvocationList()) await handler();
	}
	private void buttonCancelRecognition_Click(object sender, EventArgs e) => ReceiptRecognitionCancellationRequested?.Invoke();
}
