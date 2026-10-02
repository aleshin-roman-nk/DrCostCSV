namespace Presentation.Screens.Common;

internal static class WindowIcon
{
	private const string ResourceName = "Presentation.Screens.Resources.fe.ico";
	private static readonly Icon icon = Load();

	public static void Apply(Form form)
	{
		form.Icon = icon;
	}

	private static Icon Load()
	{
		using var stream = typeof(WindowIcon).Assembly
			.GetManifestResourceStream(ResourceName)
			?? throw new InvalidOperationException($"Embedded icon '{ResourceName}' was not found.");
		using var resourceIcon = new Icon(stream);

		return (Icon)resourceIcon.Clone();
	}
}
