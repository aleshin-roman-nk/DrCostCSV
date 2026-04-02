using Application.ExpenseDocuments.Queries.GetExpenseDocumentsByMonth;
using Application.Reports.CategoryExpense;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Queries;

public interface IExpenseReportsQueryService
{
	IReadOnlyList<CategoryExpenseRowDto> GetCategoryExpensesByMonth(
					int year,
					int month);
}
