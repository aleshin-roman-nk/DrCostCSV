using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Main.ViewModels;

public class DailyExpenseRowViewModel
{
	public DateTime Date {  get; set; }
	public decimal TotalSum { get; set; }
}
