using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Common;

public sealed class ActionResult<TViewModel>
{
	private ActionResult(
		bool isSuccess,
		TViewModel? data,
		string? error)
	{
		IsSuccess = isSuccess;
		Data = data;
		Error = error;
	}

	public bool IsSuccess { get; }

	public TViewModel? Data { get; }

	public string? Error { get; }

	public static ActionResult<TViewModel> Success(TViewModel data)
	{
		ArgumentNullException.ThrowIfNull(data);

		return new ActionResult<TViewModel>(
			isSuccess: true,
			data,
			error: null);
	}

	public static ActionResult<TViewModel> Failure(string error)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(error);

		return new ActionResult<TViewModel>(
			isSuccess: false,
			data: default,
			error);
	}
}

public sealed class ActionResult
{
	private ActionResult(
		bool isSuccess,
		string? error)
	{
		IsSuccess = isSuccess;
		Error = error;
	}

	public bool IsSuccess { get; }

	public string? Error { get; }

	public static ActionResult Success()
	{
		return new ActionResult(
			isSuccess: true,
			error: null);
	}

	public static ActionResult Failure(string error)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(error);

		return new ActionResult(
			isSuccess: false,
			error);
	}
}
