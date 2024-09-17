namespace Maui.SafeInsets;

[Flags]
public enum ThicknessDimensions
{
	
	None = 0,
	Left = 2,
	Top = 4,
	Right = 8,
	Bottom = 16,

	Horizontal = Left | Right,
	Vertical = Top | Bottom,

	All = Top | Right | Bottom | Left,
}
