using Application.BudgetLines;
using Application.BudgetLines.GetBudgetLines;
using Application.BudgetTags;
using Application.BudgetTags.GetBudgetTags;
using Application.Common;
using Application.ExpenseDocuments.CreateExpenseDocument;
using Application.ExpenseDocuments.GetExpenseDocumentForEdit;
using Application.ExpenseDocuments.UpdateExpenseDocument;
using Application.Currencies.GetCurrencies;
using Application.CurrencyDefaults;
using Presentation.Screens.Common;
using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public sealed class ExpenseDocumentEditActions
{
	private readonly IUseCaseScopedExecutor useCaseScopedExecutor;
	public ExpenseDocumentEditActions(IUseCaseScopedExecutor useCaseScopedExecutor) => this.useCaseScopedExecutor = useCaseScopedExecutor;

	public ActionResult CreateDocument(ExpenseDocumentViewModel document)
	{
		var values = ParseCurrencyValues(document.CurrencyValues);
		if (!values.IsSuccess || values.Data is null)
			return ActionResult.Failure(values.Error ?? "Проверьте значения валют.");

		return useCaseScopedExecutor.Execute<CreateExpenseDocumentUseCase, ActionResult>(useCase =>
		{
			var result = useCase.Execute(new CreateExpenseDocumentCommand
			{
				Date = document.Date, SellerName = document.Seller,
				CurrencyId = document.CurrencyId, CurrencyValues = values.Data,
				Items = document.Items.Select(x => new CreateExpenseDocumentItemCommand
				{
					Name = x.Name, Price = x.Price, Amount = x.Amount,
					BudgetLineId = x.BudgetLineId, BudgetLineName = x.BudgetLineName,
					BudgetTagId = x.BudgetTagId, BudgetTagName = x.BudgetTagName
				}).ToList()
			});
			return result.IsSuccess ? ActionResult.Success()
				: ActionResult.Failure(result.Error?.Message ?? "Не удалось сохранить документ.");
		});
	}

	public ActionResult UpdateDocument(ExpenseDocumentViewModel document)
	{
		if (!document.Id.HasValue) return ActionResult.Failure("Не указан идентификатор документа.");
		var values = ParseCurrencyValues(document.CurrencyValues);
		if (!values.IsSuccess || values.Data is null)
			return ActionResult.Failure(values.Error ?? "Проверьте значения валют.");
		return useCaseScopedExecutor.Execute<UpdateExpenseDocumentUseCase, ActionResult>(useCase =>
		{
			var result = useCase.Execute(new UpdateExpenseDocumentCommand
			{
				DocumentId = document.Id.Value, Date = document.Date, SellerName = document.Seller,
				CurrencyId = document.CurrencyId, CurrencyValues = values.Data,
				Items = document.Items.Select(x => new UpdateExpenseDocumentItemCommand
				{
					Id = x.Id, Name = x.Name, Price = x.Price, Amount = x.Amount,
					BudgetLineId = x.BudgetLineId, BudgetLineName = x.BudgetLineName,
					BudgetTagId = x.BudgetTagId, BudgetTagName = x.BudgetTagName
				}).ToList()
			});
			return result.IsSuccess ? ActionResult.Success() : ActionResult.Failure(result.Error?.Message ?? "Не удалось обновить документ.");
		});
	}

	public ActionResult<IReadOnlyList<CurrencyOptionViewModel>> GetCurrencies() =>
		useCaseScopedExecutor.Execute<GetCurrenciesUseCase, ActionResult<IReadOnlyList<CurrencyOptionViewModel>>>(useCase =>
		{
			var result = useCase.Execute();
			return result.IsSuccess && result.Value is not null
				? ActionResult<IReadOnlyList<CurrencyOptionViewModel>>.Success(result.Value
					.Select(currency => new CurrencyOptionViewModel(currency.Id, currency.Code)).ToList())
				: ActionResult<IReadOnlyList<CurrencyOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить валюты.");
		});

	public ActionResult<ExpenseDocumentCurrencyDefaultsViewModel> GetCurrencyDefaults() =>
		useCaseScopedExecutor.Execute<GetCurrencyDefaultsUseCase, ActionResult<ExpenseDocumentCurrencyDefaultsViewModel>>(useCase =>
		{
			var result = useCase.Execute();
			return result.IsSuccess && result.Value is not null
				? ActionResult<ExpenseDocumentCurrencyDefaultsViewModel>.Success(new(
					result.Value.DocumentCurrencyId,
					result.Value.CurrencyValues.Select(value => new ExpenseDocumentCurrencyValueViewModel
					{
						CurrencyId = value.CurrencyId,
						Value = value.Value.ToString(CultureInfo.GetCultureInfo("ru-RU"))
					}).ToList()))
				: ActionResult<ExpenseDocumentCurrencyDefaultsViewModel>.Failure(result.Error?.Message ?? "Не удалось загрузить валюту по умолчанию.");
		});

	private static ActionResult<IReadOnlyList<CurrencyValueDto>> ParseCurrencyValues(
		IEnumerable<ExpenseDocumentCurrencyValueViewModel> rows)
	{
		var values = new List<CurrencyValueDto>();
		foreach (var row in rows)
		{
			if (!decimal.TryParse((row.Value ?? "").Trim().Replace(',', '.'),
				NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
				CultureInfo.InvariantCulture, out var amount) || amount <= 0)
				return ActionResult<IReadOnlyList<CurrencyValueDto>>.Failure(
					$"Строка валют {values.Count + 1}: укажите положительное число, например 28,5.");
			values.Add(new(row.CurrencyId, amount));
		}
		return ActionResult<IReadOnlyList<CurrencyValueDto>>.Success(values);
	}

	public ActionResult<IReadOnlyList<BudgetLineOptionViewModel>> GetBudgetLines() => useCaseScopedExecutor.Execute<GetBudgetLinesUseCase, ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>>(useCase =>
	{
		var result = useCase.Execute();
		return result.IsSuccess
			? ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>.Success((result.Value ?? Array.Empty<BudgetLineDto>()).Select(x => new BudgetLineOptionViewModel { Id = x.Id, Name = x.Name }).ToList())
			: ActionResult<IReadOnlyList<BudgetLineOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить строки бюджета.");
	});

	public ActionResult<IReadOnlyList<BudgetTagOptionViewModel>> GetBudgetTags() => useCaseScopedExecutor.Execute<GetBudgetTagsUseCase, ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>>(useCase =>
	{
		var result = useCase.Execute();
		return result.IsSuccess
			? ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>.Success((result.Value ?? Array.Empty<BudgetTagDto>()).Select(x => new BudgetTagOptionViewModel { Id = x.Id, BudgetLineId = x.BudgetLineId, Name = x.Name }).ToList())
			: ActionResult<IReadOnlyList<BudgetTagOptionViewModel>>.Failure(result.Error?.Message ?? "Не удалось загрузить теги.");
	});

	public ActionResult<ExpenseDocumentViewModel> GetDocument(int id) => useCaseScopedExecutor.Execute<GetExpenseDocumentForEditUseCase, ActionResult<ExpenseDocumentViewModel>>(useCase =>
	{
		var result = useCase.Execute(id);
		if (!result.IsSuccess || result.Value is null) return ActionResult<ExpenseDocumentViewModel>.Failure(result.Error?.Message ?? "Документ не найден.");
		var dto = result.Value;
		return ActionResult<ExpenseDocumentViewModel>.Success(new ExpenseDocumentViewModel
		{
			Id = dto.Id, Date = dto.Date, Seller = dto.SellerName,
			CurrencyId = dto.CurrencyId,
			CurrencyValues = new BindingList<ExpenseDocumentCurrencyValueViewModel>(dto.CurrencyValues
				.Select(value => new ExpenseDocumentCurrencyValueViewModel
				{
					CurrencyId = value.CurrencyId,
					Value = value.Value.ToString(CultureInfo.GetCultureInfo("ru-RU"))
				}).ToList()),
			Items = new BindingList<ExpenseDocumentItemViewModel>(dto.Items.Select(x =>
				new ExpenseDocumentItemViewModel
				{
					Id = x.Id, Name = x.Name, Price = x.Price, Amount = x.Amount,
					BudgetLineId = x.BudgetLineId, BudgetLineName = x.BudgetLineName,
					BudgetTagId = x.BudgetTagId, BudgetTagName = x.BudgetTagName ?? string.Empty
				}).ToList())
		});
	});
}
