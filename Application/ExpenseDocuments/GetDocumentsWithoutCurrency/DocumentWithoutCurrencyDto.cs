namespace Application.ExpenseDocuments.GetDocumentsWithoutCurrency;

public sealed record DocumentWithoutCurrencyDto(int Id, DateTime Date, string Seller, int ItemCount);
