using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetDocumentTitlesByDay;
using Application.Reports.GetBudgetLineExpensesByMonth;
using Application.Reports;

namespace Application.Reports.Abstractions;

public interface IExpenseReportsReader
{
	MonthlyReportReadResult<DailyExpenseDto> GetDailyExpensesByMonth(int year, int month, int reportCurrencyId);
	IReadOnlyList<DocumentTitleDto> GetDocumentTitlesByDay(DateTime dt);
	MonthlyReportReadResult<BudgetLineExpenseDto> GetBudgetLineExpensesByMonth(
		int year, int month, int reportCurrencyId);
}
