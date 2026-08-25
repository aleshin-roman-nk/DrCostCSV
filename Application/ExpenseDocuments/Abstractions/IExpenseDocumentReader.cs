using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Abstractions
{
	public interface IExpenseDocumentReader
	{
		ExpenseDocumentForEditDto? GetForEdit(int documentId);
	}
}
