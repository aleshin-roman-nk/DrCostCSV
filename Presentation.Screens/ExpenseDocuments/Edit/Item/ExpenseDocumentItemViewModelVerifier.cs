using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.Item;

public sealed class ExpenseDocumentItemViewModelVerifier
	: IViewModelVerifier<ExpenseDocumentItemViewModel>
{
	public ValidationResult Verify(ExpenseDocumentItemViewModel item)
	{
		var result = new ValidationResult();

		if (string.IsNullOrWhiteSpace(item.Name))
			result.Errors.Add("Укажите наименование позиции.");

		if (item.Amount <= 0)
			result.Errors.Add("Количество позиции должно быть больше нуля.");

		if (!item.BudgetLineId.HasValue && string.IsNullOrWhiteSpace(item.BudgetLineName))
			result.Errors.Add("Укажите строку бюджета.");

		return result;
	}
}
