#if IOS || MACCATALYST
using UIKit;
using Foundation;

namespace Maui.SafeInsets;

// All the code in this file is only included on Android.
public class SafeInsetsSerivce : ISafeInsetsService
{
	public event EventHandler<SafeInsetsChangedEventArgs>? SafeInsetsChanged;

	UIView? connectedView;
	IDisposable? observer;
	Action<ISafeInsetsService, UIView>? callback;

	void InsetsChangedCallback()
	{
		this.callback?.Invoke(this, connectedView);
		SafeInsetsChanged?.Invoke(this, new SafeInsetsChangedEventArgs(this));
	}

	public void Connect(UIView view, Action<ISafeInsetsService, UIView>? callback = null)
	{
		this.callback = callback;
		connectedView = view;
		observer = connectedView.AddObserver(
			"safeAreaInsets",
			NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.New,
			_ => InsetsChangedCallback());
	}


	public Thickness GetSafeInsets(SafeInsetsType type = SafeInsetsType.All, ThicknessDimensions dimensions = ThicknessDimensions.All)
	{
		var value = connectedView?.SafeAreaInsets ?? new UIEdgeInsets(0, 0, 0, 0);

		var left = dimensions.HasFlag(ThicknessDimensions.Left) ? value.Left : 0;
		var top = dimensions.HasFlag(ThicknessDimensions.Top) ? value.Top : 0;
		var right = dimensions.HasFlag(ThicknessDimensions.Right) ? value.Right : 0;
		var bottom = dimensions.HasFlag(ThicknessDimensions.Bottom) ? value.Bottom : 0;

		return new Thickness(left, top, right, bottom);
	}
}
#endif