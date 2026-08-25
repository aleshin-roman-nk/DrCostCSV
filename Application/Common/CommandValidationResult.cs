using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common;

public sealed class CommandValidationResult
{
	public bool IsValid { get; }

	public UseCaseError? Error { get; }

	private CommandValidationResult(bool isValid, UseCaseError? error)
	{
		IsValid = isValid;
		Error = error;
	}

	public static CommandValidationResult Success()
	{
		return new CommandValidationResult(true, null);
	}

	public static CommandValidationResult Failure(string code, string message)
	{
		return new CommandValidationResult(
			false,
			new UseCaseError(code, message));
	}
}
