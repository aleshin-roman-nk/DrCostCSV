using GoShare.Application.Common;
using GoShare.Application.Database.Common;
using GoShare.Application.Database.CreateDatabase;
using GoShare.Application.Database.OpenDatabase;
using GoShare.Application.Sharers;
using GoShare.Application.Sharers.GetSharers;
using GoShare.Application.Sharers.SaveSharerMonthCard;
using GoShare.Application.Reports.ObjectList;
using GoShare.Application.Reports.SharerPersonal;
using GoShare.Domain;
using GoShare.Screens.Common;
using GoShare.Screens.Main.InputDatabase;
using GoShare.Screens.Main.ViewModels;
using GoShare.Screens.MonthCards.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoShare.Screens.Main;

public class MainActions
{
	private readonly IScopedExecutor scopedExecutor;

	public MainActions(IScopedExecutor scopedExecutor)
	{
		this.scopedExecutor = scopedExecutor;
	}

	public ActionResult<CurrentDatabaseInfo> CreateDatabase(CreateDatabaseInputModel inputModel)
	{
		var useCaseResult = scopedExecutor.Execute<CreateDatabaseUseCase, UseCaseResult<CurrentDatabaseInfo>>(useCase =>
		{
			return useCase.Execute(new CreateDatabaseCommand(inputModel.FullFileName, inputModel.Password));
		});

		return useCaseResult switch
		{
			{ IsSuccess: true, Value: not null } =>
				ActionResult<CurrentDatabaseInfo>.Success(useCaseResult.Value),

			{ IsSuccess: false, Error: not null } =>
				ActionResult<CurrentDatabaseInfo>.Failure(useCaseResult.Error),

			_ => throw new InvalidOperationException(
				"Некорректное состояние UseCaseResult.")
		};
	}

	public ActionResult<IReadOnlyList<SharerRowViewModel>> GetMatrixForYear(
		int year,
		SharerApplicMode applicMode)
	{
		var periodStart = new DateTime(year, 9, 1);
		var periodEnd = periodStart.AddYears(1);

		var useCaseResult = scopedExecutor.Execute<
			GetSharersUseCase,
			UseCaseResult<IReadOnlyList<SharerListItemDto>>>(
			useCase => useCase.Execute());

		return useCaseResult switch
		{
			{ IsSuccess: true, Value: not null } =>
				ActionResult<IReadOnlyList<SharerRowViewModel>>.Success(
					useCaseResult.Value
						.Where(sharer => sharer.ApplicMode == applicMode)
						.Select(sharer => new SharerRowViewModel
						{
							SharerId = sharer.Id,
							Name = sharer.Name,
							DSuffix = sharer.DSuffix,
							Note = sharer.Note,
							GroupCode = sharer.GroupCode,
							GroupNumber = sharer.GroupNumber,
							GroupColor = sharer.GroupColorArgb,
							AvailableApplics =
								SharerMonthCardApplicPolicy.GetAllowedApplics(
									sharer.ApplicMode),
							Cells = sharer.MonthCards
								.Where(card =>
									card.Month >= periodStart && card.Month < periodEnd)
								.Select(card => new SharerCellViewModel
								{
									CardId = card.Id,
									Month = new YearMonth(card.Month.Year, card.Month.Month),
									Applic = card.Applic,
									H = card.H,
									HasTheSharing = card.HasTheSharing,
									IB = card.IB,
									Note = card.Note,
									DisplayValue = GetCardDisplayValue(card)
								})
								.ToArray()
						})
						.ToArray()),

			{ IsSuccess: false, Error: not null } =>
				ActionResult<IReadOnlyList<SharerRowViewModel>>.Failure(
					useCaseResult.Error),

			_ => throw new InvalidOperationException(
				"Invalid UseCaseResult state.")
		};
	}

	public ActionResult SaveMonthCard(
		int sharerId,
		YearMonth month,
		SharerMonthCardInputModel inputModel)
	{
		if (inputModel.Applic is null)
		{
			return ActionResult.Failure("Select an applic.");
		}

		var useCaseResult = scopedExecutor.Execute<
			SaveSharerMonthCardUseCase,
			UseCaseResult<SharerMonthCardDto>>(
			useCase => useCase.Execute(new SaveSharerMonthCardCommand(
				sharerId,
				month.FirstDay,
				inputModel.Applic.Value,
				inputModel.H,
				inputModel.HasTheSharing,
				inputModel.IB,
				inputModel.Note)));

		return useCaseResult switch
		{
			{ IsSuccess: true } => ActionResult.Success(),
			{ IsSuccess: false, Error: not null } =>
				ActionResult.Failure(useCaseResult.Error),
			_ => throw new InvalidOperationException(
				"Invalid UseCaseResult state.")
		};
	}

	public ActionResult<CurrentDatabaseInfo> OpenDatabase(OpenDatabaseInputModel inputModel)
	{
		var useCaseResult = scopedExecutor.Execute<OpenDatabaseUseCase, UseCaseResult<CurrentDatabaseInfo>>(useCase =>
		{
			return useCase.Execute(new OpenDatabaseCommand(inputModel.FullFileName, inputModel.Password));
		});

		return useCaseResult switch
		{
			{ IsSuccess: true, Value: not null } =>
				ActionResult<CurrentDatabaseInfo>.Success(useCaseResult.Value),

			{ IsSuccess: false, Error: not null } =>
				ActionResult<CurrentDatabaseInfo>.Failure(useCaseResult.Error),

			_ => throw new InvalidOperationException(
				"Некорректное состояние UseCaseResult.")
		};
	}

	public ActionResult<GeneratedFileDto> GenerateObjectListReport(int year)
	{
		var useCaseResult = scopedExecutor.Execute<
			GenerateObjectListReportUseCase,
			UseCaseResult<GeneratedFileDto>>(
			useCase => useCase.Execute(new GenerateObjectListReportCommand(year)));

		return useCaseResult switch
		{
			{ IsSuccess: true, Value: not null } =>
				ActionResult<GeneratedFileDto>.Success(useCaseResult.Value),
			{ IsSuccess: false, Error: not null } =>
				ActionResult<GeneratedFileDto>.Failure(useCaseResult.Error),
			_ => throw new InvalidOperationException("Invalid UseCaseResult state.")
		};
	}

	public ActionResult<IReadOnlyList<GeneratedSharerPersonalReportDto>>
		GenerateSharerPersonalReports(int year)
	{
		var useCaseResult = scopedExecutor.Execute<
			GenerateSharerPersonalReportsUseCase,
			UseCaseResult<IReadOnlyList<GeneratedSharerPersonalReportDto>>>(
			useCase => useCase.Execute(
				new GenerateSharerPersonalReportsCommand(year)));

		return useCaseResult switch
		{
			{ IsSuccess: true, Value: not null } =>
				ActionResult<IReadOnlyList<GeneratedSharerPersonalReportDto>>.Success(
					useCaseResult.Value),
			{ IsSuccess: false, Error: not null } =>
				ActionResult<IReadOnlyList<GeneratedSharerPersonalReportDto>>.Failure(
					useCaseResult.Error),
			_ => throw new InvalidOperationException("Invalid UseCaseResult state.")
		};
	}

	private static string GetCardDisplayValue(SharerMonthCardDto card)
	{
		string mainValue = card.Applic is CardApplic.Pogoda or CardApplic.Odejda
			? card.H?.ToString() ?? string.Empty
			: card.HasTheSharing == true ? "ДА" : "нет";

		return card.IB > 0
			? $"{mainValue}/{card.IB}"
			: mainValue;
	}
}
