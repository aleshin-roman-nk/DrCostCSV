using Application;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Presentation.Screens;
using Presentation.Screens.Main;
using Serilog;
using Serilog.Events;


namespace Startup.WinForms;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();

		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
			.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
			.WriteTo.File(
				path: "logs/family-economy-.log",
				rollingInterval: RollingInterval.Day,
				retainedFileCountLimit: 14)
			.CreateLogger();

		global::System.Windows.Forms.Application.ThreadException += (sender, e) =>
		{
			Log.Error(e.Exception, "Unhandled UI thread exception");
			MessageBox.Show(
				"Произошла непредвиденная ошибка.",
				"Ошибка",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		};

		AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
		{
			if (e.ExceptionObject is Exception ex)
				Log.Fatal(ex, "Unhandled application exception");
		};

		try
		{
			var services = new ServiceCollection();

			services.AddLogging(builder =>
			{
				builder.ClearProviders();
				builder.AddSerilog();
			});


			services.AddWinFormsPresentation();
			services.AddApplication();

			services.AddSqlitePersistence(SqliteDatabasePath.GetConnectionString());

			using var serviceProvider = services.BuildServiceProvider();

			using (var scope = serviceProvider.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				db.Database.Migrate();
			}

			var mainPresenter = serviceProvider.GetRequiredService<MainPresenter>();

			var f = mainPresenter.View as Form;


			if (f != null)
				global::System.Windows.Forms.Application.Run(f);
		}
		catch (Exception)
		{

			throw;
		}

	}
}