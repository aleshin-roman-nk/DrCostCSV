using Application.BudgetLines.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;

namespace Application.BudgetLines.ManageBudgetLine;

public sealed class DeleteBudgetLineUseCase
{
	private readonly IBudgetLineRepository repository;
	private readonly IUnitOfWork unitOfWork;

	public DeleteBudgetLineUseCase(IBudgetLineRepository repository, IUnitOfWork unitOfWork)
	{
		this.repository = repository;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult Execute(int id)
	{
		var line = repository.GetAll().SingleOrDefault(x => x.Id == id);
		if (line is null)
			return UseCaseResult.Failure(new UseCaseError("budget_line_not_found", "Строка бюджета не найдена."));
		if (repository.IsUsedByDocumentItem(id))
			return UseCaseResult.Failure(new UseCaseError("budget_line_in_use", "Нельзя удалить строку бюджета: она используется в позиции документа или её теге."));

		repository.Remove(line);
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
