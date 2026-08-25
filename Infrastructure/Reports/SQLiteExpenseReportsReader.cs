using Application.Common;
using Application.Reports.Abstractions;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetDocumentTitlesByDay;
using Application.Reports.GetBudgetLineExpensesByMonth;
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

	public IReadOnlyList<DailyExpenseDto> GetDailyExpensesByMonth(int year, int month)
	{
		var from = new DateTime(year, month, 1);
		var to = from.AddMonths(1);

		var expensesByDate = db.Set<ExpenseDocumentItem>()
			.AsNoTracking()
			.Where(item =>
				item.ExpenseDocument.Date >= from &&
				item.ExpenseDocument.Date < to)
			.Select(item => new
			{
				Date = item.ExpenseDocument.Date,
				Sum = item.Price * item.Amount
			})
			.ToList()
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
					: 0m
			});
		}

		return result;
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
				Sum = x.Items.Sum(i => i.Price * i.Amount)
			})
			.OrderBy(x => x.Date)
			.ToList();

		return result;
	}

	public IReadOnlyList<BudgetLineExpenseDto> GetBudgetLineExpensesByMonth(int year, int month)
	{
		var from = new DateTime(year, month, 1);
		var to = from.AddMonths(1);

		var items = db.ExpenseDocumentItems
			.AsNoTracking()
			.Where(item => item.ExpenseDocument.Date >= from && item.ExpenseDocument.Date < to)
			.Select(item => new
			{
				BudgetLineName = item.BudgetLine.Name,
				BudgetTagName = item.BudgetTag == null ? null : item.BudgetTag.Name,
				Sum = item.Price * item.Amount
			})
			.ToList();

		return items
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
	}
}
