using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetCategoryExpensesByMonth;

public class GetCategoryExpenseResult
{
	public IReadOnlyCollection<CategoryExpenseRowDto>? Items {  get; private set; }
}
