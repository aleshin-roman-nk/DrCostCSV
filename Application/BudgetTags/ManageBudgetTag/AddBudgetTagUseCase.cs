using Application.BudgetLines.Abstractions;
using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Domain;

namespace Application.BudgetTags.ManageBudgetTag;

public sealed class AddBudgetTagUseCase
{
	private readonly IBudgetLineRepository budgetLineRepository;
	private readonly IBudgetTagRepository budgetTagRepository;
	private readonly IUnitOfWork unitOfWork;

	public AddBudgetTagUseCase(IBudgetLineRepository budgetLineRepository, IBudgetTagRepository budgetTagRepository, IUnitOfWork unitOfWork)
	{
		this.budgetLineRepository = budgetLineRepository;
		this.budgetTagRepository = budgetTagRepository;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult Execute(int budgetLineId, string name)
	{
		var normalizedName = name.Trim();
		if (budgetLineRepository.GetAll().All(x => x.Id != budgetLineId))
			return UseCaseResult.Failure(new UseCaseError("budget_line_not_found", "Сначала выберите строку бюджета."));
		if (string.IsNullOrWhiteSpace(normalizedName))
			return UseCaseResult.Failure(new UseCaseError("budget_tag_name_required", "Введите название уточняющего тега."));
		if (normalizedName.Length > 100)
			return UseCaseResult.Failure(new UseCaseError("budget_tag_name_too_long", "Название уточняющего тега не должно быть длиннее 100 символов."));
		if (budgetTagRepository.GetAll().Any(x => x.BudgetLineId == budgetLineId && string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
			return UseCaseResult.Failure(new UseCaseError("budget_tag_exists", "Такой уточняющий тег уже существует у выбранной строки бюджета."));

		budgetTagRepository.Add(new BudgetTag(budgetLineId, normalizedName));
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
