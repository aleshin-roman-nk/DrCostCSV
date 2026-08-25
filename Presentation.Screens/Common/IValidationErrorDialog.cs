namespace Presentation.Screens.Common;

public interface IValidationErrorDialog
{
	void Show(IReadOnlyList<string> errors);
}
