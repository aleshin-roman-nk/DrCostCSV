using Application.Common;
using Application.Currencies.Abstractions;
using Application.CurrencyDefaults;

namespace Application.ExpenseDocuments;

internal static class CurrencySelectionValidator
{
	public static UseCaseError? Validate(
		int? currencyId, IReadOnlyList<CurrencyValueDto> values, ICurrencyReader currencies)
	{
		if (currencyId is <= 0)
			return new("invalid_currency", "Выберите корректную валюту цен документа.");
		if (values.Any(value => value.CurrencyId <= 0 || value.Value <= 0))
			return new("invalid_currency_value", "В каждой строке выберите валюту и укажите значение больше нуля.");
		if (values.Select(value => value.CurrencyId).Distinct().Count() != values.Count)
			return new("duplicate_currency", "Валюта не должна повторяться в шкале документа.");
		if (values.Count > 0 && !values.Any(value => value.CurrencyId == currencyId))
			return new("document_currency_missing", "Шкала должна содержать валюту цен документа.");

		var ids = currencies.GetAll().Select(currency => currency.Id).ToHashSet();
		if ((currencyId.HasValue && !ids.Contains(currencyId.Value)) ||
			values.Any(value => !ids.Contains(value.CurrencyId)))
			return new("currency_not_found", "Одна из выбранных валют больше не существует.");
		return null;
	}
}
