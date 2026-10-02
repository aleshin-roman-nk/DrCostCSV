using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.Currencies.Abstractions;

namespace Application.Currencies.DeleteCurrency;

public sealed class DeleteCurrencyUseCase(ICurrencyRepository repository, IUnitOfWork unitOfWork)
{
	public UseCaseResult Execute(int id)
	{
		var currency = repository.GetById(id);
		if (currency is null)
			return UseCaseResult.Failure(new UseCaseError("currency_not_found", "Валюта не найдена."));
		if (repository.IsUsed(id))
			return UseCaseResult.Failure(new UseCaseError("currency_in_use", "Нельзя удалить валюту, используемую в документах или настройках."));

		repository.Remove(currency);
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
