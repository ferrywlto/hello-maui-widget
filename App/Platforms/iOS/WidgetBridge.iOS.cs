// Platforms/iOS/WidgetBridge.iOS.cs
#if IOS
using System.Runtime.InteropServices;
using Foundation;

public static class WidgetBridge
{
    const string AppGroupId = "group.ferry.hello-maui-widget";

    public static void WriteHello(string value)
    {
        using var defaults = new NSUserDefaults(AppGroupId, NSUserDefaultsType.SuiteName);
        defaults.SetString(value, "helloValue");
        defaults.Synchronize();
        HelloWidgetBridge.SayHello();
    }
}

public static class HelloWidgetBridge
{
    [DllImport("__Internal", EntryPoint = "HelloWidgetBridge_SayHello")]
    private static extern void _SayHello();

    public static void SayHello() => _SayHello();
}
#endif