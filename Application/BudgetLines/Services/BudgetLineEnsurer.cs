using Application.BudgetLines.Abstractions;
using Application.Common.Abstractions.Persistence;
using Domain;

namespace Application.BudgetLines.Services;

public sealed class BudgetLineEnsurer : IBudgetLineEnsurer
{
	private readonly IBudgetLineRepository budgetLineRepository;
	private readonly IUnitOfWork unitOfWork;

	public BudgetLineEnsurer(IBudgetLineRepository budgetLineRepository, IUnitOfWork unitOfWork)
	{
		this.budgetLineRepository = budgetLineRepository;
		this.unitOfWork = unitOfWork;
	}

	public IReadOnlyDictionary<string, int> EnsureAndGetMap(IReadOnlyList<string> budgetLineNames)
	{
		var names = budgetLineNames.Where(x => !string.IsNullOrWhiteSpace(x))
			.Select(x => x.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		var map = budgetLineRepository.GetAll().ToDictionary(x => Normalize(x.Name), x => x.Id);
		var created = new List<BudgetLine>();
		foreach (var name in names)
		{
			var key = Normalize(name);
			if (map.ContainsKey(key)) continue;
			var budgetLine = new BudgetLine(name);
			budgetLineRepository.Add(budgetLine);
			created.Add(budgetLine);
		}
		if (created.Count > 0)
		{
			unitOfWork.SaveChanges();
			foreach (var budgetLine in created) map[Normalize(budgetLine.Name)] = budgetLine.Id;
		}
		return map;
	}

	private static string Normalize(string value) => value.Trim().ToUpperInvariant();
}
