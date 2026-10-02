using Application.BudgetLines.Services;
using Application.BudgetTags.Services;
using Application.Currencies.DeleteCurrency;
using Application.CurrencyDefaults;
using Application.ExpenseDocuments.CreateExpenseDocument;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.ExpenseDocuments.UpdateExpenseDocument;
using Domain;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Readers;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Tests;

public sealed class CurrencyDefaultsTests : IDisposable
{
	private readonly SqliteConnection connection;
	private readonly AppDbContext db;
	private readonly EfCurrencyDefaultSettingsRepository defaults;
	private readonly EfUnitOfWork unitOfWork;
	private readonly SaveCurrencyDefaultsUseCase save;
	private readonly int byn;
	private readonly int rub;
	private readonly int usd;

	public CurrencyDefaultsTests()
	{
		connection = new SqliteConnection("Data Source=:memory:;Foreign Keys=True");
		connection.Open();
		db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options);
		db.Database.Migrate();
		var currencies = new[] { new Currency("BYN", "Белорусский рубль"), new Currency("RUB", "Российский рубль"), new Currency("USD", "Доллар") };
		db.Currencies.AddRange(currencies);
		db.BudgetLines.Add(new BudgetLine("Продукты"));
		db.SaveChanges();
		byn = currencies[0].Id;
		rub = currencies[1].Id;
		usd = currencies[2].Id;
		defaults = new EfCurrencyDefaultSettingsRepository(db);
		unitOfWork = new EfUnitOfWork(db, NullLogger<EfUnitOfWork>.Instance);
		save = new SaveCurrencyDefaultsUseCase(defaults, new EfCurrencyReader(db), unitOfWork);
	}

	[Fact]
	public void SaveAndReload_ReplacesRowsAndKeepsDecimalPrecision()
	{
		Assert.True(save.Execute(new(byn, [new(byn, 1m), new(rub, 28.512345678m)])).IsSuccess);
		db.ChangeTracker.Clear();
		Assert.Equal(28.512345678m, defaults.Get()!.CurrencyValues.Single(value => value.CurrencyId == rub).Value);
		Assert.True(save.Execute(new(byn, [new(byn, 2m), new(usd, 0.66m)])).IsSuccess);
		db.ChangeTracker.Clear();
		var result = new GetCurrencyDefaultsUseCase(defaults).Execute().Value!;
		Assert.Equal(byn, result.DocumentCurrencyId);
		Assert.Equal(2, result.CurrencyValues.Count);
		Assert.DoesNotContain(result.CurrencyValues, value => value.CurrencyId == rub);
		Assert.Equal(0.66m, result.CurrencyValues.Single(value => value.CurrencyId == usd).Value);
		Assert.True(save.Execute(new(null, [])).IsSuccess);
		db.ChangeTracker.Clear();
		Assert.Empty(defaults.Get()!.CurrencyValues);
		Assert.Null(defaults.Get()!.DocumentCurrencyId);
	}

	[Fact]
	public void InvalidSettings_DoNotOverwriteSavedDefaults()
	{
		save.Execute(new(byn, [new(byn, 1m), new(rub, 28.5m)]));
		var invalidCommands = new SaveCurrencyDefaultsCommand[]
		{
			new(byn, [new(byn, 0m)]),
			new(byn, [new(byn, -1m)]),
			new(byn, [new(byn, 1m), new(byn, 2m)]),
			new(byn, [new(rub, 28.5m)]),
			new(null, [new(byn, 1m)]),
			new(999, []),
			new(byn, [new(byn, 1m), new(999, 1m)])
		};
		foreach (var command in invalidCommands)
			Assert.False(save.Execute(command).IsSuccess);
		db.ChangeTracker.Clear();
		Assert.Equal(28.5m, defaults.Get()!.CurrencyValues.Single(value => value.CurrencyId == rub).Value);
	}

	[Fact]
	public void CreateCopiesDefaults_UpdateAndLaterDefaultsDoNotChangeExistingDocument()
	{
		save.Execute(new(byn, [new(byn, 1m), new(rub, 28.5m), new(usd, 0.33m)]));
		var firstId = CreateDocument();
		save.Execute(new(rub, [new(rub, 1m), new(byn, 0.036m)]));
		var secondId = CreateDocument();
		db.ChangeTracker.Clear();
		var first = new ExpenseDocumentRepositorySQLite(db).GetById(firstId)!;
		var second = new ExpenseDocumentRepositorySQLite(db).GetById(secondId)!;
		Assert.Equal(byn, first.CurrencyId);
		Assert.Equal(28.5m, first.CurrencyValues.Single(value => value.CurrencyId == rub).Value);
		Assert.Equal(rub, second.CurrencyId);
		Assert.Equal(0.036m, second.CurrencyValues.Single(value => value.CurrencyId == byn).Value);

		var lineId = db.BudgetLines.Single().Id;
		var item = Assert.Single(first.Items);
		var update = new UpdateExpenseDocumentUseCase(new ExpenseDocumentRepositorySQLite(db),
			new BudgetLineEnsurer(new EfBudgetLineRepository(db), unitOfWork),
			new BudgetTagEnsurer(new EfBudgetTagRepository(db), unitOfWork), unitOfWork,
			new EfCurrencyReader(db));
		Assert.True(update.Execute(new()
		{
			DocumentId = firstId, Date = DateTime.Today, SellerName = "Другой магазин",
			Items = [new UpdateExpenseDocumentItemCommand
			{
				Id = item.Id, Name = item.Name, Price = 10m, Amount = 2m,
				BudgetLineId = lineId, BudgetLineName = "Продукты"
			}]
		}).IsSuccess);
		db.ChangeTracker.Clear();
		var reloaded = new ExpenseDocumentRepositorySQLite(db).GetById(firstId)!;
		Assert.Equal(byn, reloaded.CurrencyId);
		Assert.Equal(3, reloaded.CurrencyValues.Count);
		Assert.Equal(28.5m, reloaded.CurrencyValues.Single(value => value.CurrencyId == rub).Value);
		Assert.Equal(20m, reloaded.TotalSum);
	}

	[Fact]
	public void NoSettingsAndEmptyScale_AllowDocumentCreation()
	{
		Assert.Null(new GetCurrencyDefaultsUseCase(defaults).Execute().Value!.DocumentCurrencyId);
		var firstId = CreateDocument();
		var first = db.ExpenseDocuments.Single(document => document.Id == firstId);
		Assert.Null(first.CurrencyId);
		Assert.Empty(first.CurrencyValues);
		save.Execute(new(byn, []));
		var id = CreateDocument();
		db.ChangeTracker.Clear();
		var second = db.ExpenseDocuments.Single(document => document.Id == id);
		Assert.Equal(byn, second.CurrencyId);
		Assert.Empty(second.CurrencyValues);
	}

	[Fact]
	public void ExplicitCurrencySelection_OverridesDefaults_AndCanBeEdited()
	{
		Assert.True(save.Execute(new(byn, [new(byn, 1m), new(rub, 28.5m)])).IsSuccess);
		var lineId = db.BudgetLines.Single().Id;
		var create = new CreateExpenseDocumentUseCase(new ExpenseDocumentRepositorySQLite(db),
			new BudgetLineEnsurer(new EfBudgetLineRepository(db), unitOfWork),
			new BudgetTagEnsurer(new EfBudgetTagRepository(db), unitOfWork),
			unitOfWork, NullLogger<CreateExpenseDocumentUseCase>.Instance, defaults,
			new EfCurrencyReader(db));
		var created = create.Execute(new()
		{
			Date = DateTime.Today, SellerName = "Магазин",
			CurrencyId = usd, CurrencyValues = [new(usd, 1m), new(rub, 90.25m)],
			Items = [new CreateExpenseDocumentItemCommand
			{
				Name = "Хлеб", Price = 2m, Amount = 1m, BudgetLineId = lineId
			}]
		});
		Assert.True(created.IsSuccess, created.Error?.Message);
		db.ChangeTracker.Clear();

		var reader = new GetExpenseDocumentForEditUseCase(new ExpenseDocumentReader(db));
		var document = reader.Execute(created.Value!.DocumentId).Value!;
		Assert.Equal(usd, document.CurrencyId);
		Assert.Equal(2, document.CurrencyValues.Count);
		Assert.Equal(90.25m, document.CurrencyValues.Single(value => value.CurrencyId == rub).Value);
		Assert.DoesNotContain(document.CurrencyValues, value => value.CurrencyId == byn);

		var update = new UpdateExpenseDocumentUseCase(new ExpenseDocumentRepositorySQLite(db),
			new BudgetLineEnsurer(new EfBudgetLineRepository(db), unitOfWork),
			new BudgetTagEnsurer(new EfBudgetTagRepository(db), unitOfWork), unitOfWork,
			new EfCurrencyReader(db));
		var change = new UpdateExpenseDocumentCommand
		{
			DocumentId = document.Id, Date = document.Date, SellerName = document.SellerName,
			CurrencyId = rub, CurrencyValues = [new(rub, 1m), new(usd, 0.011m)],
			Items = [new UpdateExpenseDocumentItemCommand
			{
				Id = document.Items.Single().Id, Name = "Хлеб", Price = 10m, Amount = 1m,
				BudgetLineId = lineId, BudgetLineName = "Продукты"
			}]
		};
		Assert.True(update.Execute(change).IsSuccess);
		db.ChangeTracker.Clear();

		var changed = reader.Execute(document.Id).Value!;
		Assert.Equal(rub, changed.CurrencyId);
		Assert.Equal(2, changed.CurrencyValues.Count);
		Assert.Equal(0.011m, changed.CurrencyValues.Single(value => value.CurrencyId == usd).Value);
		Assert.Equal(10m, changed.Items.Single().Price);
		Assert.Equal(byn, defaults.Get()!.DocumentCurrencyId);
	}

	[Fact]
	public void InvalidExplicitCurrencySelection_DoesNotAlterExistingDocument()
	{
		var id = CreateDocument();
		var lineId = db.BudgetLines.Single().Id;
		var itemId = db.ExpenseDocumentItems.Single(item => item.ExpenseDocumentId == id).Id;
		var update = new UpdateExpenseDocumentUseCase(new ExpenseDocumentRepositorySQLite(db),
			new BudgetLineEnsurer(new EfBudgetLineRepository(db), unitOfWork),
			new BudgetTagEnsurer(new EfBudgetTagRepository(db), unitOfWork), unitOfWork,
			new EfCurrencyReader(db));
		var invalid = new UpdateExpenseDocumentCommand
		{
			DocumentId = id, Date = DateTime.Today, SellerName = "Измененный",
			CurrencyId = byn, CurrencyValues = [new(rub, 28.5m)],
			Items = [new UpdateExpenseDocumentItemCommand
			{
				Id = itemId, Name = "Хлеб", Price = 99m, Amount = 1m,
				BudgetLineId = lineId, BudgetLineName = "Продукты"
			}]
		};
		Assert.Equal("document_currency_missing", update.Execute(invalid).Error!.Code);
		db.ChangeTracker.Clear();
		var document = db.ExpenseDocuments.Single(value => value.Id == id);
		Assert.Equal("Магазин", document.SellerName);
		Assert.Null(document.CurrencyId);
		Assert.Equal(3.5m, db.ExpenseDocumentItems.Single(item => item.Id == itemId).Price);
	}

	[Fact]
	public void ReferencedCurrenciesCannotBeDeleted_UntilDefaultsAndDocumentsAreRemoved()
	{
		save.Execute(new(byn, [new(byn, 1m), new(rub, 28.5m)]));
		var delete = new DeleteCurrencyUseCase(new EfCurrencyRepository(db), unitOfWork);
		Assert.Equal("currency_in_use", delete.Execute(byn).Error!.Code);
		Assert.Equal("currency_in_use", delete.Execute(rub).Error!.Code);
		var id = CreateDocument();
		save.Execute(new(null, []));
		Assert.Equal("currency_in_use", delete.Execute(rub).Error!.Code);
		db.ExpenseDocuments.Remove(db.ExpenseDocuments.Single(document => document.Id == id));
		db.SaveChanges();
		Assert.Equal(0L, db.Database.SqlQueryRaw<long>("SELECT COUNT(*) AS Value FROM ExpenseDocumentCurrencyValues").Single());
		Assert.True(delete.Execute(rub).IsSuccess);
	}

	[Fact]
	public void MigrationAddsEmptyScalesWithoutChangingExistingDocuments()
	{
		var migrator = db.GetService<IMigrator>();
		var previous = db.Database.GetMigrations().TakeWhile(name => !name.EndsWith("_AddCurrencyDefaults")).Last();
		migrator.Migrate(previous);
		db.Database.ExecuteSqlInterpolated($"INSERT INTO ExpenseDocuments (Id, Date, SellerName, CurrencyId) VALUES (77, '2026-09-01', 'Старый документ', {byn})");
		migrator.Migrate();
		db.ChangeTracker.Clear();
		var document = db.ExpenseDocuments.Single(value => value.Id == 77);
		Assert.Equal(byn, document.CurrencyId);
		Assert.Empty(document.CurrencyValues);
		Assert.Null(defaults.Get());
		Assert.False(db.Database.HasPendingModelChanges());
	}

	private int CreateDocument()
	{
		var useCase = new CreateExpenseDocumentUseCase(new ExpenseDocumentRepositorySQLite(db),
			new BudgetLineEnsurer(new EfBudgetLineRepository(db), unitOfWork),
			new BudgetTagEnsurer(new EfBudgetTagRepository(db), unitOfWork),
			unitOfWork, NullLogger<CreateExpenseDocumentUseCase>.Instance, defaults,
			new EfCurrencyReader(db));
		var result = useCase.Execute(new()
		{
			Date = DateTime.Today, SellerName = "Магазин",
			Items = [new CreateExpenseDocumentItemCommand
			{
				Name = "Хлеб", Price = 3.5m, Amount = 2m, BudgetLineId = db.BudgetLines.Single().Id
			}]
		});
		Assert.True(result.IsSuccess, result.Error?.Message);
		return result.Value!.DocumentId;
	}

	public void Dispose()
	{
		db.Dispose();
		connection.Dispose();
	}
}
