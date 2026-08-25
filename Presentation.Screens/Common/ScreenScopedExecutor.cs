using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Screens.Common;

public sealed class ScreenScopedExecutor : IScreenScopedExecutor
{
	private readonly IServiceScopeFactory scopeFactory;

	public ScreenScopedExecutor(IServiceScopeFactory scopeFactory)
	{
		this.scopeFactory = scopeFactory;
	}

	public TResult Execute<TPresenter, TResult>(Func<TPresenter, TResult> action)
		where TPresenter : notnull
	{
		using var scope = scopeFactory.CreateScope();
		var presenter = scope.ServiceProvider.GetRequiredService<TPresenter>();

		return action(presenter);
	}

	public async Task<TResult> ExecuteAsync<TPresenter, TResult>(Func<TPresenter, Task<TResult>> action)
		where TPresenter : notnull
	{
		await using var scope = scopeFactory.CreateAsyncScope();
		var presenter = scope.ServiceProvider.GetRequiredService<TPresenter>();

		return await action(presenter);
	}

	public async Task ExecuteAsync<TPresenter>(Func<TPresenter, Task> action)
		where TPresenter : notnull
	{
		await using var scope = scopeFactory.CreateAsyncScope();
		var presenter = scope.ServiceProvider.GetRequiredService<TPresenter>();

		await action(presenter);
	}
}
