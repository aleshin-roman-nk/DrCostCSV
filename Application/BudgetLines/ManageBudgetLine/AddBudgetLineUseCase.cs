using Application.BudgetLines.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;
using Domain;

namespace Application.BudgetLines.ManageBudgetLine;

public sealed class AddBudgetLineUseCase
{
	private readonly IBudgetLineRepository repository;
	private readonly IUnitOfWork unitOfWork;

	public AddBudgetLineUseCase(IBudgetLineRepository repository, IUnitOfWork unitOfWork)
	{
		this.repository = repository;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult Execute(string name)
	{
		var normalizedName = name.Trim();
		if (string.IsNullOrWhiteSpace(normalizedName))
			return UseCaseResult.Failure(new UseCaseError("budget_line_name_required", "Введите название строки бюджета."));
		if (normalizedName.Length > 100)
			return UseCaseResult.Failure(new UseCaseError("budget_line_name_too_long", "Название строки бюджета не должно быть длиннее 100 символов."));
		if (repository.GetAll().Any(x => string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
			return UseCaseResult.Failure(new UseCaseError("budget_line_exists", "Такая строка бюджета уже существует."));

		repository.Add(new BudgetLine(normalizedName));
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
