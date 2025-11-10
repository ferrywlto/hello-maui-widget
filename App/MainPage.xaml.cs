namespace App;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        var writeBtn = new Button { Text = "Write & Reload Widget" };
        writeBtn.Clicked += (s, e) =>
        {
#if IOS
            count++;
            WidgetBridge.WriteHello($"Hello #{count} at {DateTime.Now:T}");
            // WidgetBridge.ReloadAllViaSwift(); // or .ReloadAll() if you have direct binding
#endif
        };

        Content = new VerticalStackLayout
        {
            Children = { new Label { Text = "MAUI ↔︎ Widget PoC" }, writeBtn }
        };		
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

