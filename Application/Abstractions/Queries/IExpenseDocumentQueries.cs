using Application.ExpenseDocuments.Queries.GetExpenseDocumentsByMonth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Queries
{
	public interface IExpenseDocumentQueries
	{
		IReadOnlyList<ExpenseDocumentDto> GetDocumentsByMonth(
				int year,
				int month);
		ExpenseDocumentDto GetDocumentById(int id);
	}
}
