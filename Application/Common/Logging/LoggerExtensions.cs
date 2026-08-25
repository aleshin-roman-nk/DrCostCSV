using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Common.Logging;

public static class LoggerExtensions
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		WriteIndented = true,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		ReferenceHandler = ReferenceHandler.IgnoreCycles
	};

	public static void LogDebugAsJson<T>(
		this ILogger logger,
		string message,
		T value)
	{
		if (!logger.IsEnabled(LogLevel.Debug))
			return;

		var json = JsonSerializer.Serialize(value, JsonOptions);

		logger.LogDebug(
			"{Message}:{NewLine}{Json}",
			message,
			Environment.NewLine,
			json);
	}

	public static void LogInformationAsJson<T>(
		this ILogger logger,
		string message,
		T value)
	{
		if (!logger.IsEnabled(LogLevel.Information))
			return;

		var json = JsonSerializer.Serialize(value, JsonOptions);

		logger.LogInformation(
			"{Message}:{NewLine}{Json}",
			message,
			Environment.NewLine,
			json);
	}
}
