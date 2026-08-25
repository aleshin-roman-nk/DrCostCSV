using Application.BudgetTags.Abstractions;
using Application.Common;
using Application.Common.Abstractions.Persistence;

namespace Application.BudgetTags.ManageBudgetTag;

public sealed class DeleteBudgetTagUseCase
{
	private readonly IBudgetTagRepository repository;
	private readonly IUnitOfWork unitOfWork;

	public DeleteBudgetTagUseCase(IBudgetTagRepository repository, IUnitOfWork unitOfWork)
	{
		this.repository = repository;
		this.unitOfWork = unitOfWork;
	}

	public UseCaseResult Execute(int id)
	{
		var tag = repository.GetAll().SingleOrDefault(x => x.Id == id);
		if (tag is null)
			return UseCaseResult.Failure(new UseCaseError("budget_tag_not_found", "Уточняющий тег не найден."));
		if (repository.IsUsedByDocumentItem(id))
			return UseCaseResult.Failure(new UseCaseError("budget_tag_in_use", "Нельзя удалить уточняющий тег: он используется в позиции документа."));

		repository.Remove(tag);
		unitOfWork.SaveChanges();
		return UseCaseResult.Success();
	}
}
