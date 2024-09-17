#if ANDROID
using PlatformView = Android.Views.View;
#elif IOS || MACCATALYST
using PlatformView = UIKit.UIView;
#endif

namespace Maui.SafeInsets;

// All the code in this file is included in all platforms.
public interface ISafeInsetsService
{
	event EventHandler<SafeInsetsChangedEventArgs> SafeInsetsChanged;
	

	public void Connect(PlatformView view, Action<ISafeInsetsService, PlatformView>? insetsChangedCallback = null);

	Thickness GetSafeInsets(
		SafeInsetsType type = SafeInsetsType.All,
		ThicknessDimensions dimensions = ThicknessDimensions.All);
}
