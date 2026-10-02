namespace Application.CurrencyDefaults;

public sealed record SaveCurrencyDefaultsCommand(
	int? DocumentCurrencyId, IReadOnlyList<CurrencyValueDto> CurrencyValues, int? ReportCurrencyId = null);
