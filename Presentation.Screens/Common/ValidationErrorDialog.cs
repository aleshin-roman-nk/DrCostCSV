using System.Windows.Forms;

namespace Presentation.Screens.Common;

public sealed class ValidationErrorDialog : IValidationErrorDialog
{
	public void Show(IReadOnlyList<string> errors)
	{
		MessageBox.Show(
			string.Join(Environment.NewLine, errors),
			"ERROR",
			MessageBoxButtons.OK,
			MessageBoxIcon.Error);
	}
}
