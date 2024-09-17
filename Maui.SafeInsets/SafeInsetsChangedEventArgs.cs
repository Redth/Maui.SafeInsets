namespace Maui.SafeInsets;

public class SafeInsetsChangedEventArgs : EventArgs
{
	public SafeInsetsChangedEventArgs(ISafeInsetsService safeInsets)
	{
		SafeInsets = safeInsets;
	}

	public ISafeInsetsService SafeInsets { get; }
}
