---
name: implement-use-case
description: Implement or change one application use case while respecting the repository's documented architecture.
---

# Implement a use case

Read `AGENTS.md`, `docs/architecture/ARCHITECTURE.md`, `docs/architecture/APPLICATION.md`, `docs/architecture/VALIDATION.md` when validation is involved, and `docs/CONVENTIONS.md` before editing.

1. Read the task and inspect the closest existing use case, its callers, and the relevant ports.
2. Identify the use case input, output, required DTOs, result/error behavior, persistence interactions, and transaction boundary from the architecture documentation.
3. If any of these rules are unresolved in the documentation, stop before choosing a new pattern and record the architectural question for the user.
4. Make the smallest feature-local implementation change, including DI registration only when required.
5. Verify the affected projects, run relevant tests if present, and inspect the final diff.

Do not infer a new repository, DTO, Unit of Work, validation, or lifetime convention from legacy code alone.
