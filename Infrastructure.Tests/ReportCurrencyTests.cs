using Application.CurrencyDefaults;
using Application.ExpenseDocuments.GetDocumentsWithoutCurrency;
using Application.Currencies.DeleteCurrency;
using Application.Reports.GetBudgetLineExpensesByMonth;
using Application.Reports.GetDailyExpensesByMonth;
using Domain;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Readers;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Reports;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Tests;

public sealed class ReportCurrencyTests : IDisposable
{
	private readonly SqliteConnection connection;
	private readonly AppDbContext db;
	private readonly EfCurrencyDefaultSettingsRepository settingsRepository;
	private readonly SaveCurrencyDefaultsUseCase saveSettings;
	private readonly int byn;
	private readonly int rub;
	private readonly int usd;
	private readonly int budgetLineId;

	public ReportCurrencyTests()
	{
		connection = new SqliteConnection("Data Source=:memory:;Foreign Keys=True");
		connection.Open();
		db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options);
		db.Database.Migrate();
		var currencies = new[]
		{
			new Currency("BYN", "Белорусский рубль"),
			new Currency("RUB", "Российский рубль"),
			new Currency("USD", "Доллар")
		};
		db.Currencies.AddRange(currencies);
		var budgetLine = new BudgetLine("Продукты");
		db.BudgetLines.Add(budgetLine);
		db.SaveChanges();
		byn = currencies[0].Id;
		rub = currencies[1].Id;
		usd = currencies[2].Id;
		budgetLineId = budgetLine.Id;
		settingsRepository = new EfCurrencyDefaultSettingsRepository(db);
		saveSettings = new SaveCurrencyDefaultsUseCase(
			settingsRepository, new EfCurrencyReader(db),
			new EfUnitOfWork(db, NullLogger<EfUnitOfWork>.Instance));
	}

	[Fact]
	public void DiscountLine_ReducesStoredDocumentAndReportTotals()
	{
		var document = new ExpenseDocument(new DateTime(2026, 9, 1), "Магазин");
		document.SetCurrencyValues(byn, []);
		document.AddItem("Товар", 10m, 1m, budgetLineId);
		document.AddItem("Скидка", -2m, 1m, budgetLineId);
		db.ExpenseDocuments.Add(document);
		db.SaveChanges();
		db.ChangeTracker.Clear();

		Assert.True(saveSettings.Execute(new(byn, [], byn)).IsSuccess);
		db.ChangeTracker.Clear();
		Assert.Equal(-2m, db.ExpenseDocumentItems.Single(item => item.Name == "Скидка").Price);
		var reader = new SQLiteExpenseReportsReader(db, NullLogger<SQLiteExpenseReportsReader>.Instance);
		Assert.Equal(8m, reader.GetDocumentTitlesByDay(new DateTime(2026, 9, 1)).Single().Sum);
		Assert.Equal(8m, Daily().Execute(new(2026, 9)).Value!.Days.Single(day => day.Date.Day == 1).TotalSum);
		Assert.Equal(8m, Budget().Execute(new(2026, 9)).Value!.BudgetLines.Single().TotalSum);
	}

	[Fact]
	public void DailyTotals_ConvertMixedCurrenciesAndTrackMissingRatesByDay()
	{
		AddDocument(5, byn, [], 100m);
		AddDocument(5, rub, [new(rub, 1m), new(byn, 0.035m)], 200m);
		AddDocument(6, rub, [], 25m);
		db.ChangeTracker.Clear();
		Assert.True(saveSettings.Execute(new(byn, [], byn)).IsSuccess);
		db.ChangeTracker.Clear();

		var report = Daily().Execute(new(2026, 9)).Value!;
		var fifth = report.Days.Single(day => day.Date.Day == 5);
		var sixth = report.Days.Single(day => day.Date.Day == 6);
		Assert.Equal(107m, fifth.TotalSum);
		Assert.Equal(0, fifth.ExcludedDocumentCount);
		Assert.Equal(1, sixth.ExcludedDocumentCount);
		Assert.Equal(1, report.ExcludedDocumentCount);
	}

	[Fact]
	public void NoReportCurrency_ReturnsFailureWithoutMixedSums()
	{
		AddDocument(1, byn, [new(byn, 1m), new(rub, 28.5m)], 10m);
		var daily = Daily();
		var budget = Budget();
		Assert.Equal("report_currency_required",
			daily.Execute(new(2026, 9)).Error!.Code);
		Assert.Equal("report_currency_required",
			budget.Execute(new(2026, 9)).Error!.Code);
	}

	[Fact]
	public void MonthlyViewsUseSelectedCurrency_AndCountExcludedDocuments()
	{
		AddDocument(1, byn, [new(byn, 1m), new(rub, 28.5m), new(usd, 0.33m)], 20m);
		AddDocument(2, rub, [], 50m);
		AddDocument(2, usd, [new(usd, 1m), new(rub, 90m)], 2m);
		AddDocument(3, byn, [new(byn, 1m), new(usd, 0.33m)], 10m);
		AddDocument(4, null, [], 5m);
		db.ChangeTracker.Clear();

		Assert.True(saveSettings.Execute(new(byn, [], rub)).IsSuccess);
		db.ChangeTracker.Clear();
		var dailyRub = Daily().Execute(new(2026, 9)).Value!;
		var budgetRub = Budget().Execute(new(2026, 9)).Value!;
		Assert.Equal("RUB", dailyRub.CurrencyCode);
		Assert.Equal(570m, dailyRub.Days.Single(day => day.Date.Day == 1).TotalSum);
		Assert.Equal(230m, dailyRub.Days.Single(day => day.Date.Day == 2).TotalSum);
		Assert.Equal(800m, budgetRub.BudgetLines.Single().TotalSum);
		Assert.Equal("RUB", budgetRub.CurrencyCode);
		Assert.Equal(2, dailyRub.ExcludedDocumentCount);
		Assert.Equal(2, budgetRub.ExcludedDocumentCount);

		Assert.True(saveSettings.Execute(new(byn, [], usd)).IsSuccess);
		db.ChangeTracker.Clear();
		var dailyUsd = Daily().Execute(new(2026, 9)).Value!;
		var budgetUsd = Budget().Execute(new(2026, 9)).Value!;
		Assert.Equal("USD", budgetUsd.CurrencyCode);
		Assert.Equal(6.6m, dailyUsd.Days.Single(day => day.Date.Day == 1).TotalSum);
		Assert.Equal(2m, dailyUsd.Days.Single(day => day.Date.Day == 2).TotalSum);
		Assert.Equal(3.3m, dailyUsd.Days.Single(day => day.Date.Day == 3).TotalSum);
		Assert.Equal(11.9m, budgetUsd.BudgetLines.Single().TotalSum);
		Assert.Equal(2, budgetUsd.ExcludedDocumentCount);
		Assert.Equal(usd, new GetCurrencyDefaultsUseCase(settingsRepository).Execute().Value!.ReportCurrencyId);
	}

	[Fact]
	public void ReverseScale_UsesRatioAndChangingReportCurrencyDoesNotModifyDocument()
	{
		var documentId = AddDocument(1, byn, [new(byn, 0.036m), new(rub, 1m)], 10m);
		Assert.True(saveSettings.Execute(new(byn, [], rub)).IsSuccess);
		db.ChangeTracker.Clear();
		var report = Budget().Execute(new(2026, 9)).Value!;
		Assert.Equal(10m / 0.036m, report.BudgetLines.Single().TotalSum);
		Assert.Equal(0.036m, db.ExpenseDocuments.Single(document => document.Id == documentId)
			.CurrencyValues.Single(value => value.CurrencyId == byn).Value);
	}

	[Fact]
	public void ReportCurrencyMustExist_AndCannotBeDeletedWhileSelected()
	{
		Assert.Equal("currency_not_found",
			saveSettings.Execute(new(byn, [], 999)).Error!.Code);
		Assert.True(saveSettings.Execute(new(null, [], rub)).IsSuccess);
		var delete = new DeleteCurrencyUseCase(new EfCurrencyRepository(db),
			new EfUnitOfWork(db, NullLogger<EfUnitOfWork>.Instance));
		Assert.Equal("currency_in_use", delete.Execute(rub).Error!.Code);
	}

	[Fact]
	public void MigrationPreservesOlderSettingsWithNoReportCurrency()
	{
		var migrator = db.GetService<IMigrator>();
		var previous = db.Database.GetMigrations()
			.TakeWhile(name => !name.EndsWith("_AddReportCurrency")).Last();
		migrator.Migrate(previous);
		db.Database.ExecuteSqlInterpolated(
			$"INSERT INTO CurrencyDefaultSettings (Id, DocumentCurrencyId) VALUES (1, {byn})");
		migrator.Migrate();
		db.ChangeTracker.Clear();
		var settings = new GetCurrencyDefaultsUseCase(settingsRepository).Execute().Value!;
		Assert.Equal(byn, settings.DocumentCurrencyId);
		Assert.Null(settings.ReportCurrencyId);
		Assert.False(db.Database.HasPendingModelChanges());
	}

	[Fact]
	public void DocumentsWithoutCurrency_AreListedUntilCurrencyIsSaved()
	{
		var first = AddDocument(1, null, [], 5m);
		AddDocument(2, byn, [], 7m);
		var last = AddDocument(3, null, [], 9m);
		db.ChangeTracker.Clear();

		var useCase = new GetDocumentsWithoutCurrencyUseCase(new ExpenseDocumentReader(db));
		var documents = useCase.Execute().Value!;
		Assert.Equal(new[] { last, first }, documents.Select(document => document.Id));
		Assert.All(documents, document => Assert.Equal(1, document.ItemCount));

		var edited = db.ExpenseDocuments.Single(document => document.Id == last);
		edited.SetCurrencyValues(byn, []);
		db.SaveChanges();
		db.ChangeTracker.Clear();

		Assert.Equal(first, Assert.Single(useCase.Execute().Value!).Id);
	}
	[Fact]
	public void DocumentTitles_ReturnEachDocumentsCurrency_AndKeepDocumentsWithoutCurrency()
	{
		var bynDocument = AddDocument(1, byn, [], 10m);
		var rubDocument = AddDocument(1, rub, [], 20m);
		var withoutCurrency = AddDocument(1, null, [], 5m);
		AddDocument(2, usd, [], 30m);
		db.ChangeTracker.Clear();

		var reader = new SQLiteExpenseReportsReader(db, NullLogger<SQLiteExpenseReportsReader>.Instance);
		var documents = reader.GetDocumentTitlesByDay(new DateTime(2026, 9, 1));

		Assert.Equal(3, documents.Count);
		Assert.Equal("BYN", documents.Single(document => document.Id == bynDocument).CurrencyCode);
		Assert.Equal("RUB", documents.Single(document => document.Id == rubDocument).CurrencyCode);
		Assert.Null(documents.Single(document => document.Id == withoutCurrency).CurrencyCode);
		Assert.Equal(10m, documents.Single(document => document.Id == bynDocument).Sum);
		Assert.Equal(20m, documents.Single(document => document.Id == rubDocument).Sum);
	}

	private int AddDocument(int day, int? currencyId, CurrencyValue[] values, decimal price)
	{
		var document = new ExpenseDocument(new DateTime(2026, 9, day), "Магазин");
		document.SetCurrencyValues(currencyId, values);
		document.AddItem("Хлеб", price, 1m, budgetLineId);
		db.ExpenseDocuments.Add(document);
		db.SaveChanges();
		return document.Id;
	}

	private GetDailyExpensesByMonthUseCase Daily() =>
		new(new SQLiteExpenseReportsReader(db, NullLogger<SQLiteExpenseReportsReader>.Instance),
			settingsRepository, new EfCurrencyReader(db));
	private GetBudgetLineExpensesByMonthUseCase Budget() =>
		new(new SQLiteExpenseReportsReader(db, NullLogger<SQLiteExpenseReportsReader>.Instance),
			settingsRepository, new EfCurrencyReader(db));

	public void Dispose()
	{
		db.Dispose();
		connection.Dispose();
	}
}
