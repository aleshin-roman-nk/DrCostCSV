using DrCostCSV.Screens.ExpenseDocuments.Edit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrCostCSV.Screens.Main
{
	public class MainPresenter
	{
		private readonly IMainView mainView;
		private readonly ExpenseDocumentEditScreen documentEditScreen;

		public MainPresenter(IMainView mainView, 
			ExpenseDocumentEditScreen documentEditScreen)
		{
			this.mainView = mainView;
			this.documentEditScreen = documentEditScreen;

			this.mainView.DateChanged += MainView_DateChanged;
			this.mainView.CreateDocumentRequested += MainView_CreateDocumentRequested;
		}

		private void MainView_CreateDocumentRequested(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void MainView_DateChanged(object? sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		public void Initialize()
		{
			this.mainView.BindDocs();
		}
	}
}
