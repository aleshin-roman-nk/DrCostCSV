namespace Application.Currencies.Abstractions;

public interface ICurrencyReader
{
	IReadOnlyList<CurrencyDto> GetAll();
	CurrencyDto? GetById(int id);
}
