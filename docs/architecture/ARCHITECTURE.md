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

`Startup.WinForms` composes the DI modules exposed by the other projects. Infrastructure supplies implementations of Application abstractions. Except for the DI ownership rules below, this is an inference from the current project references and not an explicit target dependency rule.

## Dependency injection registration ownership

This section is normative.

Every DI registration must be declared by the project that owns the concrete implementation being registered. An interface-to-implementation mapping belongs to the project containing the implementation, not to the project containing the interface or the implementation's consumers.

- `Application.DependencyInjection` registers Application use cases and concrete Application services.
- `Infrastructure.DependencyInjection` (or a feature-specific Infrastructure DI extension) registers repository, reader, Unit of Work, database-context, persistence, and external-adapter implementations supplied by Infrastructure.
- `Presentation.Screens.DependencyInjection` registers Forms, Views, Presenters, Actions, Flows, and other presentation implementations.
- `Startup.WinForms` is the composition root. It invokes the projects' DI extensions and may configure host-owned concerns, but it must not take ownership of registrations belonging to Application, Infrastructure, or Presentation.

A service must not be registered in another project merely because it depends on that project's services. In particular, an Application use case remains registered by Application even when all of its ports are implemented by Infrastructure.

Framework and third-party services are registered by the project that configures and uses them to implement its responsibility. For example, EF Core persistence configuration belongs to Infrastructure, while application-host logging configuration belongs to Startup.

When a required registration has no clear owning project under these rules, stop and record an architectural question instead of placing it in a convenient DI module.

## Major boundaries requested by the user

- Screen architecture is documented in [SCREENS.md](SCREENS.md).
- Application boundaries are documented in [APPLICATION.md](APPLICATION.md).
- Validation architecture is documented in [VALIDATION.md](VALIDATION.md).
- Cross-cutting implementation conventions are documented in [CONVENTIONS.md](../CONVENTIONS.md).

## Unresolved architectural questions

1. Which dependency directions beyond the DI registration ownership rule are normative, including whether Presentation may reference Application directly?
2. Whether Domain must remain free of framework dependencies and what domain invariants belong there.
3. Whether Infrastructure may expose framework-specific behavior through Application ports.
4. Whether project boundaries above are the intended target or merely the present legacy arrangement.
