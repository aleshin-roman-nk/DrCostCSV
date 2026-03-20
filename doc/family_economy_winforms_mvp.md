# FamilyEconomy — структура проекта и полная минимальная реализация

Ниже — рабочий пример для .NET WinForms в стиле MVP.

Решение разбито на 4 проекта:

- `FamilyEconomy.Domain`
- `FamilyEconomy.Application`
- `FamilyEconomy.Infrastructure`
- `FamilyEconomy.Presentation.WinForms`

Архитектурная идея:

- `Presentation.WinForms` содержит View, Presenter, формы, composition root.
- `Application` содержит use case / сервисы приложения и DTO.
- `Domain` содержит сущности.
- `Infrastructure` содержит in-memory репозитории. Вместо них потом можно поставить EF Core.

---

# 1. Схема «Структура проекта»

```text
Solution FamilyEconomy
│
├── FamilyEconomy.Domain
│   ├── Entities
│   │   ├── ExpenseDocument.cs
│   │   └── ExpenseItem.cs
│   └── Repositories
│       └── IExpenseDocumentRepository.cs
│
├── FamilyEconomy.Application
│   ├── Dto
│   │   ├── CreateExpenseDocumentRequest.cs
│   │   ├── ExpenseDocumentDto.cs
│   │   ├── ExpenseDocumentItemDto.cs
│   │   ├── ExpenseSummaryRowDto.cs
│   │   └── SaveExpenseDocumentItemRequest.cs
│   └── Services
│       └── ExpenseDocumentService.cs
│
├── FamilyEconomy.Infrastructure
│   └── Repositories
│       └── InMemoryExpenseDocumentRepository.cs
│
└── FamilyEconomy.Presentation.WinForms
    ├── Contracts
    │   ├── IDocumentEditView.cs
    │   └── IMainView.cs
    │
    ├── UiModels
    │   ├── DocumentItemVm.cs
    │   └── ExpenseSummaryRowVm.cs
    │
    ├── Presenters
    │   ├── DocumentEditPresenter.cs
    │   └── MainPresenter.cs
    │
    ├── Forms
    │   ├── DocumentEditForm.cs
    │   └── MainForm.cs
    │
    ├── Composition
    │   └── ScreenFactory.cs
    │
    └── Program.cs
```

---

# 2. Способ взаимодействия presenter-view единиц

```text
[MainView] <--> [MainPresenter] ---> [ExpenseDocumentService] ---> [Repository]
      |
      | кнопка "Создать документ"
      v
[ScreenFactory] creates
      v
[DocumentEditView] <--> [DocumentEditPresenter] ---> [ExpenseDocumentService] ---> [Repository]
      |
      | Save clicked
      v
Document saved
      |
      | success callback / event
      v
[MainPresenter.Refresh()]
      v
Grid "Расходы по категориям" обновляется
```

Ключевой принцип:

- `MainPresenter` не создает форму напрямую через `new DocumentEditForm()`.
- Для открытия вторичного экрана используется `ScreenFactory`.
- После успешного сохранения `DocumentEditPresenter` вызывает callback `onSaved`.
- Этот callback вызывает `MainPresenter.LoadSummary()`.

---

# 3. Ограниченный цикл жизни экрана Create/Edit

Экран `Создать / редактировать документ` живет временно:

1. Пользователь нажал `Создать документ`.
2. `MainPresenter` вызывает фабрику.
3. Фабрика создает:
   - `DocumentEditForm`
   - `DocumentEditPresenter`
4. Окно показывается как модальное через `ShowDialog()`.
5. После закрытия окна `using`/`Dispose()` освобождает ресурсы формы.
6. Если презентер больше нигде не удерживается ссылкой, он также становится доступным для GC.

То есть связка presenter-view создается только на время сценария.

---

# 4. Полный код

## 4.1 FamilyEconomy.Domain

### Entities/ExpenseItem.cs
```csharp
namespace FamilyEconomy.Domain.Entities;

public sealed class ExpenseItem
{
    public ExpenseItem(string name, decimal amount, string categoryName)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(categoryName))
            throw new ArgumentException("Category name is required.", nameof(categoryName));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

        Name = name;
        Amount = amount;
        CategoryName = categoryName;
    }

    public string Name { get; }
    public decimal Amount { get; }
    public string CategoryName { get; }
}
```

### Entities/ExpenseDocument.cs
```csharp
namespace FamilyEconomy.Domain.Entities;

public sealed class ExpenseDocument
{
    private readonly List<ExpenseItem> _items = new();

    public ExpenseDocument(int id, string sellerName, DateOnly date, IReadOnlyCollection<ExpenseItem> items)
    {
        if (string.IsNullOrWhiteSpace(sellerName))
            throw new ArgumentException("Seller name is required.", nameof(sellerName));

        if (items is null || items.Count == 0)
            throw new ArgumentException("Document must contain at least one item.", nameof(items));

        Id = id;
        SellerName = sellerName;
        Date = date;
        _items.AddRange(items);
    }

    public int Id { get; }
    public string SellerName { get; }
    public DateOnly Date { get; }
    public IReadOnlyList<ExpenseItem> Items => _items;
}
```

### Repositories/IExpenseDocumentRepository.cs
```csharp
using FamilyEconomy.Domain.Entities;

namespace FamilyEconomy.Domain.Repositories;

public interface IExpenseDocumentRepository
{
    int GetNextId();
    void Add(ExpenseDocument document);
    IReadOnlyList<ExpenseDocument> GetAll();
}
```

---

## 4.2 FamilyEconomy.Application

### Dto/SaveExpenseDocumentItemRequest.cs
```csharp
namespace FamilyEconomy.Application.Dto;

public sealed class SaveExpenseDocumentItemRequest
{
    public string Name { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}
```

### Dto/CreateExpenseDocumentRequest.cs
```csharp
namespace FamilyEconomy.Application.Dto;

public sealed class CreateExpenseDocumentRequest
{
    public string SellerName { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public IReadOnlyList<SaveExpenseDocumentItemRequest> Items { get; init; } = [];
}
```

### Dto/ExpenseDocumentItemDto.cs
```csharp
namespace FamilyEconomy.Application.Dto;

public sealed class ExpenseDocumentItemDto
{
    public string Name { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}
```

### Dto/ExpenseDocumentDto.cs
```csharp
namespace FamilyEconomy.Application.Dto;

public sealed class ExpenseDocumentDto
{
    public int Id { get; init; }
    public string SellerName { get; init; } = string.Empty;
    public DateOnly Date { get; init; }
    public IReadOnlyList<ExpenseDocumentItemDto> Items { get; init; } = [];
}
```

### Dto/ExpenseSummaryRowDto.cs
```csharp
namespace FamilyEconomy.Application.Dto;

public sealed class ExpenseSummaryRowDto
{
    public string CategoryName { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
}
```

### Services/ExpenseDocumentService.cs
```csharp
using FamilyEconomy.Application.Dto;
using FamilyEconomy.Domain.Entities;
using FamilyEconomy.Domain.Repositories;

namespace FamilyEconomy.Application.Services;

public sealed class ExpenseDocumentService
{
    private readonly IExpenseDocumentRepository _repository;

    public ExpenseDocumentService(IExpenseDocumentRepository repository)
    {
        _repository = repository;
    }

    public int Create(CreateExpenseDocumentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.SellerName))
            throw new InvalidOperationException("Seller name is required.");

        if (request.Items.Count == 0)
            throw new InvalidOperationException("At least one item is required.");

        var items = request.Items
            .Select(x => new ExpenseItem(x.Name, x.Amount, x.CategoryName))
            .ToList();

        var id = _repository.GetNextId();
        var document = new ExpenseDocument(id, request.SellerName, request.Date, items);

        _repository.Add(document);
        return id;
    }

    public IReadOnlyList<ExpenseSummaryRowDto> GetExpenseSummaryByCategory()
    {
        return _repository
            .GetAll()
            .SelectMany(d => d.Items)
            .GroupBy(x => x.CategoryName)
            .Select(g => new ExpenseSummaryRowDto
            {
                CategoryName = g.Key,
                TotalAmount = g.Sum(x => x.Amount)
            })
            .OrderBy(x => x.CategoryName)
            .ToList();
    }

    public IReadOnlyList<ExpenseDocumentDto> GetAllDocuments()
    {
        return _repository.GetAll()
            .Select(d => new ExpenseDocumentDto
            {
                Id = d.Id,
                SellerName = d.SellerName,
                Date = d.Date,
                Items = d.Items.Select(i => new ExpenseDocumentItemDto
                {
                    Name = i.Name,
                    Amount = i.Amount,
                    CategoryName = i.CategoryName
                }).ToList()
            })
            .ToList();
    }
}
```

---

## 4.3 FamilyEconomy.Infrastructure

### Repositories/InMemoryExpenseDocumentRepository.cs
```csharp
using FamilyEconomy.Domain.Entities;
using FamilyEconomy.Domain.Repositories;

namespace FamilyEconomy.Infrastructure.Repositories;

public sealed class InMemoryExpenseDocumentRepository : IExpenseDocumentRepository
{
    private readonly List<ExpenseDocument> _documents = new();
    private int _nextId = 1;

    public int GetNextId()
    {
        return _nextId++;
    }

    public void Add(ExpenseDocument document)
    {
        _documents.Add(document);
    }

    public IReadOnlyList<ExpenseDocument> GetAll()
    {
        return _documents.ToList();
    }
}
```

---

## 4.4 FamilyEconomy.Presentation.WinForms

### Contracts/IMainView.cs
```csharp
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Contracts;

public interface IMainView
{
    event EventHandler? CreateDocumentRequested;

    void BindSummary(IReadOnlyList<ExpenseSummaryRowVm> rows);
    void ShowView();
}
```

### Contracts/IDocumentEditView.cs
```csharp
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Contracts;

public interface IDocumentEditView
{
    event EventHandler? SaveRequested;
    event EventHandler? AddItemRequested;

    string SellerName { get; }
    DateTime DocumentDate { get; }

    string NewItemName { get; }
    decimal NewItemAmount { get; }
    string NewItemCategoryName { get; }

    IReadOnlyList<DocumentItemVm> Items { get; }

    void AddItemToGrid(DocumentItemVm item);
    void ClearNewItemInputs();
    void ShowError(string message);
    void CloseView();
    DialogResult ShowModal();
}
```

### UiModels/ExpenseSummaryRowVm.cs
```csharp
namespace FamilyEconomy.Presentation.WinForms.UiModels;

public sealed class ExpenseSummaryRowVm
{
    public string CategoryName { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
}
```

### UiModels/DocumentItemVm.cs
```csharp
namespace FamilyEconomy.Presentation.WinForms.UiModels;

public sealed class DocumentItemVm
{
    public string Name { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string CategoryName { get; init; } = string.Empty;
}
```

### Presenters/MainPresenter.cs
```csharp
using FamilyEconomy.Application.Services;
using FamilyEconomy.Presentation.WinForms.Composition;
using FamilyEconomy.Presentation.WinForms.Contracts;
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Presenters;

public sealed class MainPresenter
{
    private readonly IMainView _view;
    private readonly ExpenseDocumentService _service;
    private readonly ScreenFactory _screenFactory;

    public MainPresenter(
        IMainView view,
        ExpenseDocumentService service,
        ScreenFactory screenFactory)
    {
        _view = view;
        _service = service;
        _screenFactory = screenFactory;

        _view.CreateDocumentRequested += OnCreateDocumentRequested;
    }

    public void Initialize()
    {
        LoadSummary();
    }

    private void OnCreateDocumentRequested(object? sender, EventArgs e)
    {
        _screenFactory.ShowCreateDocumentDialog(onSaved: LoadSummary);
    }

    private void LoadSummary()
    {
        var rows = _service.GetExpenseSummaryByCategory()
            .Select(x => new ExpenseSummaryRowVm
            {
                CategoryName = x.CategoryName,
                TotalAmount = x.TotalAmount
            })
            .ToList();

        _view.BindSummary(rows);
    }
}
```

### Presenters/DocumentEditPresenter.cs
```csharp
using FamilyEconomy.Application.Dto;
using FamilyEconomy.Application.Services;
using FamilyEconomy.Presentation.WinForms.Contracts;
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Presenters;

public sealed class DocumentEditPresenter
{
    private readonly IDocumentEditView _view;
    private readonly ExpenseDocumentService _service;
    private readonly Action _onSaved;

    public DocumentEditPresenter(
        IDocumentEditView view,
        ExpenseDocumentService service,
        Action onSaved)
    {
        _view = view;
        _service = service;
        _onSaved = onSaved;

        _view.AddItemRequested += OnAddItemRequested;
        _view.SaveRequested += OnSaveRequested;
    }

    private void OnAddItemRequested(object? sender, EventArgs e)
    {
        try
        {
            var item = new DocumentItemVm
            {
                Name = _view.NewItemName,
                Amount = _view.NewItemAmount,
                CategoryName = _view.NewItemCategoryName
            };

            ValidateItem(item);

            _view.AddItemToGrid(item);
            _view.ClearNewItemInputs();
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
        }
    }

    private void OnSaveRequested(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_view.SellerName))
                throw new InvalidOperationException("Введите наименование продавца.");

            if (_view.Items.Count == 0)
                throw new InvalidOperationException("Добавьте хотя бы одну позицию документа.");

            var request = new CreateExpenseDocumentRequest
            {
                SellerName = _view.SellerName,
                Date = DateOnly.FromDateTime(_view.DocumentDate),
                Items = _view.Items
                    .Select(x => new SaveExpenseDocumentItemRequest
                    {
                        Name = x.Name,
                        Amount = x.Amount,
                        CategoryName = x.CategoryName
                    })
                    .ToList()
            };

            _service.Create(request);
            _onSaved();
            _view.CloseView();
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
        }
    }

    private static void ValidateItem(DocumentItemVm item)
    {
        if (string.IsNullOrWhiteSpace(item.Name))
            throw new InvalidOperationException("Введите наименование позиции.");

        if (string.IsNullOrWhiteSpace(item.CategoryName))
            throw new InvalidOperationException("Введите категорию.");

        if (item.Amount <= 0)
            throw new InvalidOperationException("Сумма должна быть больше нуля.");
    }
}
```

### Composition/ScreenFactory.cs
```csharp
using FamilyEconomy.Application.Services;
using FamilyEconomy.Presentation.WinForms.Forms;
using FamilyEconomy.Presentation.WinForms.Presenters;

namespace FamilyEconomy.Presentation.WinForms.Composition;

public sealed class ScreenFactory
{
    private readonly ExpenseDocumentService _expenseDocumentService;

    public ScreenFactory(ExpenseDocumentService expenseDocumentService)
    {
        _expenseDocumentService = expenseDocumentService;
    }

    public void ShowCreateDocumentDialog(Action onSaved)
    {
        using var form = new DocumentEditForm();
        var presenter = new DocumentEditPresenter(form, _expenseDocumentService, onSaved);
        form.ShowModal();
    }
}
```

### Forms/MainForm.cs
```csharp
using FamilyEconomy.Presentation.WinForms.Contracts;
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Forms;

public sealed class MainForm : Form, IMainView
{
    private readonly DataGridView _summaryGrid;
    private readonly Button _createDocumentButton;

    public MainForm()
    {
        Text = "Family Economy";
        Width = 800;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;

        _summaryGrid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AutoGenerateColumns = true,
            AllowUserToAddRows = false
        };

        _createDocumentButton = new Button
        {
            Text = "Создать документ",
            Dock = DockStyle.Top,
            Height = 40
        };

        _createDocumentButton.Click += (_, _) => CreateDocumentRequested?.Invoke(this, EventArgs.Empty);

        Controls.Add(_summaryGrid);
        Controls.Add(_createDocumentButton);
    }

    public event EventHandler? CreateDocumentRequested;

    public void BindSummary(IReadOnlyList<ExpenseSummaryRowVm> rows)
    {
        _summaryGrid.DataSource = null;
        _summaryGrid.DataSource = rows.ToList();
    }

    public void ShowView()
    {
        Application.Run(this);
    }
}
```

### Forms/DocumentEditForm.cs
```csharp
using System.ComponentModel;
using FamilyEconomy.Presentation.WinForms.Contracts;
using FamilyEconomy.Presentation.WinForms.UiModels;

namespace FamilyEconomy.Presentation.WinForms.Forms;

public sealed class DocumentEditForm : Form, IDocumentEditView
{
    private readonly TextBox _sellerNameTextBox;
    private readonly DateTimePicker _datePicker;

    private readonly TextBox _itemNameTextBox;
    private readonly NumericUpDown _amountNumeric;
    private readonly TextBox _categoryTextBox;
    private readonly Button _addItemButton;
    private readonly Button _saveButton;
    private readonly DataGridView _itemsGrid;

    private readonly BindingList<DocumentItemVm> _items = new();

    public DocumentEditForm()
    {
        Text = "Создать / редактировать документ";
        Width = 900;
        Height = 600;
        StartPosition = FormStartPosition.CenterParent;

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(10)
        };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var headerPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        _sellerNameTextBox = new TextBox { Width = 220, PlaceholderText = "Наименование продавца" };
        _datePicker = new DateTimePicker { Width = 160 };

        headerPanel.Controls.Add(new Label { Text = "Продавец", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
        headerPanel.Controls.Add(_sellerNameTextBox);
        headerPanel.Controls.Add(new Label { Text = "Дата", AutoSize = true, Padding = new Padding(10, 8, 0, 0) });
        headerPanel.Controls.Add(_datePicker);

        var itemInputPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        _itemNameTextBox = new TextBox { Width = 220, PlaceholderText = "Позиция" };
        _amountNumeric = new NumericUpDown
        {
            Width = 120,
            DecimalPlaces = 2,
            Maximum = 1000000,
            Minimum = 0
        };
        _categoryTextBox = new TextBox { Width = 180, PlaceholderText = "Категория" };
        _addItemButton = new Button { Text = "Добавить позицию", Width = 160, Height = 30 };
        _addItemButton.Click += (_, _) => AddItemRequested?.Invoke(this, EventArgs.Empty);

        itemInputPanel.Controls.Add(new Label { Text = "Наименование", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
        itemInputPanel.Controls.Add(_itemNameTextBox);
        itemInputPanel.Controls.Add(new Label { Text = "Сумма", AutoSize = true, Padding = new Padding(10, 8, 0, 0) });
        itemInputPanel.Controls.Add(_amountNumeric);
        itemInputPanel.Controls.Add(new Label { Text = "Категория", AutoSize = true, Padding = new Padding(10, 8, 0, 0) });
        itemInputPanel.Controls.Add(_categoryTextBox);
        itemInputPanel.Controls.Add(_addItemButton);

        _itemsGrid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = true,
            AllowUserToAddRows = false,
            ReadOnly = true,
            DataSource = _items
        };

        var bottomPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            AutoSize = true
        };

        _saveButton = new Button { Text = "Сохранить документ", Width = 180, Height = 35 };
        _saveButton.Click += (_, _) => SaveRequested?.Invoke(this, EventArgs.Empty);
        bottomPanel.Controls.Add(_saveButton);

        root.Controls.Add(headerPanel, 0, 0);
        root.Controls.Add(itemInputPanel, 0, 1);
        root.Controls.Add(_itemsGrid, 0, 2);
        root.Controls.Add(bottomPanel, 0, 3);

        Controls.Add(root);
    }

    public event EventHandler? SaveRequested;
    public event EventHandler? AddItemRequested;

    public string SellerName => _sellerNameTextBox.Text.Trim();
    public DateTime DocumentDate => _datePicker.Value;

    public string NewItemName => _itemNameTextBox.Text.Trim();
    public decimal NewItemAmount => _amountNumeric.Value;
    public string NewItemCategoryName => _categoryTextBox.Text.Trim();

    public IReadOnlyList<DocumentItemVm> Items => _items.ToList();

    public void AddItemToGrid(DocumentItemVm item)
    {
        _items.Add(item);
    }

    public void ClearNewItemInputs()
    {
        _itemNameTextBox.Clear();
        _amountNumeric.Value = 0;
        _categoryTextBox.Clear();
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void CloseView()
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    public DialogResult ShowModal()
    {
        return ShowDialog();
    }
}
```

### Program.cs
```csharp
using FamilyEconomy.Application.Services;
using FamilyEconomy.Domain.Repositories;
using FamilyEconomy.Infrastructure.Repositories;
using FamilyEconomy.Presentation.WinForms.Composition;
using FamilyEconomy.Presentation.WinForms.Forms;
using FamilyEconomy.Presentation.WinForms.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyEconomy.Presentation.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        ConfigureServices(services);

        using var provider = services.BuildServiceProvider();

        var mainForm = provider.GetRequiredService<MainForm>();
        var mainPresenter = provider.GetRequiredService<MainPresenter>();

        mainPresenter.Initialize();
        Application.Run(mainForm);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IExpenseDocumentRepository, InMemoryExpenseDocumentRepository>();
        services.AddSingleton<ExpenseDocumentService>();
        services.AddSingleton<ScreenFactory>();

        services.AddSingleton<MainForm>();
        services.AddSingleton<MainPresenter>(sp =>
            new MainPresenter(
                sp.GetRequiredService<MainForm>(),
                sp.GetRequiredService<ExpenseDocumentService>(),
                sp.GetRequiredService<ScreenFactory>()));
    }
}
```

---

# 5. Как работает сценарий сохранения

## Сценарий

1. Пользователь в `MainForm` нажимает `Создать документ`.
2. `MainPresenter` получает событие `CreateDocumentRequested`.
3. `MainPresenter` вызывает `_screenFactory.ShowCreateDocumentDialog(onSaved: LoadSummary)`.
4. `ScreenFactory` создает `DocumentEditForm` и `DocumentEditPresenter`.
5. Пользователь вводит:
   - продавца
   - дату
   - позиции
6. Нажимает `Сохранить документ`.
7. `DocumentEditPresenter` собирает данные из view.
8. Вызывает `ExpenseDocumentService.Create(...)`.
9. Сервис сохраняет документ в репозиторий.
10. `DocumentEditPresenter` вызывает `_onSaved()`.
11. Это приводит к вызову `MainPresenter.LoadSummary()`.
12. `MainView.BindSummary(...)` обновляет Grid `Расходы по категориям`.
13. Диалог закрывается.

---

# 6. Что здесь является правильным с точки зрения MVP

Правильно сделано следующее:

- форма не содержит бизнес-логики сохранения;
- форма не обращается в репозиторий;
- presenter читает данные из view и передает их в application service;
- главный экран не знает деталей внутренней реализации экрана редактирования;
- вторичный экран имеет ограниченный жизненный цикл;
- обновление главного экрана происходит через callback, а не через прямую зависимость одного presenter на другой.

---

# 7. Что заменить в следующем шаге

Для production-версии заменить:

- `InMemoryExpenseDocumentRepository` → EF Core repository
- `ExpenseDocumentService` → use cases / commands / queries
- callback `Action onSaved` → event bus / mediator / application event
- ручное создание `DocumentEditPresenter` в `ScreenFactory` → фабрика через DI
- `DocumentEditForm` можно расширить режимом `Create/Edit`

---

# 8. Итоговая краткая схема жизненного цикла

```text
MainForm + MainPresenter    = живут всё время работы приложения
DocumentEditForm + Presenter = создаются при открытии окна
                               уничтожаются после закрытия окна
```

Именно так обычно и организуют подобный сценарий в WinForms MVP.

