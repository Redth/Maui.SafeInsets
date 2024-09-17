using System;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

#if ANDROID
using PlatformView = Android.Views.View;
#elif IOS || MACCATALYST
using PlatformView = UIKit.UIView;
#endif

namespace Maui.SafeInsets;

public static class BuilderExtensions
{
	public static MauiAppBuilder UseSafeInsets(this MauiAppBuilder builder, Action<ISafeInsetsService, PlatformView>? insetsChanged = null)
	{
		#if ANDROID || IOS || MACCATALYST
		builder.Services.AddScoped<ISafeInsetsService, SafeInsetsSerivce>();
		#endif

		builder.ConfigureLifecycleEvents(lifecycle => {
#if ANDROID
			lifecycle.AddAndroid(androidLifecycle => {
				androidLifecycle.OnCreate((activity, bundle) => {
					var safeInsets = activity.GetWindow()?.Handler?.MauiContext?.Services?.GetRequiredService<ISafeInsetsService>();
					safeInsets!.Connect(activity!.Window!.DecorView, insetsChanged);
				});
			});
#endif
		});

#if IOS || MACCATALYST
		PageHandler.Mapper.AppendToMapping("SafeInsetsService", (handler, view) => {
			var safeInsets = handler!.MauiContext!.Services.GetRequiredService<ISafeInsetsService>();
			safeInsets.Connect(handler.PlatformView, insetsChanged);
		});
#endif
		
		return builder;
	}
}
