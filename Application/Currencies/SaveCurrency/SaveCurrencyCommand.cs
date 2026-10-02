namespace Application.Currencies.SaveCurrency;

public sealed record SaveCurrencyCommand(int? Id, string Code, string Name);
