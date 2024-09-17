
using AndroidX.Core.View;
using Google.Android.Material.Carousel;

namespace Maui.SafeInsets;

// All the code in this file is only included on Android.
public class SafeInsetsSerivce : Java.Lang.Object, ISafeInsetsService, IOnApplyWindowInsetsListener
{
	public event EventHandler<SafeInsetsChangedEventArgs>? SafeInsetsChanged;

	Android.Views.View? connectedView;

	public void Connect(Android.Views.View view,  Action<ISafeInsetsService, Android.Views.View>? callback = null)
	{
		ViewCompat.SetOnApplyWindowInsetsListener(view, this);

		connectedView = view;
	}

	public Thickness GetSafeInsets(SafeInsetsType type = SafeInsetsType.All, ThicknessDimensions dimensions = ThicknessDimensions.All)
	{
		if (connectedView is null)
			return new Thickness();

		var insets = ViewCompat.GetRootWindowInsets(connectedView);

		var mask = GetInsetsTypeMask(type);
		var value = insets?.GetInsets(mask);

		if (value != null)
		{
			var left = dimensions.HasFlag(ThicknessDimensions.Left) ? value.Left : 0;
			var top = dimensions.HasFlag(ThicknessDimensions.Top) ? value.Top : 0;
			var right = dimensions.HasFlag(ThicknessDimensions.Right) ? value.Right : 0;
			var bottom = dimensions.HasFlag(ThicknessDimensions.Bottom) ? value.Bottom : 0;

			return new Thickness(left, top, right, bottom);
		}

		return new Thickness();
	}

	int GetInsetsTypeMask(SafeInsetsType type)
	{
		if (type == SafeInsetsType.None)
			return 0;

		var mask = 0;

		if (type.HasFlag(SafeInsetsType.All))
			mask |= WindowInsetsCompat.Type.SystemGestures()
				| WindowInsetsCompat.Type.SystemBars()
				| WindowInsetsCompat.Type.DisplayCutout();

		if (type.HasFlag(SafeInsetsType.AllSystemBars))
			mask |= WindowInsetsCompat.Type.SystemBars();

		if (type.HasFlag(SafeInsetsType.StatusBars))
			mask |= WindowInsetsCompat.Type.StatusBars();

		if (type.HasFlag(SafeInsetsType.NavigationBars))
			mask |= WindowInsetsCompat.Type.NavigationBars();

		if (type.HasFlag(SafeInsetsType.DisplayCutout))
			mask |= WindowInsetsCompat.Type.DisplayCutout();

		if (type.HasFlag(SafeInsetsType.Inputs))
			mask |= WindowInsetsCompat.Type.Ime();

		return mask;
	}

    public WindowInsetsCompat OnApplyWindowInsets(Android.Views.View v, WindowInsetsCompat insets)
    {
        this.SafeInsetsChanged?.Invoke(this, new SafeInsetsChangedEventArgs(this));
		return insets;
    }
}
