using Application.JsonImportPromptSettings.Abstractions;
using PromptSettings = Domain.JsonImportPromptSettings;

namespace Infrastructure.Persistence.Repositories;

public sealed class EfJsonImportPromptSettingsRepository : IJsonImportPromptSettingsRepository
{
	private readonly AppDbContext dbContext;

	public EfJsonImportPromptSettingsRepository(AppDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public PromptSettings? Get()
	{
		return dbContext.Set<PromptSettings>().SingleOrDefault(x => x.Id == PromptSettings.SingletonId);
	}

	public void Add(PromptSettings settings)
	{
		dbContext.Set<PromptSettings>().Add(settings);
	}
}
