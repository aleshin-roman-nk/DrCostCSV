using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ExpenseDocuments.Abstractions.Persistence;

public interface IExpenseDocumentRepository
{
	void Add(ExpenseDocument document);
	ExpenseDocument? GetById(int id);
	void Remove(ExpenseDocument document);
}
