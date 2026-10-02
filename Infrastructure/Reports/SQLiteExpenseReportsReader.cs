using Application.Common;
using Application.Reports.Abstractions;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetDocumentTitlesByDay;
using Application.Reports.GetBudgetLineExpensesByMonth;
using Application.Reports;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reports;

public class SQLiteExpenseReportsReader : IExpenseReportsReader
{
	private readonly AppDbContext db;
	private readonly ILogger<SQLiteExpenseReportsReader> logger;

	public SQLiteExpenseReportsReader(
		AppDbContext db,
		ILogger<SQLiteExpenseReportsReader> logger
		)
	{
		this.db = db;
		this.logger = logger;
	}

	public MonthlyReportReadResult<DailyExpenseDto> GetDailyExpensesByMonth(
		int year, int month, int reportCurrencyId)
	{
		var expenses = LoadConvertedExpenses(year, month, reportCurrencyId);
		var expensesByDate = expenses.Rows
			.GroupBy(x => x.Date.Date)
			.ToDictionary(
				group => group.Key,
				group => group.Sum(x => x.Sum));

		var daysInMonth = DateTime.DaysInMonth(year, month);

		var result = new List<DailyExpenseDto>(daysInMonth);

		for (int day = 1; day <= daysInMonth; day++)
		{
			var date = new DateTime(year, month, day);

			result.Add(new DailyExpenseDto
			{
				Date = date,
				TotalSum = expensesByDate.TryGetValue(date, out var totalSum)
					? totalSum
					: 0m,
				ExcludedDocumentCount = expenses.ExcludedDocumentsByDate.GetValueOrDefault(date)
			});
		}

		return new(result, expenses.ExcludedDocumentCount);
	}

	public IReadOnlyList<DocumentTitleDto> GetDocumentTitlesByDay(DateTime dt)
	{
		var dayStart = dt.Date;
		var nextDay = dayStart.AddDays(1);

		var result = db.ExpenseDocuments
			.AsNoTracking()
			.Where(x => x.Date >= dayStart && x.Date < nextDay)
			.Select(x => new DocumentTitleDto
			{
				Id = x.Id,
				Date = x.Date,
				Seller = x.SellerName,
				CurrencyCode = db.Currencies
					.Where(currency => currency.Id == x.CurrencyId)
					.Select(currency => currency.Code)
					.FirstOrDefault(),
				Sum = x.Items.Sum(i => i.Price * i.Amount)
			})
			.OrderBy(x => x.Date)
			.ToList();

		return result;
	}

	public MonthlyReportReadResult<BudgetLineExpenseDto> GetBudgetLineExpensesByMonth(
		int year, int month, int reportCurrencyId)
	{
		var expenses = LoadConvertedExpenses(year, month, reportCurrencyId);

		var rows = expenses.Rows
			.GroupBy(item => item.BudgetLineName)
			.OrderBy(group => group.Key)
			.Select(group => new BudgetLineExpenseDto
			{
				BudgetLineName = group.Key,
				TotalSum = group.Sum(item => item.Sum),
				Tags = group
					.Where(item => !string.IsNullOrWhiteSpace(item.BudgetTagName))
					.GroupBy(item => item.BudgetTagName!)
					.OrderBy(tagGroup => tagGroup.Key)
					.Select(tagGroup => new BudgetTagExpenseDto
					{
						BudgetTagName = tagGroup.Key,
						TotalSum = tagGroup.Sum(item => item.Sum)
					})
					.ToList()
			})
			.ToList();
		return new(rows, expenses.ExcludedDocumentCount);
	}

	private ConvertedExpenses LoadConvertedExpenses(int year, int month, int reportCurrencyId)
	{
		var from = new DateTime(year, month, 1);
		var to = from.AddMonths(1);
		var documents = db.ExpenseDocuments
			.AsNoTracking()
			.AsSplitQuery()
			.Where(document => document.Date >= from && document.Date < to)
			.Include(document => document.CurrencyValues)
			.Include(document => document.Items).ThenInclude(item => item.BudgetLine)
			.Include(document => document.Items).ThenInclude(item => item.BudgetTag)
			.ToList();

		var rows = new List<ConvertedExpenseRow>();
		var excludedDocumentCount = 0;
		var excludedDocumentsByDate = new Dictionary<DateTime, int>();
		foreach (var document in documents)
		{
			if (document.Items.Count == 0)
				continue;

			decimal factor;
			if (document.CurrencyId == reportCurrencyId)
			{
				factor = 1m;
			}
			else
			{
				var source = document.CurrencyValues
					.FirstOrDefault(value => value.CurrencyId == document.CurrencyId);
				var target = document.CurrencyValues
					.FirstOrDefault(value => value.CurrencyId == reportCurrencyId);
				if (source is null || target is null || source.Value <= 0 || target.Value <= 0)
				{
					excludedDocumentCount++;
					excludedDocumentsByDate[document.Date.Date] =
						excludedDocumentsByDate.GetValueOrDefault(document.Date.Date) + 1;
					continue;
				}
				factor = target.Value / source.Value;
			}

			rows.AddRange(document.Items.Select(item => new ConvertedExpenseRow(
				document.Date, item.BudgetLine.Name, item.BudgetTag?.Name,
				item.Price * item.Amount * factor)));
		}
		return new(rows, excludedDocumentCount, excludedDocumentsByDate);
	}

	private sealed record ConvertedExpenseRow(
		DateTime Date, string BudgetLineName, string? BudgetTagName, decimal Sum);
	private sealed record ConvertedExpenses(
		IReadOnlyList<ConvertedExpenseRow> Rows, int ExcludedDocumentCount,
		IReadOnlyDictionary<DateTime, int> ExcludedDocumentsByDate);
}
