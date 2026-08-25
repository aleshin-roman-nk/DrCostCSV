using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.Common
{
	public class ScreenResult<T>
	{
		public bool IsSuccess { get; }
		public bool IsCancelled { get; }
		public T? Data { get; }
		public string? Error { get; }

		private ScreenResult(bool isSuccess, bool isCancelled, T? data, string? error)
		{
			IsSuccess = isSuccess;
			IsCancelled = isCancelled;
			Data = data;
			Error = error;
		}

		public static ScreenResult<T> Success(T data)
			=> new ScreenResult<T>(true, false, data, null);

		public static ScreenResult<T> Cancelled()
			=> new ScreenResult<T>(false, true, default, null);

		public static ScreenResult<T> Failure(string error)
			=> new ScreenResult<T>(false, false, default, error);
	}
	
	public class ScreenResult
	{
		public bool IsSuccess { get; }
		public bool IsCancelled { get; }
		public string? Error { get; }

		private ScreenResult(bool isSuccess, bool isCancelled, string? error)
		{
			IsSuccess = isSuccess;
			IsCancelled = isCancelled;
			Error = error;
		}

		public static ScreenResult Success()
			=> new ScreenResult(true, false, null);

		public static ScreenResult Cancelled()
			=> new ScreenResult(false, true, null);

		public static ScreenResult Failure(string error)
			=> new ScreenResult(false, false, error);
	}
}
