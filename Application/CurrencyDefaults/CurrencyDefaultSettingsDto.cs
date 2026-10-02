namespace Application.CurrencyDefaults;

public sealed record CurrencyValueDto(int CurrencyId, decimal Value);
public sealed record CurrencyDefaultSettingsDto(
	int? DocumentCurrencyId, int? ReportCurrencyId, IReadOnlyList<CurrencyValueDto> CurrencyValues);
