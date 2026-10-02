namespace Domain;

public sealed class Currency
{
	public int Id { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }

	public Currency(string code, string name)
	{
		Code = code;
		Name = name;
	}
}
