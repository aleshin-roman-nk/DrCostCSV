namespace Presentation.Screens.Common;

public interface IScreenScopedExecutor
{
	TResult Execute<TPresenter, TResult>(Func<TPresenter, TResult> action)
		where TPresenter : notnull;

	Task<TResult> ExecuteAsync<TPresenter, TResult>(Func<TPresenter, Task<TResult>> action)
		where TPresenter : notnull;

	Task ExecuteAsync<TPresenter>(Func<TPresenter, Task> action)
		where TPresenter : notnull;
}
