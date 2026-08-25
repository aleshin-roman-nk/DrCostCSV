using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public sealed class ExpenseDocumentViewModelVerifier
	: IViewModelVerifier<ExpenseDocumentViewModel>
{
	public ValidationResult Verify(ExpenseDocumentViewModel document)
	{
		var result = new ValidationResult();

		if (string.IsNullOrWhiteSpace(document.Seller))
			result.Errors.Add("Укажите продавца.");

		if (document.Date == default)
			result.Errors.Add("Укажите дату документа.");

		if (document.Items is null || document.Items.Count == 0)
			result.Errors.Add("Добавьте хотя бы одну позицию документа.");

		if (document.Items is null)
			return result;

		for (var index = 0; index < document.Items.Count; index++)
		{
			var item = document.Items[index];

			if (string.IsNullOrWhiteSpace(item.Name))
				result.Errors.Add($"Строка {index + 1}: укажите наименование.");

		}

		return result;
	}
}
