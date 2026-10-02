using Domain;

namespace Application.CurrencyDefaults.Abstractions;

public interface ICurrencyDefaultSettingsRepository
{
	CurrencyDefaultSettings? Get();
	void Add(CurrencyDefaultSettings settings);
}
