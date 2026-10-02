namespace Application.Reports;

public sealed record MonthlyReportReadResult<T>(
	IReadOnlyList<T> Rows, int ExcludedDocumentCount);
