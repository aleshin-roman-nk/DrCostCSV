using DrCostCSV.Screens.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrCostCSV.Screens.Main
{
	public interface IMainView
	{
		public event EventHandler DateChanged;
		public event EventHandler CreateDocumentRequested;
		public void BindDocs(IEnumerable<CategoryExpenseRowViewModel> docs);
	}
}
