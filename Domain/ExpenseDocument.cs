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

	public void AddItem(
		string name,
		decimal price,
		decimal amount,
		int? budgetLineId,
		int? budgetTagId = null)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Item name is required.", nameof(name));

		if (price < 0)
			throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

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
	int? budgetLineId,
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
