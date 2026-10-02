using Application.Currencies.DeleteCurrency;
using Application.Currencies.GetCurrencies;
using Application.Currencies.GetCurrencyForEdit;
using Application.Currencies.SaveCurrency;
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

public sealed class CurrencyTests : IDisposable
{
	private readonly SqliteConnection connection;
	private readonly AppDbContext db;
	private readonly SaveCurrencyUseCase save;
	private readonly DeleteCurrencyUseCase delete;

	public CurrencyTests()
	{
		connection = new SqliteConnection("Data Source=:memory:;Foreign Keys=True");
		connection.Open();
		db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options);
		db.Database.Migrate();
		var repository = new EfCurrencyRepository(db);
		var unitOfWork = new EfUnitOfWork(db, NullLogger<EfUnitOfWork>.Instance);
		save = new SaveCurrencyUseCase(repository, unitOfWork);
		delete = new DeleteCurrencyUseCase(repository, unitOfWork);
	}

	[Fact]
	public void CreateEditReadDelete_RoundTripsThroughDatabase()
	{
		var created = save.Execute(new(null, " byn ", " Белорусский рубль "));
		Assert.True(created.IsSuccess);
		db.ChangeTracker.Clear();
		var loaded = new GetCurrencyForEditUseCase(new EfCurrencyReader(db)).Execute(created.Value);
		Assert.Equal("BYN", loaded.Value!.Code);
		Assert.Equal("Белорусский рубль", loaded.Value.Name);

		var edited = save.Execute(new(created.Value, "rub", "Российский рубль"));
		Assert.True(edited.IsSuccess);
		db.ChangeTracker.Clear();
		var row = Assert.Single(new GetCurrenciesUseCase(new EfCurrencyReader(db)).Execute().Value!);
		Assert.Equal(created.Value, row.Id);
		Assert.Equal("RUB", row.Code);
		Assert.Equal("Российский рубль", row.Name);

		Assert.True(delete.Execute(created.Value).IsSuccess);
		Assert.Empty(db.Currencies.AsNoTracking());
	}

	[Fact]
	public void DuplicateCode_IsRejectedOnCreateAndEdit()
	{
		var first = save.Execute(new(null, "BYN", "Белорусский рубль")).Value;
		var second = save.Execute(new(null, "RUB", "Российский рубль")).Value;
		Assert.Equal("duplicate_currency", save.Execute(new(null, "byn", "Дубль")).Error!.Code);
		Assert.Equal("duplicate_currency", save.Execute(new(second, "BYN", "Дубль")).Error!.Code);
		Assert.True(save.Execute(new(first, "BYN", "Новое название")).IsSuccess);
		Assert.Equal(2, db.Currencies.Count());
	}

	[Theory]
	[InlineData("", "Валюта")]
	[InlineData("РУБ", "Валюта")]
	[InlineData("US", "Валюта")]
	[InlineData("USDD", "Валюта")]
	[InlineData("USD", " ")]
	public void InvalidInput_DoesNotWriteToDatabase(string code, string name)
	{
		Assert.False(save.Execute(new(null, code, name)).IsSuccess);
		Assert.Empty(db.Currencies);
	}

	[Fact]
	public void MissingCurrency_ReturnsExpectedFailures()
	{
		Assert.Equal("currency_not_found", save.Execute(new(999, "USD", "Доллар")).Error!.Code);
		Assert.Equal("currency_not_found", delete.Execute(999).Error!.Code);
		Assert.Equal("currency_not_found",
			new GetCurrencyForEditUseCase(new EfCurrencyReader(db)).Execute(999).Error!.Code);
	}

	[Fact]
	public void UsedCurrency_CannotBeDeleted_AndForeignKeyProtectsDirectDeletion()
	{
		var id = save.Execute(new(null, "BYN", "Белорусский рубль")).Value;
		var document = new ExpenseDocument(DateTime.Today, "Магазин");
		document.ChangeCurrency(id);
		db.ExpenseDocuments.Add(document);
		db.SaveChanges();
		db.ChangeTracker.Clear();

		Assert.Equal("currency_in_use", delete.Execute(id).Error!.Code);
		Assert.Equal(id, db.ExpenseDocuments.AsNoTracking().Single().CurrencyId);
		db.Currencies.Remove(db.Currencies.Single());
		Assert.Throws<DbUpdateException>(() => db.SaveChanges());
	}

	[Fact]
	public void UniqueIndex_RejectsCaseInsensitiveDuplicates()
	{
		save.Execute(new(null, "USD", "Доллар"));
		db.Currencies.Add(new Currency("usd", "Дубль"));
		Assert.Throws<DbUpdateException>(() => db.SaveChanges());
	}

	[Fact]
	public void CurrencyMigration_PreservesExistingDocumentAndItems()
	{
		var migrator = db.GetService<IMigrator>();
		var previous = db.Database.GetMigrations().TakeWhile(name => !name.EndsWith("_AddCurrencies")).Last();
		migrator.Migrate(previous);
		db.Database.ExecuteSqlRaw("INSERT INTO BudgetLines (Id, Name) VALUES (1, 'Продукты')");
		db.Database.ExecuteSqlRaw("INSERT INTO ExpenseDocuments (Id, Date, SellerName) VALUES (1, '2026-09-01', 'Магазин')");
		db.Database.ExecuteSqlRaw(
			"INSERT INTO ExpenseDocumentItems (Id, ExpenseDocumentId, Name, Price, Amount, BudgetLineId) VALUES (1, 1, 'Хлеб', '3.5', '2', 1)");
		migrator.Migrate();

		var document = db.ExpenseDocuments.AsNoTracking().Include(value => value.Items).Single();
		Assert.Null(document.CurrencyId);
		Assert.Equal("Магазин", document.SellerName);
		Assert.Equal(7m, document.TotalSum);
		Assert.Empty(db.Currencies);
		Assert.False(db.Database.HasPendingModelChanges());
	}

	public void Dispose()
	{
		db.Dispose();
		connection.Dispose();
	}
}
