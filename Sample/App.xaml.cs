
namespace Sample;

public partial class App : Application
{
	public App(MainPage mainPage)
	{
		InitializeComponent();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
		var safeInsets = Services.GetRequiredService<ISafeInsets>();
		var mainPage = new MainPage(safeInsets);
		return new Window(mainPage);
    }
}
