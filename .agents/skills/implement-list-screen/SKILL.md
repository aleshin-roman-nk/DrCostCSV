---
name: implement-list-screen
description: Implement or change one list-oriented WinForms Screen while following the documented Screen architecture.
---

# Implement a list screen

Read `AGENTS.md`, `docs/architecture/ARCHITECTURE.md`, `docs/architecture/SCREENS.md`, `docs/architecture/APPLICATION.md` when calling use cases, `docs/architecture/VALIDATION.md` when validation is involved, and `docs/CONVENTIONS.md` before editing.

1. Read the task and inspect the closest list Screen, its row view models, loading action, presenter, view, form, flow, and callers.
2. Establish the list input, row model, loading/error behavior, selection/navigation behavior, refresh behavior, and Screen result from the task and architecture documentation.
3. Check scope ownership and DI lifetimes before introducing a Flow, Actions class, or scoped service resolution.
4. If these responsibilities or contracts are unresolved, record the question and ask for a decision rather than treating existing Screen code as authority.
5. Implement only the requested feature-local changes; preserve manually designed WinForms Forms unless UI changes are requested.
6. Build the affected projects, run relevant tests if present, and inspect the final diff.
