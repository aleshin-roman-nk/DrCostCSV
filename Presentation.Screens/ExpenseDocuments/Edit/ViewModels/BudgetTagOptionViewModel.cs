namespace Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

public sealed class BudgetTagOptionViewModel
{
	public int Id { get; init; }
	public int BudgetLineId { get; init; }
	public required string Name { get; init; }
}
