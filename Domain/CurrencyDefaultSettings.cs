namespace Domain;

public sealed class CurrencyDefaultSettings
{
	public const int SingletonId = 1;
	public int Id { get; private set; } = SingletonId;
	public int? DocumentCurrencyId { get; private set; }
	public int? ReportCurrencyId { get; private set; }
	private readonly List<CurrencyValue> currencyValues = [];
	public IReadOnlyList<CurrencyValue> CurrencyValues => currencyValues.AsReadOnly();

	public void SetDefaults(int? documentCurrencyId, IEnumerable<CurrencyValue> values)
	{
		CurrencyValue.ReplaceScale(currencyValues, documentCurrencyId, values);
		DocumentCurrencyId = documentCurrencyId;
	}

	public void SetReportCurrency(int? reportCurrencyId)
	{
		if (reportCurrencyId is <= 0)
			throw new ArgumentOutOfRangeException(nameof(reportCurrencyId));
		ReportCurrencyId = reportCurrencyId;
	}
}
