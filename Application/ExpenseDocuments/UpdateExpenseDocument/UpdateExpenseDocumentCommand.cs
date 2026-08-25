using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.UpdateExpenseDocument
{
	public class UpdateExpenseDocumentCommand
	{
		public int DocumentId { get; init; }

		public DateTime Date { get; init; }

		public required string SellerName { get; init; }

		public required IReadOnlyList<UpdateExpenseDocumentItemCommand> Items
		{
			get;
			init;
		}
	}
}
