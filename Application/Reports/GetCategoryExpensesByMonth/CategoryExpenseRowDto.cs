using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetCategoryExpensesByMonth;

public class CategoryExpenseRowDto
{
	public required string Name { get; set; }
	public decimal Sum { get; set; }
}
