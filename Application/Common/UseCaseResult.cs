using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common;

public sealed class UseCaseResult<T>
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;

	public T? Value { get; }
	public UseCaseError? Error { get; }

	private UseCaseResult(bool isSuccess, T? value, UseCaseError? error)
	{
		IsSuccess = isSuccess;
		Value = value;
		Error = error;
	}

	public static UseCaseResult<T> Success(T value)
	{
		return new UseCaseResult<T>(true, value, null);
	}

	public static UseCaseResult<T> Failure(UseCaseError error)
	{
		return new UseCaseResult<T>(false, default, error);
	}
}


public sealed class UseCaseResult
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;

	public UseCaseError? Error { get; }

	private UseCaseResult(bool isSuccess, UseCaseError? error)
	{
		IsSuccess = isSuccess;
		Error = error;
	}

	public static UseCaseResult Success()
	{
		return new UseCaseResult(true, null);
	}

	public static UseCaseResult Failure(UseCaseError error)
	{
		return new UseCaseResult(false, error);
	}
}

public sealed record UseCaseError(string Code, string Message);