using Application.Common;
using Application.Common.Abstractions.Persistence;
using Application.Currencies.Abstractions;
using Application.CurrencyDefaults.Abstractions;
using Domain;

namespace Application.CurrencyDefaults;

public sealed class SaveCurrencyDefaultsUseCase(
	ICurrencyDefaultSettingsRepository repository, ICurrencyReader currencies, IUnitOfWork unitOfWork)
{
	public UseCaseResult Execute(SaveCurrencyDefaultsCommand command)
	{
		if (command.DocumentCurrencyId is <= 0)
			return Failure("invalid_currency", "Выберите валюту документа.");
		if (command.ReportCurrencyId is <= 0)
			return Failure("invalid_report_currency", "Выберите корректную валюту отчета.");
		if (command.CurrencyValues.Any(value => value.CurrencyId <= 0 || value.Value <= 0))
			return Failure("invalid_currency_value", "В каждой строке выберите валюту и укажите значение больше нуля.");
		if (command.CurrencyValues.Select(value => value.CurrencyId).Distinct().Count() != command.CurrencyValues.Count)
			return Failure("duplicate_currency", "Валюта не должна повторяться в шкале.");
		if (command.CurrencyValues.Count > 0 &&
			!command.CurrencyValues.Any(value => value.CurrencyId == command.DocumentCurrencyId))
			return Failure("document_currency_missing", "Добавьте в шкалу валюту документа или очистите шкалу.");

		var existingIds = currencies.GetAll().Select(currency => currency.Id).ToHashSet();
		if ((command.DocumentCurrencyId.HasValue && !existingIds.Contains(command.DocumentCurrencyId.Value)) ||
			(command.ReportCurrencyId.HasValue && !existingIds.Contains(command.ReportCurrencyId.Value)) ||
			command.CurrencyValues.Any(value => !existingIds.Contains(value.CurrencyId)))
			return Failure("currency_not_found", "Одна из выбранных валют больше не существует. Откройте настройки заново.");

		var settings = repository.Get();
		if (settings is null)
		{
			settings = new CurrencyDefaultSettings();
			repository.Add(settings);
		}
		settings.SetDefaults(command.DocumentCurrencyId,
			command.CurrencyValues.Select(value => new CurrencyValue(value.CurrencyId, value.Value)));
		settings.SetReportCurrency(command.ReportCurrencyId);
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}

	private static UseCaseResult Failure(string code, string message) =>
		UseCaseResult.Failure(new UseCaseError(code, message));
}
