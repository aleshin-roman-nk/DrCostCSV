using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.Currencies.Abstractions;
using Domain;

namespace Application.Currencies.SaveCurrency;

public sealed class SaveCurrencyUseCase(ICurrencyRepository repository, IUnitOfWork unitOfWork)
{
	public UseCaseResult<int> Execute(SaveCurrencyCommand command)
	{
		var code = (command.Code ?? "").Trim().ToUpperInvariant();
		var name = (command.Name ?? "").Trim();
		if (command.Id is <= 0)
			return Failure("invalid_id", "Некорректный идентификатор валюты.");
		if (code.Length != 3 || code.Any(character => character < 'A' || character > 'Z'))
			return Failure("invalid_code", "Код валюты должен содержать три латинские буквы, например BYN.");
		if (name.Length == 0 || name.Length > 100)
			return Failure("invalid_name", "Укажите наименование валюты длиной до 100 символов.");

		var currency = command.Id.HasValue ? repository.GetById(command.Id.Value) : null;
		if (command.Id.HasValue && currency is null)
			return Failure("currency_not_found", "Валюта не найдена.");
		if (repository.CodeExists(code, command.Id))
			return Failure("duplicate_currency", "Валюта с таким кодом уже существует.");

		if (currency is null)
		{
			currency = new Currency(code, name);
			repository.Add(currency);
		}
		else
		{
			currency.Code = code;
			currency.Name = name;
		}

		unitOfWork.SaveChanges();
		return UseCaseResult<int>.Success(currency.Id);
	}

	private static UseCaseResult<int> Failure(string code, string message) =>
		UseCaseResult<int>.Failure(new UseCaseError(code, message));
}
