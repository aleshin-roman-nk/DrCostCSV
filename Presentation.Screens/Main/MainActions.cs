using Application.Reports.Abstractions;
using Application.Reports.GetDailyExpensesByMonth;
using Application.Reports.GetBudgetLineExpensesByMonth;
using Presentation.Screens.Common;
using Presentation.Screens.Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Main;

public sealed class MainActions
{
	private readonly IUseCaseScopedExecutor useCaseScopedExecutor;

	public MainActions(IUseCaseScopedExecutor useCaseScopedExecutor)
	{
		this.useCaseScopedExecutor = useCaseScopedExecutor;
	}

	public ActionResult<IReadOnlyList<DailyExpenseRowViewModel>> GetDayExpenses(int year, int month)
	{
		return useCaseScopedExecutor.Execute<GetDailyExpensesByMonthUseCase, ActionResult<IReadOnlyList<DailyExpenseRowViewModel>>>(useCase =>
		{
			var result = useCase.Execute(
				new GetDailyExpensesByMonthQuery(year, month));

			if (!result.IsSuccess)
			{
				var errorMessage = result.Error?.Message ?? "Unknown error.";

				return ActionResult<IReadOnlyList<DailyExpenseRowViewModel>>
					.Failure(errorMessage);
			}

			if (result.Value is null)
			{
				return ActionResult<IReadOnlyList<DailyExpenseRowViewModel>>
					.Failure("Use case returned success result without value.");
			}

			var rows = result.Value.Days
				.Select(x => new DailyExpenseRowViewModel
				{
					Date = x.Date,
					TotalSum = x.TotalSum
				})
				.ToList();

			return ActionResult<IReadOnlyList<DailyExpenseRowViewModel>>
				.Success(rows);
		});
	}

	public ActionResult<string> GetMonthlyReport(int year, int month)
	{
		return useCaseScopedExecutor.Execute<GetBudgetLineExpensesByMonthUseCase, ActionResult<string>>(useCase =>
		{
			var result = useCase.Execute(new GetBudgetLineExpensesByMonthQuery(year, month));
			if (!result.IsSuccess)
				return ActionResult<string>.Failure(result.Error?.Message ?? "Не удалось построить отчёт.");

			return ActionResult<string>.Success(FormatReport(result.Value ?? []));
		});
	}

	private static string FormatReport(IReadOnlyList<BudgetLineExpenseDto> budgetLines)
	{
		var report = new StringBuilder();
		var totalSum = budgetLines.Sum(budgetLine => budgetLine.TotalSum);
		report.AppendLine($"ОБЩИЕ РАСХОДЫ ЗА МЕСЯЦ : {totalSum:N2} ₽");
		report.AppendLine();

		foreach (var budgetLine in budgetLines)
		{
			report.AppendLine($"{budgetLine.BudgetLineName} : {budgetLine.TotalSum:N2} ₽");

			if (budgetLine.Tags.Count > 0)
			{
				var untaggedSum = budgetLine.TotalSum - budgetLine.Tags.Sum(tag => tag.TotalSum);
				var hasUntaggedItems = untaggedSum > 0m;
				var tagNameWidth = Math.Max(
					budgetLine.Tags.Max(tag => tag.BudgetTagName.Length),
					hasUntaggedItems ? "[Остальное]".Length : 0);
				var childCount = budgetLine.Tags.Count + (hasUntaggedItems ? 1 : 0);

				for (var index = 0; index < budgetLine.Tags.Count; index++)
				{
					var tag = budgetLine.Tags[index];
					var branch = index == childCount - 1 ? "└──" : "├──";
					report.AppendLine($" {branch} {tag.BudgetTagName.PadRight(tagNameWidth)} : {tag.TotalSum:N2} ₽");
				}

				if (hasUntaggedItems)
					report.AppendLine($" └── {"[Остальное]".PadRight(tagNameWidth)} : {untaggedSum:N2} ₽");
			}

			report.AppendLine();
		}

		return report.ToString().TrimEnd();
	}

}
