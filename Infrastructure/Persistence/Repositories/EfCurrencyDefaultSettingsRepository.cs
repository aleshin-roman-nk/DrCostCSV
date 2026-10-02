using Application.CurrencyDefaults.Abstractions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class EfCurrencyDefaultSettingsRepository(AppDbContext db) : ICurrencyDefaultSettingsRepository
{
	public CurrencyDefaultSettings? Get() => db.CurrencyDefaultSettings
		.Include(settings => settings.CurrencyValues)
		.SingleOrDefault(settings => settings.Id == CurrencyDefaultSettings.SingletonId);
	public void Add(CurrencyDefaultSettings settings) => db.CurrencyDefaultSettings.Add(settings);
}
