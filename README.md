# hello-maui-widget
This is a demo project to record how to create a simple .NET MAUI app with iOS Widget Extension.

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

5. Run the project

```bash
dotnet build -t:Run -f net9.0-ios -p:_DeviceName="iPhone 15"
# or
maui run ios --device "iPhone 15"
```

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


### Cannot find iPhone 4s simulator
> EXEC : error MT1207: Could not find the simulator device type 'iPhone 4s'.

Find the correct UDID with:
```bash
xcrun simctl list devices
```

```
# Shows
-- iOS 26.1 --
    iPhone 17 Pro (716C3FE0-62D8-4D79-B213-3F247D6BB4B4) (Booted) 
    iPhone 17 Pro Max (23E55348-EFEE-4BE9-83B2-EE842DBA8A55) (Shutdown) 
    iPhone Air (B2E4286E-DC4D-47BB-A3B1-23B5E79DE73C) (Shutdown) 
    iPhone 17 (7A1FBC27-9B27-46BF-B911-96E7899CE091) (Shutdown) 
    iPhone 16e (81C3ACEE-4091-46A8-9099-2335AB89A185) (Shutdown) 
```
Then run with:
```bash
# Note that the parameter is _DeviceName not _DeviceId, but the value is UDID
dotnet build -t:Run -f net9.0-ios -p:_DeviceName=:v2:udid=684A48DD-AC09-41D0-979E-FB8DE6F69F5C
```

### Android emulator
Find available Android emulators by:

```bash
# List available AVDs
$ANDROID_HOME/emulator/emulator -list-avds

# Start a specific AVD
$ANDROID_HOME/emulator/emulator -avd Pixel_3a_API_34_extension_level_7_arm64-v8a
```

Have to start the emulator first before running the app, then run:
```bash
dotnet build -t:Run -f net9.0-android -p:_DeviceId=Pixel_3a_API_34_extension_level_7_arm64-v8a
```
