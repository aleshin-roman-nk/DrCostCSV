using Application.Common;
using Application.Reports.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetDocumentTitlesByDay;

public class GetDocumentTitlesByDayUseCase
{
	private readonly IExpenseReportsReader expenseReportsReader;

	public GetDocumentTitlesByDayUseCase(IExpenseReportsReader expenseReportsReader)
	{
		this.expenseReportsReader = expenseReportsReader;
	}

	public UseCaseResult<IReadOnlyList<DocumentTitleDto>> Execute(DateTime date)
	{
		return UseCaseResult<IReadOnlyList<DocumentTitleDto>>
			.Success(expenseReportsReader.GetDocumentTitlesByDay(date));
	}
}
