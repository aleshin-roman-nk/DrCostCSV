using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels
{
	public class CategoryOptionViewModel
	{
		public int? Id { get; init; }

		public string Name { get; init; } = string.Empty;

		public bool IsNew => Id is null;
	}
}
