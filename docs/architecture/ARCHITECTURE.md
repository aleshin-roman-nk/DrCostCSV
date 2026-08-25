# Architecture

## Status and authority

This is the initial architecture map. The only stated architectural intent available when it was written is the repository structure requested for this documentation: a system with Screens, Application, Domain, Infrastructure, and Startup concerns. The detailed architectural narrative was not supplied. Therefore, observations in this file describe the current codebase; they are not decisions to extend automatically.

When a change needs a rule that this document does not establish, record the question and obtain a decision rather than deriving a new architectural rule from legacy code.

## Current system map (inferred from code)

The solution currently contains these projects:

- `Domain`: business entities such as `ExpenseDocument`, `ExpenseDocumentItem`, and `Category`.
- `Application`: use cases, data-transfer types, result types, and abstractions for persistence and reads.
- `Infrastructure`: EF Core/SQLite persistence, readers, repositories, and the Unit of Work implementation.
- `Presentation.Screens`: WinForms-oriented Screens and their supporting presentation types.
- `Startup.WinForms`: composition root, database migration, logging, and the WinForms application entry point.

The current project-reference graph is:

```text
Startup.WinForms -> Application
Startup.WinForms -> Infrastructure
Startup.WinForms -> Presentation.Screens
Infrastructure -> Application -> Domain
Presentation.Screens -> Application -> Domain
```

`Startup.WinForms` registers the other projects in the DI container. Infrastructure supplies implementations of Application abstractions. This is an inference from the current project references and registrations, not an explicit target dependency rule.

## Major boundaries requested by the user

- Screen architecture is documented in [SCREENS.md](SCREENS.md).
- Application boundaries are documented in [APPLICATION.md](APPLICATION.md).
- Validation architecture is documented in [VALIDATION.md](VALIDATION.md).
- Cross-cutting implementation conventions are documented in [CONVENTIONS.md](../CONVENTIONS.md).

## Unresolved architectural questions

1. Which dependency directions are normative, including whether Application may reference a DI package and whether Presentation may reference Application directly?
2. Whether Domain must remain free of framework dependencies and what domain invariants belong there.
3. Whether Infrastructure may expose framework-specific behavior through Application ports.
4. Whether project boundaries above are the intended target or merely the present legacy arrangement.
