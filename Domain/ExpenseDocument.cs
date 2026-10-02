using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public sealed class ExpenseDocument
{
	public int Id { get; private set; }

	public DateTime Date { get; private set; }

	public string? SellerName { get; private set; }

	public string? Comment { get; private set; }

	/// <summary>
	/// Currency of all item prices and the document total; null when not specified.
	/// </summary>
	public int? CurrencyId { get; private set; }

	private readonly List<CurrencyValue> currencyValues = [];
	public IReadOnlyList<CurrencyValue> CurrencyValues => currencyValues.AsReadOnly();

	public void SetCurrencyValues(int? currencyId, IEnumerable<CurrencyValue> values)
	{
		CurrencyValue.ReplaceScale(currencyValues, currencyId, values);
		CurrencyId = currencyId;
	}

	private readonly List<ExpenseDocumentItem> items = [];

	public IReadOnlyList<ExpenseDocumentItem> Items => items;

	public decimal TotalSum => items.Sum(x => x.Sum);

	private ExpenseDocument()
	{
		// For EF Core
	}

	public ExpenseDocument(DateTime date, string? sellerName, string? comment = null)
	{
		Date = date.Date;
		Comment = comment;
		SellerName = sellerName;
	}

	public void ChangeDate(DateTime date)
	{
		Date = date.Date;
	}

	public void ChangeComment(string? comment)
	{
		Comment = comment;
	}

	public void ChangeSellerName(string? sellerName)
	{
		SellerName = sellerName;
	}

	public void ChangeCurrency(int currencyId)
	{
		if (currencyId <= 0)
			throw new ArgumentOutOfRangeException(nameof(currencyId), "Currency identifier must be greater than zero.");

		if (currencyValues.Count > 0 && !currencyValues.Any(value => value.CurrencyId == currencyId))
			throw new ArgumentException("The scale must contain the document currency.", nameof(currencyId));

		CurrencyId = currencyId;
	}

	public void AddItem(
		string name,
		decimal price,
		decimal amount,
		int budgetLineId,
		int? budgetTagId = null)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Item name is required.", nameof(name));

		if (amount <= 0)
			throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

		var item = new ExpenseDocumentItem(
			expenseDocument: this,
			name: name,
			price: price,
			amount: amount,
			budgetLineId: budgetLineId,
			budgetTagId: budgetTagId);

		items.Add(item);
	}

	public void UpdateItem(
	int itemId,
	string name,
	decimal price,
	decimal amount,
	int budgetLineId,
	int? budgetTagId)
	{
		var item = items.SingleOrDefault(x => x.Id == itemId);

		if (item is null)
		{
			throw new InvalidOperationException(
				$"Item {itemId} does not belong to document {Id}.");
		}

		item.Update(
			name,
			price,
			amount,
			budgetLineId,
			budgetTagId);
	}

	public void RemoveItem(int itemId)
	{
		var item = items.FirstOrDefault(x => x.Id == itemId);

		if (item is null)
			return;

		items.Remove(item);
	}

	public void ClearItems()
	{
		items.Clear();
	}
}
