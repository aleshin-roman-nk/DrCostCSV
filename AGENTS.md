# DrCostCSV Agent Instructions

## Purpose

This file defines repository-wide rules for AI coding agents.

Do not duplicate detailed architecture documentation here.
Read the relevant documentation before making changes.

## Required documentation

For any structural or architectural change, read:

- `docs/architecture/ARCHITECTURE.md`

For work involving Screens, Presenters, Views, Forms, Flows or Actions, also read:

- `docs/architecture/SCREENS.md`

For work involving use cases, repositories, DTOs, application results or persistence boundaries, also read:

- `docs/architecture/APPLICATION.md`

For work involving validation, also read:

- `docs/architecture/VALIDATION.md`

For any code change, follow:

- `docs/CONVENTIONS.md`

If the prompt references a task file, read that task before implementation.

## Architecture authority

Architecture documentation is normative.

Do not silently introduce an architectural approach that conflicts with the documentation.

If existing code conflicts with documented architecture:

1. do not spread the inconsistent pattern into new code;
2. identify the discrepancy;
3. follow the documented architecture unless the task explicitly requires migration of existing code.

Do not redesign project architecture as part of an unrelated task.

## Change discipline

Make the smallest change required to complete the task.

Do not:

- modify unrelated code;
- perform opportunistic refactoring;
- introduce new architectural abstractions without a demonstrated need;
- replace existing project patterns with framework-preferred patterns;
- add external packages unless the task requires them;
- change public contracts unnecessarily;
- generate a new UI architecture;
- replace manually designed WinForms UI with dynamically generated UI.

Inspect existing implementations before creating a new implementation.

Prefer consistency with an existing correct implementation.

## Git

Do not:

- commit;
- push;
- pull;
- merge;
- rebase;
- reset;
- checkout another branch;
- create branches;
- create tags.

Git may be used only to inspect repository state and diffs.

## Verification

After implementation:

1. build affected projects;
2. run relevant tests if they exist;
3. inspect the final diff;
4. verify that unrelated files were not changed.

Do not claim that a build or test passed unless it was actually executed.

If verification cannot be executed, state exactly what was not verified.

## Skills

Use repository skills when the task matches them:

- `$implement-use-case`
- `$implement-modal-screen`
- `$implement-list-screen`

A skill defines the implementation workflow.
Architecture documents remain the source of architectural rules even when a skill is used.
