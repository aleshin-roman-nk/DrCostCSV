using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class ExpenseDocument
	{
		public int id {  get; set; }
		public string? SellerName { get; set; }
		public DateOnly Date {  get; set; }
		public IReadOnlyList<ExpenseDocumentItem>? ExpenseDocumentItem {  get; set; }
	}
}
