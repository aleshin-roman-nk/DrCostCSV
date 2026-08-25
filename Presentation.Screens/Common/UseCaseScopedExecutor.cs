using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Screens.Common;

public sealed class UseCaseScopedExecutor : IUseCaseScopedExecutor
{
	private readonly IServiceScopeFactory scopeFactory;

	public UseCaseScopedExecutor(IServiceScopeFactory scopeFactory)
	{
		this.scopeFactory = scopeFactory;
	}

	public TResult Execute<TUseCase, TResult>(Func<TUseCase, TResult> action)
		where TUseCase : notnull
	{
		using var scope = scopeFactory.CreateScope();
		var useCase = scope.ServiceProvider.GetRequiredService<TUseCase>();

		return action(useCase);
	}

	public async Task<TResult> ExecuteAsync<TUseCase, TResult>(Func<TUseCase, Task<TResult>> action)
		where TUseCase : notnull
	{
		await using var scope = scopeFactory.CreateAsyncScope();
		var useCase = scope.ServiceProvider.GetRequiredService<TUseCase>();

		return await action(useCase);
	}

	public async Task ExecuteAsync<TUseCase>(Func<TUseCase, Task> action)
		where TUseCase : notnull
	{
		await using var scope = scopeFactory.CreateAsyncScope();
		var useCase = scope.ServiceProvider.GetRequiredService<TUseCase>();

		await action(useCase);
	}
}
