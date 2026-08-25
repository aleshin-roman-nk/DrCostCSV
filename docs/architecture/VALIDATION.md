# Validation Architecture

## Status and scope

The requested validation concepts are `IValidator`, validator composition, and the responsibilities of Presenter, Application, and Domain validation. No ownership rules or validator contract were supplied. This initial document therefore does not assign new responsibilities; it records what exists and the decisions needed before implementation can be standardized.

## Current implementation (inferred)

- `Presentation.Screens.Common` contains an empty `IViewModelValidator` marker interface and a `ValidationResult` with a list of strings. No implementation or composition mechanism was found.
- `ExpenseDocumentEditPresenter` has a private `ValidateDocument` method and displays returned messages through the view.
- Create and update expense-document use cases each have private `ValidateCommand` methods that return `CommandValidationResult`, which wraps a `UseCaseError`.
- `ExpenseDocument` and `ExpenseDocumentItem` reject invalid item data by throwing argument exceptions from aggregate methods.

The existing checks do not fully agree: the presenter comments out price, amount, and category validation, while the corresponding use cases and domain methods enforce those constraints. This is a discrepancy, not a documented rule.

## Unresolved architectural questions

1. Is `IValidator` intended to replace `IViewModelValidator`, and what input/result contract should it have?
2. Which rules are presentation feedback, application command validation, and domain invariants?
3. Must Application validate every command before invoking domain behavior?
4. How are multiple validation errors composed, ordered, localized, and associated with fields or rows?
5. Should validation failures be returned through `UseCaseResult`, thrown, or both at different boundaries?
6. May validation be duplicated across layers for user experience, and how must duplicated rules remain consistent?
