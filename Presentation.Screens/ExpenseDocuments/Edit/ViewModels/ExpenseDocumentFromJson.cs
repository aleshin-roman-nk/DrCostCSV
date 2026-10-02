namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public sealed class ExpenseDocumentFromJson
{
	public required DateOnly? Date { get; init; }
	public required IReadOnlyList<ExpenseDocumentItemFromJson> Items { get; init; }
}
