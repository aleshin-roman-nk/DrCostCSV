using Application.Currencies;
using Application.Currencies.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Readers;

public sealed class EfCurrencyReader(AppDbContext db) : ICurrencyReader
{
	public IReadOnlyList<CurrencyDto> GetAll() => db.Currencies.AsNoTracking()
		.OrderBy(currency => currency.Code)
		.Select(currency => new CurrencyDto(currency.Id, currency.Code, currency.Name))
		.ToList();

	public CurrencyDto? GetById(int id) => db.Currencies.AsNoTracking()
		.Where(currency => currency.Id == id)
		.Select(currency => new CurrencyDto(currency.Id, currency.Code, currency.Name))
		.SingleOrDefault();
}
