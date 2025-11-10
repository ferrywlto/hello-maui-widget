# hello-maui-widget

## Instructions to Reproduce

1. Create the MAUI project

```bash
# Needed to have intellisense works and able to restore/build for MAUI in VSCode
sudo dotnet workload install maui
dotnet new maui -n hello_maui_widget
```

2. Remove non-mobile platforms code and project files for brevity

We don't need Tizen, Windows and MacCatalyst platforms for this demo.

3. Initialized XCode project
- Open XCode
- Create a new App project
- File -> New -> Target -> Widget Extension

## Troubleshooting
### If it is saying Android SDK not found:
```bash
dotnet build -t:InstallAndroidDependencies -f net9.0-android "-p:AndroidSdkDirectory=/Users/ferrywlto/Library/Android/sdk"
```
If it is saying about accepting licenses:

> /usr/local/share/dotnet/packs/Microsoft.Android.Sdk.Darwin/35.0.105/tools/Xamarin.Installer.Common.targets(19,3): error : The Android SDK license agreements were not accepted, please set `$(AcceptAndroidSDKLicenses)` to accept.

```bash
Run: yes | "$ANDROID_HOME/cmdline-tools/latest/bin/sdkmanager" --licenses (or sdkmanager --licenses from Android Studio) to accept interactively.
```

Temporary: 
```bash
dotnet build -p:AcceptAndroidSDKLicenses=true. 
```

Permanent: add to App.csproj:
```xml
<PropertyGroup>
    <AcceptAndroidSDKLicenses>true</AcceptAndroidSDKLicenses>
</PropertyGroup>. 
```
Ensure ANDROID_HOME is set (echo $ANDROID_HOME) and sdkmanager exists.

### If it is complaining XCode version:
> /usr/local/share/dotnet/packs/Microsoft.iOS.Sdk.net9.0_26.0/26.0.9752/targets/Xamarin.Shared.Sdk.targets(2346,3): error : This version of .NET for iOS (26.0.9752) requires Xcode 26.0. The current version of Xcode is 26.1. Either install Xcode 26.0, or use a different version of .NET for iOS. See https://aka.ms/xcode-requirement for more information.

Go to Apple Developer [site](https://developer.apple.com/download/all/) and download the required XCode version.

### If iOS failed to build
```bash
/usr/local/share/dotnet/packs/Microsoft.iOS.Sdk.net9.0_26.0/26.0.9752/tools/msbuild/Xamarin.Shared.targets(614,3): error MSB4018: 
      The "CompileAppManifest" task failed unexpectedly.
      System.NullReferenceException: Object reference not set to an instance of an object.
         at Xamarin.MacDev.Tasks.CompileAppManifest.SetXcodeValues(PDictionary plist, IAppleSdk currentSDK) in /Users/builder/azdo/_work/1/s/macios/msbuild/Xamarin.MacDev.Tasks/Tasks/CompileAppManifest.cs:line 534
         at Xamarin.MacDev.Tasks.CompileAppManifest.Compile(PDictionary plist) in /Users/builder/azdo/_work/1/s/macios/msbuild/Xamarin.MacDev.Tasks/Tasks/CompileAppManifest.cs:line 338
         at Xamarin.MacDev.Tasks.CompileAppManifest.Execute() in /Users/builder/azdo/_work/1/s/macios/msbuild/Xamarin.MacDev.Tasks/Tasks/CompileAppManifest.cs:line 166
      1/s/macios/msbuild/Xamarin.MacDev.Tasks/Tasks/CompileAppManifest.cs:line 166
         at Microsoft.Build.BackEnd.TaskExecutionHost.Execute()
         at Microsoft.Build.BackEnd.TaskBuilder.ExecuteInstantiatedTask(TaskExecutionHost taskExecutionHost, TaskLoggingContext taskLoggingContext, TaskHost taskHost, ItemBucket bucket, TaskExecutionMode howToExecuteTask)
```
- Info.plist lacks CFBundleIdentifier, CFBundleName, CFBundleVersion, CFBundleShortVersionString; these must match ApplicationId and versions.
- Add keys to Info.plist: 
    - CFBundleIdentifier=com.companyname.app
    - CFBundleName=App
    - CFBundleVersion=1
    - CFBundleShortVersionString=1.0.
    
### App Group Id need to be in Entitlements.plist instead of Info.plist
