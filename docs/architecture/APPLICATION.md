# Application Architecture

## Status and scope

The requested application concepts are use cases, application boundaries, repositories/ports, DTOs, `UseCaseResult`, Unit of Work, and use-case lifetime. The supplied intent does not define their normative contracts. This file records the current implementation and identifies decisions still required.

## Current implementation (inferred)

Use cases are classes in `Application`, grouped by feature and operation. Their public `Execute` methods accept either a command/query/value input and return `UseCaseResult` or `UseCaseResult<T>`.

The current Application project contains:

- command types and result types next to create/update use cases;
- DTOs for reading data, including editing and reporting;
- repository and reader interfaces under feature `Abstractions` folders;
- `IUnitOfWork` under `Application.Common.Abstractions.Persistence`; and
- an Application DI extension that registers use cases as scoped.

Infrastructure implements Application persistence and read interfaces using EF Core/SQLite. Mutating use cases call repositories to load/add aggregates and call `IUnitOfWork.SaveChanges()`. Query use cases currently depend on reader/report interfaces and return DTOs.

## Current lifetime behavior (inferred)

Use cases, repositories, readers, `IUnitOfWork`, and `AppDbContext` are registered as scoped. `IScopedExecutor` resolves a service inside a newly-created scope for a callback. Because presentation Actions currently invoke it per call, a single presenter interaction may use separate DI scopes.

## Unresolved architectural questions

1. What interface and error contract must every use case expose?
2. Should commands, queries, DTOs, and results be immutable, and where should each be located?
3. Which ports belong in Application, and what distinguishes a repository from a read-model reader/report reader?
4. Which layer owns transaction boundaries and when must Unit of Work save changes?
5. Is `SaveChanges` inside an Application service such as `CategoryEnsurer` permitted, especially when it is called from another use case?
6. Is a use case scoped per screen, per action invocation, or per explicit application operation?
7. How should expected failures, validation failures, exceptions, and not-found results be represented by `UseCaseResult`?
