using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.CategoryExpense;

public class GetCategoryExpensesUseCase
{
	public GetCategoryExpensesUseCase() { }

	public IReadOnlyCollection<CategoryExpenseRowDto> Handle(GetCategoryExpensesQuery query)
	{
		return new List<CategoryExpenseRowDto>();
	}
}
