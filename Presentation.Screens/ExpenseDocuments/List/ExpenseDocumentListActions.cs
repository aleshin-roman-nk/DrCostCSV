using Application.Common;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.Reports.GetDocumentTitlesByDay;
using Microsoft.Extensions.Logging;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using Presentation.Screens.ExpenseDocuments.List.ViewModels;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.List;

public sealed class ExpenseDocumentListActions
{
	private readonly IUseCaseScopedExecutor useCaseScopedExecutor;
	private readonly ILogger<ExpenseDocumentListActions> logger;

	public ExpenseDocumentListActions(
		IUseCaseScopedExecutor useCaseScopedExecutor,

		ILogger<ExpenseDocumentListActions> logger
		
		)
	{
		this.useCaseScopedExecutor = useCaseScopedExecutor;
		this.logger = logger;
	}


	public ActionResult<IReadOnlyList<ExpenseDocumentTitleViewModel>> GetDocumentTitles(DateTime dt)
	{
		var useCaseResult = useCaseScopedExecutor
			.Execute<GetDocumentTitlesByDayUseCase, UseCaseResult<IReadOnlyList<DocumentTitleDto>>>(useCase =>
		{
			return useCase.Execute(dt);
		});

		//logger.LogInformation("{@useCaseResult}", useCaseResult);

		if (!useCaseResult.IsSuccess)
		{
			var errorMessage = useCaseResult.Error?.Message ?? "Unknown error.";
			return ActionResult<IReadOnlyList<ExpenseDocumentTitleViewModel>>
				.Failure(errorMessage);
		}

		var documents = useCaseResult.Value ?? Array.Empty<DocumentTitleDto>();

		var viewModelResult = documents.Select(x => new ExpenseDocumentTitleViewModel
		{
			Id = x.Id,
			Date = x.Date,
			Seller = x.Seller,
			Sum = x.Sum
		}).ToList();

		return ActionResult<IReadOnlyList<ExpenseDocumentTitleViewModel>>
				.Success(viewModelResult);
	}
}
