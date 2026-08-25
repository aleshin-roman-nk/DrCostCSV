using Application.Common;
using Application.ExpenseDocuments.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.GetExpenseDocumentForEdit;

public class GetExpenseDocumentForEditUseCase
{
	private readonly IExpenseDocumentReader documentReader;

	public GetExpenseDocumentForEditUseCase(IExpenseDocumentReader documentReader)
	{
		this.documentReader = documentReader;
	}

	public UseCaseResult<ExpenseDocumentForEditDto> Execute(int documentId)
	{
		var document = documentReader.GetForEdit(documentId);

		if (document is null)
		{
			return UseCaseResult<ExpenseDocumentForEditDto>.Failure(new UseCaseError(Code : "0", Message : "Документ не найден"));
		}

		return UseCaseResult<ExpenseDocumentForEditDto>.Success(document);
	}
}
