// Platforms/iOS/WidgetBridge.iOS.cs
#if IOS
using System.Runtime.InteropServices;

public static class HelloWidgetBridge
{
    [DllImport("__Internal", EntryPoint = "HelloWidgetBridge_SayHello")]
    private static extern void _SayHello();

    public static void SayHello() => _SayHello();
}
#endif