using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.ExpenseDocument.Dto
{
	public sealed class ExpenseDocumentItemDto
	{
		public string? Name { get; set; }
		public decimal? Price { get; set; }
		public decimal? Amount { get; set; }
		public decimal? Sum => Price * Amount;
	}
}
