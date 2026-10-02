using Application.Currencies.Abstractions;
using Domain;

namespace Infrastructure.Persistence.Repositories;

public sealed class EfCurrencyRepository(AppDbContext db) : ICurrencyRepository
{
	public Currency? GetById(int id) => db.Currencies.Find(id);
	public bool CodeExists(string code, int? exceptId) =>
		db.Currencies.Any(currency => currency.Code == code && (!exceptId.HasValue || currency.Id != exceptId.Value));
	public bool IsUsed(int id) =>
		db.ExpenseDocuments.Any(document => document.CurrencyId == id ||
			document.CurrencyValues.Any(value => value.CurrencyId == id)) ||
		db.CurrencyDefaultSettings.Any(settings => settings.DocumentCurrencyId == id ||
			settings.ReportCurrencyId == id ||
			settings.CurrencyValues.Any(value => value.CurrencyId == id));
	public void Add(Currency currency) => db.Currencies.Add(currency);
	public void Remove(Currency currency) => db.Currencies.Remove(currency);
}
