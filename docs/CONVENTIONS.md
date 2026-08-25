# Conventions

## Status

These are initial implementation conventions derived from the current repository structure and standard .NET usage. They do not establish new architectural responsibilities; architecture is defined only in `docs/architecture` once an explicit decision exists.

## Naming and folders

- Use PascalCase for project, folder, file, type, method, property, and public member names. Use camelCase for local variables and private fields.
- Keep a feature's related application types together under its feature/operation folder, as with `Application/ExpenseDocuments/CreateExpenseDocument`.
- Keep a Screen's Form, view interface, Presenter, Actions, Flow, and view models under its feature folder in `Presentation.Screens` when those roles are applicable.
- Name interfaces with an `I` prefix. Name implementation files after their primary type.
- Keep WinForms designer-generated files (`*.Designer.cs` and `*.resx`) paired with their Form; do not replace manually designed Forms with dynamic UI generation.

## Dependency injection

- Keep registrations in the owning project's `DependencyInjection` extension and compose them in `Startup.WinForms`.
- Match a service's registration lifetime to the documented scope rules. If those rules are unresolved for the change, do not guess; record the question.
- Register an interface mapping when callers depend on an interface. When a singleton Form is also used through an interface, map both resolutions to the same instance if that is required by the screen.

## Coding and implementation

- Enable and respect nullable reference types.
- Prefer explicit result handling at layer boundaries; do not silently discard an unsuccessful result.
- Reuse a representative implementation only after checking that it agrees with the architecture documentation; existing code is not automatically authoritative.
- Make the smallest change needed for the task. Do not add packages, public contracts, or broad refactors without a task requirement.
- Do not modify unrelated code or generated designer code unless the task requires it.

## Verification

- Build affected projects and run relevant tests when they exist.
- Inspect the final diff and confirm unrelated files were not changed.
- Report verification that could not be run; never claim an unexecuted build or test passed.
