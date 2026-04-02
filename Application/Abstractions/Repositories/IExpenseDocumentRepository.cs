using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
	public interface IExpenseDocumentRepository
	{
		void Add(ExpenseDocument document);
	}
}
