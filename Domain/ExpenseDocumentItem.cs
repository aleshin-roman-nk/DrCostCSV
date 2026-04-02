using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public class ExpenseDocumentItem
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public decimal Price { get; set; }
	public decimal Amount { get; set; }
	public decimal Sum => Price * Amount;
	public int CategoryId { get; set; }

	public ExpenseDocumentItem(
		int id,
		string name,
		decimal price,
		decimal amount,
		int categoryId)
	{

	}
}
