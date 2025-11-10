// Platforms/iOS/WidgetBridge.iOS.cs
#if IOS
using Foundation;

public static class WidgetBridge
{
    const string AppGroupId = "app-group.ferry.hello-maui-widget";

    public static void WriteHello(string value)
    {
        using var defaults = new NSUserDefaults(AppGroupId, NSUserDefaultsType.SuiteName);
        defaults.SetString(value, "helloValue");
        defaults.Synchronize();
    }

    // If macios exposes WidgetKit bindings in your setup:
    // using WidgetKit;
    // public static void ReloadAll() => WidgetCenter.Shared.ReloadAllTimelines();

    // Otherwise, add a Swift helper (see next snippet) and call via ObjC:
    // [DllImport(Constants.ObjectiveCLibrary, EntryPoint = "objc_getClass")]
    // static extern IntPtr GetClass(string name);

    // [Export("reloadAll")] static void reloadAll() { } // signature placeholder

    // public static void ReloadAllViaSwift()
    // {
    //     // Ensure class is linked
    //     var handle = GetClass("WidgetReloader");
    //     if (handle != IntPtr.Zero)
    //     {
    //         var sel = new Selector("reloadAll");
    //         NSObject.PerformSelector(sel, null, 0);
    //     }
    // }
}
#endif