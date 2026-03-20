using DrCostCSV.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrCostCSV.Main
{
	internal interface IMainView
	{
		public event EventHandler DateChanged;
		public void BindDocs(IEnumerable<CategoryExpenseRowViewModel> docs);
	}
}
