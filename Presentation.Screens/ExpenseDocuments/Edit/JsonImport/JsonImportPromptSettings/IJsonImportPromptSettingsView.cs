using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;

namespace Presentation.Screens.ExpenseDocuments.Edit.JsonImport.JsonImportPromptSettings;

public interface IJsonImportPromptSettingsView
{
	event Action? CopyRequested;
	event Action? AdditionalInstructionsChanged;
	event Action? SaveRequested;
	event Action? AddBudgetLineRequested;
	event Action? DeleteBudgetLineRequested;
	event Action? AddBudgetTagRequested;
	event Action? DeleteBudgetTagRequested;

	ModalResult ShowModal();
	void SetBudgetClassifications(IReadOnlyList<BudgetLineOptionViewModel> budgetLines, IReadOnlyList<BudgetTagOptionViewModel> budgetTags);
	string GetNewBudgetLineName();
	string GetNewBudgetTagName();
	int? GetSelectedBudgetLineId();
	int? GetSelectedBudgetTagId();
	void ClearBudgetLineInput();
	void ClearBudgetTagInput();
	void SetAdditionalInstructions(string instructions);
	string GetAdditionalInstructions();
	void SetPrompt(string prompt);
	void CopyPromptToClipboard();
	void CloseSuccessfully();
	void ShowError(string message);
}
