namespace Domain;

/// <summary>An amount equivalent to every other amount in the same currency scale.</summary>
public sealed class CurrencyValue
{
	public int CurrencyId { get; private set; }
	public decimal Value { get; private set; }

	private CurrencyValue() { }

	public CurrencyValue(int currencyId, decimal value)
	{
		if (currencyId <= 0)
			throw new ArgumentOutOfRangeException(nameof(currencyId));
		if (value <= 0)
			throw new ArgumentOutOfRangeException(nameof(value));
		CurrencyId = currencyId;
		Value = value;
	}

	internal static void ReplaceScale(List<CurrencyValue> target, int? documentCurrencyId, IEnumerable<CurrencyValue> values)
	{
		ArgumentNullException.ThrowIfNull(values);
		var incoming = values.ToList();
		if (documentCurrencyId is <= 0)
			throw new ArgumentOutOfRangeException(nameof(documentCurrencyId));
		if (incoming.Any(value => value is null || value.CurrencyId <= 0 || value.Value <= 0))
			throw new ArgumentException("Currency values must be positive.", nameof(values));
		if (incoming.Select(value => value.CurrencyId).Distinct().Count() != incoming.Count)
			throw new ArgumentException("A currency may appear only once in a scale.", nameof(values));
		if (incoming.Count > 0 && !incoming.Any(value => value.CurrencyId == documentCurrencyId))
			throw new ArgumentException("The scale must contain the document currency.", nameof(values));

		var amounts = incoming.ToDictionary(value => value.CurrencyId, value => value.Value);
		target.RemoveAll(value => !amounts.ContainsKey(value.CurrencyId));
		foreach (var (currencyId, amount) in amounts)
		{
			var existing = target.SingleOrDefault(value => value.CurrencyId == currencyId);
			if (existing is null)
				target.Add(new CurrencyValue(currencyId, amount));
			else
				existing.Value = amount;
		}
	}
}
