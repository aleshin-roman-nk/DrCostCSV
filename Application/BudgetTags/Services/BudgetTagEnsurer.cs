using Application.BudgetTags.Abstractions;
using Application.Common.Abstractions.Persistence;
using Domain;

namespace Application.BudgetTags.Services;

public sealed class BudgetTagEnsurer : IBudgetTagEnsurer
{
	private readonly IBudgetTagRepository budgetTagRepository;
	private readonly IUnitOfWork unitOfWork;
	public BudgetTagEnsurer(IBudgetTagRepository budgetTagRepository, IUnitOfWork unitOfWork)
	{
		this.budgetTagRepository = budgetTagRepository;
		this.unitOfWork = unitOfWork;
	}

	public IReadOnlyDictionary<string, int> EnsureAndGetMap(int budgetLineId, IReadOnlyList<string> tagNames)
	{
		var names = tagNames.Where(x => !string.IsNullOrWhiteSpace(x))
			.Select(x => x.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
		var map = budgetTagRepository.GetAll().Where(x => x.BudgetLineId == budgetLineId)
			.ToDictionary(x => Normalize(x.Name), x => x.Id);
		var created = new List<BudgetTag>();
		foreach (var name in names)
		{
			var key = Normalize(name);
			if (map.ContainsKey(key)) continue;
			var budgetTag = new BudgetTag(budgetLineId, name);
			budgetTagRepository.Add(budgetTag);
			created.Add(budgetTag);
		}
		if (created.Count > 0)
		{
			unitOfWork.SaveChanges();
			foreach (var budgetTag in created) map[Normalize(budgetTag.Name)] = budgetTag.Id;
		}
		return map;
	}

	private static string Normalize(string value) => value.Trim().ToUpperInvariant();
}
