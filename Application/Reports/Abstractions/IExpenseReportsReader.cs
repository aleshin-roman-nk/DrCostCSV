using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetDocumentTitlesByDay;
using Application.Reports.GetBudgetLineExpensesByMonth;

namespace Application.Reports.Abstractions;

public interface IExpenseReportsReader
{
	IReadOnlyList<DailyExpenseDto> GetDailyExpensesByMonth(int year, int month);
	IReadOnlyList<DocumentTitleDto> GetDocumentTitlesByDay(DateTime dt);
	IReadOnlyList<BudgetLineExpenseDto> GetBudgetLineExpensesByMonth(int year, int month);
}
