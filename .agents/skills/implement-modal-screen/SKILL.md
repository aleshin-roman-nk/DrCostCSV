---
name: implement-modal-screen
description: Implement or change one modal WinForms Screen while following the documented Screen architecture.
---

# Implement a modal screen

Read `AGENTS.md`, `docs/architecture/ARCHITECTURE.md`, `docs/architecture/SCREENS.md`, `docs/architecture/VALIDATION.md` when validation is involved, `docs/architecture/APPLICATION.md` when calling use cases, and `docs/CONVENTIONS.md` before editing.

1. Read the task and inspect the nearest comparable modal Screen, including its view interface, Form, Presenter, Flow, Actions, and callers.
2. Establish the required input and `ScreenResult`/modal-result behavior from the documentation and task.
3. Check the documented scope and DI rules before selecting a lifetime or resolving a Form/Presenter.
4. If the role split, scope owner, navigation contract, validation responsibility, or result semantics are unresolved, stop and ask for the architectural decision rather than copying a conflicting legacy pattern.
5. Implement only the required Screen artifacts and registrations. Preserve manually designed WinForms designer files unless the task requires UI design changes.
6. Build the affected projects, run relevant tests if present, and inspect the final diff.
