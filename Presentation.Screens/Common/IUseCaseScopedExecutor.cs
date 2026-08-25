namespace Presentation.Screens.Common;

public interface IUseCaseScopedExecutor
{
	TResult Execute<TUseCase, TResult>(Func<TUseCase, TResult> action)
		where TUseCase : notnull;

	Task<TResult> ExecuteAsync<TUseCase, TResult>(Func<TUseCase, Task<TResult>> action)
		where TUseCase : notnull;

	Task ExecuteAsync<TUseCase>(Func<TUseCase, Task> action)
		where TUseCase : notnull;
}
