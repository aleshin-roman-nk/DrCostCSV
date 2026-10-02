using Application.Common;
using Application.Currencies.Abstractions;

namespace Application.Currencies.GetCurrencies;

public sealed class GetCurrenciesUseCase(ICurrencyReader reader)
{
	public UseCaseResult<IReadOnlyList<CurrencyDto>> Execute() =>
		UseCaseResult<IReadOnlyList<CurrencyDto>>.Success(reader.GetAll());
}
