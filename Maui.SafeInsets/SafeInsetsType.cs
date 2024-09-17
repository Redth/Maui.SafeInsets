namespace Maui.SafeInsets;

[Flags]
public enum SafeInsetsType
{
	None = 0,
	StatusBars = 2,
	NavigationBars = 4,
	AllSystemBars = StatusBars | NavigationBars,
	DisplayCutout = 32,
	Inputs = 256,

	All = StatusBars | NavigationBars | DisplayCutout | Inputs
}
