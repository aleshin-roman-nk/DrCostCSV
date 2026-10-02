using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.ExpenseDocuments.GetDocumentsWithoutCurrency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Abstractions
{
	public interface IExpenseDocumentReader
	{
		ExpenseDocumentForEditDto? GetForEdit(int documentId);
		IReadOnlyList<DocumentWithoutCurrencyDto> GetWithoutCurrency();
	}
}
