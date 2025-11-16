using Foundation;

namespace App;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

        using var defaults = new NSUserDefaults("group.ferry.hello-maui-widget", NSUserDefaultsType.SuiteName);
        defaults.SetString(count.ToString(), "helloValue");
		HelloWidgetBridge.SayHello();
		
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

