namespace Presentation.Screens.Common;

public interface IViewModelVerifier<in TViewModel>
{
	ValidationResult Verify(TViewModel viewModel);
}
