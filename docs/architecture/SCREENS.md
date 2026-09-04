# Screen Architecture

## Definition and terminology

A **Screen** is a **presentation screen**: a logical module of user interaction in the presentation layer. It is not merely a WinForms `Form`.

A Screen contains the presentation components required for one user scenario or logical UI context:

```text
Screen = IView + Form + Presenter + Flow + Actions + ViewModels
```

A Screen must have a Presenter. A Form-only implementation is not a Screen and must not be used to bypass this requirement. Other parts are included when required by the Screen's scenario; a missing part in existing code is not precedent for new code unless an explicit exception is documented.

Each Screen has its own folder in `Presentation.Screens`. Nested folders express logical belonging in the user scenario; they do not by themselves define object lifetime.

## New Screen prerequisite

Before an agent implements a Screen from scratch, the user must first create that Screen's folder and manually create its `XXXXForm` in the folder. The user must complete the Form design and create the controls, including the control fields/variables that the implementation task will use.

An agent must not create the Form, design the UI, or add missing controls as part of implementing a new Screen. If the task needs a control that is absent from the supplied Form, the agent must stop and require the user to add that control manually before continuing.

## Component responsibilities

### IView

`IView` is the contract between a Presenter and its concrete UI. It exposes the UI events that the Presenter handles and the operations through which the Presenter updates the UI.

The Presenter must work with the UI through `IView`, not directly through a WinForms `Form`.

### Form

A Form is the concrete WinForms implementation of `IView`. It displays UI, reads user input, and raises UI events.

A Form must not contain business logic, application orchestration, calls to Application use cases, or navigation to another Screen.

### Presenter

A Presenter orchestrates its Screen. It coordinates the `IView`, the Screen's Actions, and Flows of other Screens when the user scenario requires another Screen.

Every Screen must have a Presenter. A Presenter must not call Application use cases directly, work with a concrete Form directly, or create/resolve another Screen's Presenter directly.

### Action

An Action is a presentation-side adapter to Application use cases, not an Application service. It calls one or more Application use cases and maps Application DTOs/results into presentation models needed by the Presenter and `IView`.

Every public Action method must return `ActionResult<TViewModel>` or `ActionResult`. Mapping from Application DTOs to presentation ViewModels occurs inside the Action. `ActionResult` is the contract between Action and Presenter; an Action must not return `ScreenResult`.

An Action must not orchestrate a Form or a Screen navigation.

### ViewModel

ViewModels are presentation-layer models for UI display, user input, or state of a particular Screen. They must reflect presentation semantics.

Application DTOs must not automatically be used as ViewModels when their semantics differ.

### Flow

A Flow is the external entry point to a Screen and the boundary for transitions between Screens. It starts the Screen inside the appropriate Screen scope.

A Screen that starts another Screen must depend on and call that Screen's Flow; it must not access the target Screen's Presenter directly.

## Interaction rules

The normal path from user interaction to application behavior is:

```text
Form / IView -> Presenter -> Action -> Application use case
```

The normal path between Screens is:

```text
Presenter A -> Flow B -> [Screen B scope: Presenter B + IView/Form B + Screen dependencies]
```

`Screen B` denotes the target Screen as a logical presentation module, not a class, file, or folder named `Screen B`. `Flow B` is its external entry point: it creates the Screen's scope through `IScreenScopedExecutor`, resolves `Presenter B` in that scope, and runs it. The Presenter then orchestrates the target Screen through its `IView`.

Application must not depend on or know about Screens, Presenters, Flows, Forms, Views, or ViewModels.

## Screen graph

Screens form a user-navigation and scenario-launch graph. `Main` is the root Screen: its Form is the main application window and is started at application startup. A parent Screen may start child Screens through their Flows; child Screens may in turn start their own child Screens.

## Scope model

Screen scope and use-case scope are distinct architectural operations, even if their implementations share low-level DI scope creation.

`IScreenScopedExecutor` is used by a Flow to create the scope for a Screen lifetime and run that Screen's Presenter, View, and Screen dependencies within it.

```text
Flow -> IScreenScopedExecutor -> DI scope -> Presenter + View + Screen dependencies
```

Every nested Screen must have its own Flow. That Flow must create a separate Screen scope for the nested Screen; a nested Screen must not share its parent Screen's scope.

`IUseCaseScopedExecutor` is used by an Action to create a short-lived scope for one Application use-case invocation and its persistence dependencies.

```text
Presenter -> Action -> IUseCaseScopedExecutor -> DI scope -> Application use case -> persistence dependencies
```

Flows use `IScreenScopedExecutor`; Actions use `IUseCaseScopedExecutor`. The previous generic `IScopedExecutor` has been replaced because the two roles are not architecturally the same.

## Current code observations and exceptions

- `ExpenseDocumentEditFlow` and `ExpenseDocumentListFlow` use `IScreenScopedExecutor` to resolve and run scoped Presenters.
- `MainActions`, `ExpenseDocumentListActions`, and `ExpenseDocumentEditActions` use `IUseCaseScopedExecutor` for short use-case invocations.
- `ExpenseDocumentItemEditFlow` uses `IScreenScopedExecutor` to create its own Screen scope.
- `ExpenseDocumentJsonImport` uses `ExpenseDocumentJsonImportFlow`, `ExpenseDocumentJsonImportPresenter`, `IExpenseDocumentJsonImportView`, and `ExpenseDocumentJsonImportForm`.
- `ExpenseDocumentListFlow.Run` discards the `ScreenResult` returned by its Presenter.

Do not extend these exceptions in new code unless a task explicitly requires compatibility.

## Unresolved decisions

1. Must every Flow return a common `ScreenResult`, including list/read-only Screens, and what are its cancellation and failure semantics?
2. Which UI-local work is permitted in a Form, particularly data parsing and UI-only calculated values?
