namespace App;

public partial class MainPage : ContentPage
{
	int count = 0;
	string currentString = "Hello, World!";

	public MainPage()
	{
		InitializeComponent();
		WidgetLabel.Text = currentString;
	}

	private void OnWriteAndReloadClicked(object sender, EventArgs e)
	{
		#if IOS
			count++;
			currentString = $"Hello #{count} at {DateTime.Now:T}";
			WidgetBridge.WriteHello(currentString);
			// WidgetBridge.ReloadAllViaSwift(); // or .ReloadAll() if you have direct binding
		#endif        
		
		WidgetLabel.Text = currentString;
		SemanticScreenReader.Announce(WidgetLabel.Text);
    }
	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

