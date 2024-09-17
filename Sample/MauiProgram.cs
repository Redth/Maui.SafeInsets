using Maui.SafeInsets;
using Microsoft.Extensions.Logging;

namespace Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSafeInsets((safeInsets, view) => {
				var si = safeInsets.GetSafeInsets(SafeInsetsType.All, ThicknessDimensions.All);
				Console.WriteLine($"SafeInsets: {si.Left}, {si.Top}, {si.Right}, {si.Bottom}");
				App.Current.Resources["SafeInsetsBottom"] = safeInsets.GetSafeInsets(SafeInsetsType.All, ThicknessDimensions.Bottom);
				App.Current.Resources["SafeInsetsTop"] = safeInsets.GetSafeInsets(SafeInsetsType.All, ThicknessDimensions.Top);
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
