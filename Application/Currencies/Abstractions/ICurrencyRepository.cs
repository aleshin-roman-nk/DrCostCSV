using Domain;

namespace Application.Currencies.Abstractions;

public interface ICurrencyRepository
{
	Currency? GetById(int id);
	bool CodeExists(string code, int? exceptId);
	bool IsUsed(int id);
	void Add(Currency currency);
	void Remove(Currency currency);
}
