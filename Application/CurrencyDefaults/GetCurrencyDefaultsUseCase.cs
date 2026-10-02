using Application.Common;
using Application.CurrencyDefaults.Abstractions;

namespace Application.CurrencyDefaults;

public sealed class GetCurrencyDefaultsUseCase(ICurrencyDefaultSettingsRepository repository)
{
	public UseCaseResult<CurrencyDefaultSettingsDto> Execute()
	{
		var settings = repository.Get();
		return UseCaseResult<CurrencyDefaultSettingsDto>.Success(new(
			settings?.DocumentCurrencyId,
			settings?.ReportCurrencyId,
			settings?.CurrencyValues.Select(value => new CurrencyValueDto(value.CurrencyId, value.Value)).ToList() ?? []));
	}
}
