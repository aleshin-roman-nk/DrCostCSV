using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public sealed class ExpenseDocumentItem
{
	public int Id { get; private set; }

	public int ExpenseDocumentId { get; private set; }

	public ExpenseDocument ExpenseDocument { get; private set; } = null!;

	public string Name { get; private set; } = "";

	public decimal Price { get; private set; }

	public decimal Amount { get; private set; }

	public int BudgetLineId { get; private set; }

	public BudgetLine BudgetLine { get; private set; } = null!;

	public int? BudgetTagId { get; private set; }

	public BudgetTag? BudgetTag { get; private set; }

	public decimal Sum => Price * Amount;

	private ExpenseDocumentItem()
	{
		// For EF Core
	}

	internal ExpenseDocumentItem(
		ExpenseDocument expenseDocument,
		string name,
		decimal price,
		decimal amount,
		int budgetLineId,
		int? budgetTagId)
	{
		Validate(name, price, amount, budgetLineId, budgetTagId);

		ExpenseDocument = expenseDocument;
		Name = name.Trim();
		Price = price;
		Amount = amount;
		BudgetLineId = budgetLineId;
		BudgetTagId = budgetTagId;
	}

	public void Update(
	string name,
	decimal price,
	decimal amount,
	int budgetLineId,
	int? budgetTagId)
	{
		Validate(name, price, amount, budgetLineId, budgetTagId);

		Name = name.Trim();
		Price = price;
		Amount = amount;
		BudgetLineId = budgetLineId;
		BudgetTagId = budgetTagId;
	}

	private static void Validate(
		string name,
		decimal price,
		decimal amount,
		int budgetLineId,
		int? budgetTagId)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException(
				"Item name is required.",
				nameof(name));

		if (amount <= 0)
			throw new ArgumentOutOfRangeException(
				nameof(amount),
				"Amount must be greater than zero.");

		if (budgetLineId <= 0)
			throw new ArgumentOutOfRangeException(
				nameof(budgetLineId),
				"Budget line is required.");

		if (budgetTagId <= 0)
			throw new ArgumentOutOfRangeException(
				nameof(budgetTagId),
				"Budget tag identifier must be greater than zero when specified.");
	}
}
