namespace App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		return new Window(new AppShell());
	}

	protected override void OnAppLinkRequestReceived(Uri uri)
	{
		if(uri.Scheme == "myapp" && uri.Host == "hello")
		{
			var window = Windows[0];
			if (window != null)
			{
				window.Page = new NavigationPage(new MainPage());
			}
		}
		
		base.OnAppLinkRequestReceived(uri);
	}
}
