using Application.Common;
using Application.Currencies.Abstractions;

namespace Application.Currencies.GetCurrencyForEdit;

public sealed class GetCurrencyForEditUseCase(ICurrencyReader reader)
{
	public UseCaseResult<CurrencyDto> Execute(int id)
	{
		var currency = reader.GetById(id);
		return currency is null
			? UseCaseResult<CurrencyDto>.Failure(new UseCaseError("currency_not_found", "Валюта не найдена."))
			: UseCaseResult<CurrencyDto>.Success(currency);
	}
}
