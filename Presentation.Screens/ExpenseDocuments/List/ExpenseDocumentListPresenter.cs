using Microsoft.Extensions.Logging;
using Presentation.Screens.ExpenseDocuments.Edit;
using Presentation.Screens.ExpenseDocuments.List.ViewModels;
using Presentation.Screens.Common;
using Presentation.Screens.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.List;

/// <summary>
/// Documents of a day screen | All documents screen
/// </summary>
public class ExpenseDocumentListPresenter
{
	private readonly IExpenseDocumentListView view;
	private readonly ExpenseDocumentEditFlow expenseDocumentEditFlow;
	private readonly ExpenseDocumentListActions documentListActions;
	private readonly ILogger<ExpenseDocumentEditActions> logger;
	private DateTime documentDate = DateTime.Today;

	public ExpenseDocumentListPresenter(

		IExpenseDocumentListView view,
		ExpenseDocumentEditFlow expenseDocumentEditFlow,
		ExpenseDocumentListActions documentListActions,

		ILogger<ExpenseDocumentEditActions> logger

		)
	{
		this.view = view;
		this.expenseDocumentEditFlow = expenseDocumentEditFlow;
		this.documentListActions = documentListActions;
		this.logger = logger;
		this.view.CreateExpenseDocument += View_CreateExpenseDocument;
		this.view.OpenExpenseDocument += View_OpenExpenseDocument;
	}

	private void View_OpenExpenseDocument(int obj)
	{
		var res = expenseDocumentEditFlow.RunEdit(obj);

		if (res.IsSuccess) ReloadDailyExpenses(documentDate);
	}

	private void View_CreateExpenseDocument()
	{
		var res = expenseDocumentEditFlow.RunCreate(documentDate);
		if (!res.IsCancelled) ReloadDailyExpenses(documentDate);
	}

	public ScreenResult Run(DateTime dt)
	{
		documentDate = dt;


		Init(documentDate);

		view.SetDate(dt);


		view.ShowModal();

		return ScreenResult.Success();
	}

	private void Init(DateTime date)
	{
		ReloadDailyExpenses(date);
	}

	private void ReloadDailyExpenses(DateTime date)
	{
		var result = documentListActions.GetDocumentTitles(date);

		

		if (!result.IsSuccess)
		{
			var errorMessage = result.Error
				?? "Не удалось загрузить документы за выбранный день.";


			view.SetDocumentTitles(Array.Empty<ExpenseDocumentTitleViewModel>());
			return;
		}



		var documentTitles = result.Data
			?? Array.Empty<ExpenseDocumentTitleViewModel>();

		view.SetDocumentTitles(documentTitles);
	}

}
