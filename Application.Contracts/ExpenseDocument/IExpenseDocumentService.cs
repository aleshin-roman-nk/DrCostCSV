using Application.Contracts.ExpenseDocument.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.ExpenseDocument
{
	public interface IExpenseDocumentService
	{
		public void Update();
		public void Create();
		public IEnumerable<ExpenseDocumentDto> GetDocs(int year, int month);
	}
}
