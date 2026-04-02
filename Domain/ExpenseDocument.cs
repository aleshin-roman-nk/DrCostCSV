using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public class ExpenseDocument
{
	public int Id { get; private set; }
	public string SellerName { get; private set; }
	public DateOnly Date { get; private set; }
	public List<ExpenseDocumentItem> Items = new();

	public ExpenseDocument(int id, string sellerName, DateOnly date)
	{
		Id = id;
		SellerName = sellerName;
		Date = date;
	}
}
