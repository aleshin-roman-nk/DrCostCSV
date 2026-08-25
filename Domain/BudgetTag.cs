namespace Domain;

public sealed class BudgetTag
{
	public int Id { get; set; }
	public int BudgetLineId { get; set; }
	public BudgetLine BudgetLine { get; set; } = null!;
	public string Name { get; set; }

	public BudgetTag(int budgetLineId, string name)
	{
		BudgetLineId = budgetLineId;
		Name = name;
	}
}
