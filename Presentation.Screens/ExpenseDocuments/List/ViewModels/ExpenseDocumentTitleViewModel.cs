using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.List.ViewModels
{
	public class ExpenseDocumentTitleViewModel
	{
		public int Id {  get; set; }
		public DateTime Date {  get; set; }
		public string? Seller {  get; set; }
		public decimal Sum {  get; set; }
	}
}
