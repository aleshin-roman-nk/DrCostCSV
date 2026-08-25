namespace Domain;

public sealed class BudgetLine
{
	public int Id { get; set; }
	public string Name { get; set; }

	public BudgetLine(string name)
	{
		Name = name;
	}
}
