# Application.Contracts

## Назначение

`Application.Contracts` — это библиотека контрактов прикладного слоя.

Она описывает внешний API слоя `Application` для других слоев системы, например:

- `UI.WinForms`
- `Web API`
- тестов
- других клиентов приложения

Эта библиотека содержит только описания того, **как внешний мир взаимодействует с прикладными сценариями**, но не содержит их реализацию.

---

## Зачем нужна эта библиотека

Основная цель — отделить:

- **контракты прикладного слоя**
от
- **внутренней реализации прикладной логики**

Это позволяет:

- проектировать UI от экранов и сценариев
- заранее определить форматы входных и выходных данных
- снизить связанность `UI` с реализацией `Application`
- использовать одинаковые контракты из разных слоев представления
- сделать архитектуру более прозрачной и устойчивой к изменениям

---

## Что должно находиться в Application.Contracts

В библиотеке `Application.Contracts` должны находиться:

### 1. Интерфейсы прикладных сервисов

Описывают доступные сценарии системы.

Примеры:

- `IExpenseDocumentService`
- `IExpenseStatisticsService`
- `IExpenseImportService`

### 2. Query / Command модели

Описывают входные данные прикладных сценариев.

Примеры:

- `GetCategoryExpensesByMonthQuery`
- `GetDailyExpensesQuery`
- `UpdateExpenseDocumentCommand`

### 3. DTO / Result модели

Описывают выходные данные прикладных сценариев.

Примеры:

- `CategoryExpenseDto`
- `DailyExpenseDto`
- `ExpenseDocumentDetailsDto`

### 4. Общие прикладные результаты

Если нужно, здесь могут находиться типы вроде:

- `OperationResult`
- `PagedResult<T>`
- `ValidationError`

---

## Что не должно находиться в Application.Contracts

В этой библиотеке не должно быть:

- доменных сущностей
- агрегатов
- value objects домена
- реализации сервисов
- EF Core entity
- DbContext
- SQL-кода
- WinForms-моделей
- Web API response-моделей, привязанных к ASP.NET
- ссылок на `System.Windows.Forms`
- ссылок на `Microsoft.AspNetCore.Mvc`
- инфраструктурных зависимостей

`Application.Contracts` не должен знать, **как** выполняется сценарий.  
Он должен знать только, **какие данные принимает** и **какие данные возвращает**.

---

## Роль в архитектуре

Типичное направление зависимостей:

```text
UI.WinForms -> Application.Contracts
Application -> Application.Contracts
Infrastructure -> Application
Domain -> (не зависит от Contracts)