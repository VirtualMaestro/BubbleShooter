/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// Class representing the Telegram Web App interface.
    /// </summary>
    public class TelegramWebApp
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        /// <summary>
        /// Gets a property as a string from the Telegram Web App.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The property value as a string.</returns>
        [DllImport("__Internal")]
        private static extern string GetPropertyAsString(string propertyName);

        /// <summary>
        /// Gets a property as a JSON string from the Telegram Web App.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The property value as a JSON string.</returns>
        [DllImport("__Internal")]
        private static extern string GetPropertyAsJsonString(string propertyName);

        /// <summary>
        /// Gets a property as an integer from the Telegram Web App.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The property value as an integer.</returns>
        [DllImport("__Internal")]
        private static extern int GetPropertyAsInt(string propertyName);

        /// <summary>
        /// Gets a property as a float from the Telegram Web App.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The property value as a float.</returns>
        [DllImport("__Internal")]
        private static extern float GetPropertyAsFloat(string propertyName);

        /// <summary>
        /// Gets a property as a boolean from the Telegram Web App.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>The property value as a boolean.</returns>
        [DllImport("__Internal")]
        private static extern bool GetPropertyAsBool(string propertyName);

        /// <summary>
        /// Invokes a method with a boolean parameter in the Telegram Web App.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The boolean parameter to pass to the method.</param>
        [DllImport("__Internal")]
        public static extern void InvokeMethodWithBoolParam(string methodName, bool param);
#else
        public static string UNSUPPORTED_PLATFORM_MESSAGE = "Only supported in WebGL Runtime";

        /// <summary>
        /// Gets a property as a string from the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>An empty string.</returns>
        private static string GetPropertyAsString(string propertyName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return "";
        }

        /// <summary>
        /// Gets a property as a JSON string from the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>null.</returns>
        private static string GetPropertyAsJsonString(string propertyName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return null;
        }

        /// <summary>
        /// Gets a property as an integer from the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>0.</returns>
        private static int GetPropertyAsInt(string propertyName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return 0;
        }

        /// <summary>
        /// Gets a property as a float from the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>0.0f.</returns>
        private static float GetPropertyAsFloat(string propertyName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return 0;
        }

        /// <summary>
        /// Gets a property as a boolean from the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        /// <returns>false.</returns>
        private static bool GetPropertyAsBool(string propertyName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return false;
        }

        /// <summary>
        /// Invokes a method with a boolean parameter in the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The boolean parameter to pass to the method.</param>
        public static void InvokeMethodWithBoolParam(string methodName, bool param)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return;
        }
#endif

        // Basic properties
        /// <summary>
        /// Gets the initialization data.
        /// </summary>
        public static string InitData => GetPropertyAsString("initData");

        /// <summary>
        /// Gets the unsafe initialization data.
        /// </summary>
        public static WebAppInitData InitDataUnsafe =>
            JsonUtility.FromJson<WebAppInitData>(
                GetPropertyAsJsonString("initDataUnsafe")
                    ?? JsonUtility.ToJson(new WebAppInitData())
            );

        /// <summary>
        /// Gets the version of the Telegram Web App.
        /// </summary>
        public static string Version => GetPropertyAsString("version");

        /// <summary>
        /// Gets the platform of the Telegram Web App.
        /// </summary>
        public static string Platform => GetPropertyAsString("platform");

        // Theme related
        /// <summary>
        /// Gets the theme parameters.
        /// </summary>
        public static ThemeParams ThemeParams =>
            JsonUtility.FromJson<ThemeParams>(
                GetPropertyAsJsonString("themeParams") ?? JsonUtility.ToJson(new ThemeParams())
            );

        /// <summary>
        /// Gets the color scheme.
        /// </summary>
        public static string ColorScheme => GetPropertyAsString("colorScheme");

        /// <summary>
        /// Gets the header color.
        /// </summary>
        public static string HeaderColor => GetPropertyAsString("headerColor");

        /// <summary>
        /// Gets the background color.
        /// </summary>
        public static string BackgroundColor => GetPropertyAsString("backgroundColor");

        // Viewport related
        /// <summary>
        /// Gets the viewport height.
        /// </summary>
        public static float ViewportHeight => GetPropertyAsFloat("viewportHeight");

        /// <summary>
        /// Gets the stable viewport height.
        /// </summary>
        public static float ViewportStableHeight => GetPropertyAsFloat("viewportStableHeight");

        /// <summary>
        /// Checks if the viewport is expanded.
        /// </summary>
        public static bool IsExpanded => GetPropertyAsBool("isExpanded");

        /// <summary>
        /// Bot API 8.0+ True, if the Mini App is currently active. False, if the Mini App is minimized.
        /// </summary>
        public static bool IsActive => GetPropertyAsBool("isActive");

        // Settings
        /// <summary>
        /// Checks if closing confirmation is enabled.
        /// </summary>
        public static bool IsClosingConfirmationEnabled =>
            GetPropertyAsBool("isClosingConfirmationEnabled");

        /// <summary>
        /// Checks if vertical swipes are enabled.
        /// </summary>
        public static bool IsVerticalSwipesEnabled => GetPropertyAsBool("isVerticalSwipesEnabled");

        public static bool IsFullscreen => GetPropertyAsBool("isFullscreen");

        public static bool IsOrientationLocked => GetPropertyAsBool("isOrientationLocked");

        public static SafeAreaInset SafeAreaInset =>
            JsonUtility.FromJson<SafeAreaInset>(
                GetPropertyAsJsonString("safeAreaInset") ?? JsonUtility.ToJson(new SafeAreaInset())
            );

        public static ContentSafeAreaInset ContentSafeAreaInset =>
            JsonUtility.FromJson<ContentSafeAreaInset>(
                GetPropertyAsJsonString("contentSafeAreaInset")
                    ?? JsonUtility.ToJson(new ContentSafeAreaInset())
            );

        public static BiometricManager BiometricManager =>
            JsonUtility.FromJson<BiometricManager>(
                GetPropertyAsJsonString("BiometricManager")
                    ?? JsonUtility.ToJson(new BiometricManager())
            );

        /// <summary>
        /// An object for controlling haptic feedback.
        /// </summary>
        public static HapticFeedback HapticFeedback =>
            JsonUtility.FromJson<HapticFeedback>(
                GetPropertyAsJsonString("HapticFeedback")
                    ?? JsonUtility.ToJson(new HapticFeedback())
            );

        /// <summary>
        /// An object for accessing accelerometer data on the device.
        /// </summary>
        public static Accelerometer Accelerometer =>
            JsonUtility.FromJson<Accelerometer>(
                GetPropertyAsJsonString("Accelerometer") ?? JsonUtility.ToJson(new Accelerometer())
            );

        /// <summary>
        /// An object for accessing device orientation data on the device.
        /// </summary>
        public static DeviceOrientation DeviceOrientation =>
            JsonUtility.FromJson<DeviceOrientation>(
                GetPropertyAsJsonString("DeviceOrientation")
                    ?? JsonUtility.ToJson(new DeviceOrientation())
            );

        /// <summary>
        /// An object for accessing gyroscope data on the device.
        /// </summary>
        public static Gyroscope Gyroscope =>
            JsonUtility.FromJson<Gyroscope>(
                GetPropertyAsJsonString("Gyroscope") ?? JsonUtility.ToJson(new Gyroscope())
            );

        /// <summary>
        /// An object for controlling location on the device.
        /// </summary>
        public static LocationManager LocationManager =>
            JsonUtility.FromJson<LocationManager>(
                GetPropertyAsJsonString("LocationManager")
                    ?? JsonUtility.ToJson(new LocationManager())
            );

        /// <summary>
        /// An object for controlling the main button in the Mini App interface.
        /// </summary>
        public static BottomButton MainButton =>
            JsonUtility.FromJson<BottomButton>(
                GetPropertyAsJsonString("MainButton") ?? JsonUtility.ToJson(new BottomButton())
            );

        /// <summary>
        /// An object for controlling the main button in the Mini App interface.
        /// </summary>
        public static BottomButton SecondaryButton =>
            JsonUtility.FromJson<BottomButton>(
                GetPropertyAsJsonString("SecondaryButton") ?? JsonUtility.ToJson(new BottomButton())
            );

        /// <summary>
        /// An object for controlling the back button in the Mini App interface.
        /// </summary>
        public static BackButton BackButton =>
            JsonUtility.FromJson<BackButton>(
                GetPropertyAsJsonString("BackButton") ?? JsonUtility.ToJson(new BackButton())
            );

        /// <summary>
        /// An object for controlling the settings button in the Mini App interface.
        /// </summary>
        public static SettingsButton SettingsButton =>
            JsonUtility.FromJson<SettingsButton>(
                GetPropertyAsJsonString("SettingsButton") ?? JsonUtility.ToJson(new SettingsButton())
            );

        // Methods
#if UNITY_WEBGL && !UNITY_EDITOR
        /// <summary>
        /// Invokes a method in the Telegram Web App.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        [DllImport("__Internal")]
        public static extern void InvokeMethod(string methodName);

        /// <summary>
        /// Invokes a method with a parameter in the Telegram Web App.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The parameter to pass to the method.</param>
        [DllImport("__Internal")]
        public static extern void InvokeMethodWithParam(string methodName, string param);

        /// <summary>
        /// Invokes a method with a JSON string parameter in the Telegram Web App.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The JSON string parameter to pass to the method.</param>
        [DllImport("__Internal")]
        public static extern void InvokeMethodWithJsonStringParam(string methodName, string param);

        /// <summary>
        /// Invokes a method with a parameter and returns a boolean in the Telegram Web App.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The parameter to pass to the method.</param>
        /// <returns>The result of the method invocation as a boolean.</returns>
        [DllImport("__Internal")]
        public static extern bool InvokeMethodWithParamReturnBoolean(
            string methodName,
            string param
        );
#else
        /// <summary>
        /// Invokes a method in the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        public static void InvokeMethod(string methodName)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return;
        }

        /// <summary>
        /// Invokes a method with a JSON string parameter in the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The JSON string parameter to pass to the method.</param>
        public static void InvokeMethodWithJsonStringParam(string methodName, string param)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return;
        }

        /// <summary>
        /// Invokes a method with a parameter in the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The parameter to pass to the method.</param>
        public static void InvokeMethodWithParam(string methodName, string param)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return;
        }

        /// <summary>
        /// Invokes a method with a parameter and returns a boolean in the Telegram Web App (unsupported platform).
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="param">The parameter to pass to the method.</param>
        /// <returns>false.</returns>
        public static bool InvokeMethodWithParamReturnBoolean(string methodName, string param)
        {
            Debug.LogWarning(UNSUPPORTED_PLATFORM_MESSAGE);
            return false;
        }
#endif

        // Navigation methods
        /// <summary>
        /// Marks the Telegram Web App as ready.
        /// </summary>
        public static void Ready() => InvokeMethod("ready");

        /// <summary>
        /// Expands the Telegram Web App.
        /// </summary>
        public static void Expand() => InvokeMethod("expand");

        /// <summary>
        /// Closes the Telegram Web App.
        /// </summary>
        public static void Close() => InvokeMethod("close");

        /// <summary>
        /// Enables closing confirmation in the Telegram Web App.
        /// </summary>
        public static void EnableClosingConfirmation() => InvokeMethod("enableClosingConfirmation");

        /// <summary>
        /// Disables closing confirmation in the Telegram Web App.
        /// </summary>
        public static void DisableClosingConfirmation() =>
            InvokeMethod("disableClosingConfirmation");

        /// <summary>
        /// Enables vertical swipes in the Telegram Web App.
        /// </summary>
        public static void EnableVerticalSwipes() => InvokeMethod("enableVerticalSwipes");

        /// <summary>
        /// Disables vertical swipes in the Telegram Web App.
        /// </summary>
        public static void DisableVerticalSwipes() => InvokeMethod("disableVerticalSwipes");

        // Utility methods
        /// <summary>
        /// Checks if the current version is at least the specified version.
        /// </summary>
        /// <param name="version">The version to compare against.</param>
        /// <returns>true if the current version is at least the specified version; otherwise, false.</returns>
        public static bool IsVersionAtLeast(string version) =>
            InvokeMethodWithParamReturnBoolean("isVersionAtLeast", version);

        // UI methods
        /// <summary>
        /// Shows a confirmation dialog with the specified message.
        /// </summary>
        /// <param name="message">The message to display in the confirmation dialog.</param>
        public static void ShowConfirm(string message) =>
            InvokeMethodWithParam("showConfirm", message);

        /// <summary>
        /// Shows an alert with the specified message.
        /// </summary>
        /// <param name="message">The message to display in the alert.</param>
        public static void ShowAlert(string message) => InvokeMethodWithParam("showAlert", message);

        // Open link in external browser
        /// <summary>
        /// Opens a link in an external browser.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        public static void OpenLink(string url) => InvokeMethodWithParam("openLink", url);

        // Open Telegram link
        /// <summary>
        /// Opens a Telegram link.
        /// </summary>
        /// <param name="url">The URL to open in Telegram.</param>
        public static void OpenTelegramLink(string url) =>
            InvokeMethodWithParam("openTelegramLink", url);

        // Open invoice
        /// <summary>
        /// Opens an invoice.
        /// </summary>
        /// <param name="url">The URL of the invoice to open.</param>
        public static void OpenInvoice(string url) => InvokeMethodWithParam("openInvoice", url);

        // Share to story
        /// <summary>
        /// Shares media to the story.
        /// </summary>
        /// <param name="mediaUrl">The URL of the media to share.</param>
        public static void ShareToStory(string mediaUrl) =>
            InvokeMethodWithParam("shareToStory", mediaUrl);

        // Show popup
        /// <summary>
        /// Shows a popup with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters for the popup.</param>
        public static void ShowPopup(PopupParams parameters) =>
            InvokeMethodWithJsonStringParam("showPopup", JsonUtility.ToJson(parameters));

        // Show QR code scanner popup
        /// <summary>
        /// Shows a QR code scanner popup with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters for the QR code scanner popup.</param>
        public static void ShowScanQrPopup(ScanQrPopupParams parameters) =>
            InvokeMethodWithJsonStringParam("showScanQrPopup", JsonUtility.ToJson(parameters));

        // Read text from clipboard
        /// <summary>
        /// Reads text from the clipboard.
        /// </summary>
        public static void ReadTextFromClipboard() => InvokeMethod("readTextFromClipboard");

        // Request write access
        /// <summary>
        /// Requests write access.
        /// </summary>
        public static void RequestWriteAccess() => InvokeMethod("requestWriteAccess");

        // Request contact
        /// <summary>
        /// Requests a contact.
        /// </summary>
        public static void RequestContact() => InvokeMethod("requestContact");

        // Set bottom bar color
        /// <summary>
        /// Sets the bottom bar color.
        /// </summary>
        /// <param name="color">The color to set for the bottom bar.</param>
        public static void SetBottomBarColor(string color) =>
            InvokeMethodWithParam("setBottomBarColor", color);

        // Set header and background colors
        /// <summary>
        /// Sets the header color.
        /// </summary>
        /// <param name="color">The color to set for the header.</param>
        public static void SetHeaderColor(string color) =>
            InvokeMethodWithParam("setHeaderColor", color);

        /// <summary>
        /// Sets the background color.
        /// </summary>
        /// <param name="color">The color to set for the background.</param>
        public static void SetBackgroundColor(string color) =>
            InvokeMethodWithParam("setBackgroundColor", color);

        /// <summary>
        /// Requests opening the Mini App in fullscreen mode.
        /// </summary>
        public static void RequestFullscreen() => InvokeMethod("requestFullscreen");

        /// <summary>
        /// Requests exiting fullscreen mode.
        /// </summary>
        public static void ExitFullscreen() => InvokeMethod("exitFullscreen");

        /// <summary>
        /// Locks the Mini App's orientation to its current mode.
        /// </summary>
        public static void LockOrientation() => InvokeMethod("lockOrientation");

        /// <summary>
        /// Unlocks the Mini App's orientation, allowing it to follow the device's rotation.
        /// </summary>
        public static void UnlockOrientation() => InvokeMethod("unlockOrientation");

        /// <summary>
        /// Prompts the user to add the Mini App to the home screen.
        /// </summary>
        public static void AddToHomeScreen() => InvokeMethod("addToHomeScreen");

        /// <summary>
        /// Checks if adding to the home screen is supported and if the Mini App has already been added.
        /// </summary>
        /// <param name="callback">Optional callback function to handle the home screen status.</param>
        public static void CheckHomeScreenStatus() => InvokeMethod("checkHomeScreenStatus");

        /// <summary>
        /// A method that opens a dialog allowing the user to share a message provided by the bot.
        /// If an optional callback parameter is provided, the callback function will be called with a boolean
        /// as the first argument, indicating whether the message was successfully sent. The message id passed
        /// to this method must belong to a PreparedInlineMessage previously obtained via the Bot API method
        /// savePreparedInlineMessage.
        /// </summary>
        public static void ShareMessage(string msgId) =>
            InvokeMethodWithParam("shareMessage", msgId);

        /// <summary>
        /// A method that opens a dialog allowing the user to set the specified custom emoji as their status.
        /// An optional params argument of type EmojiStatusParams specifies additional settings, such as duration.
        /// If an optional callback parameter is provided, the callback function will be called with a boolean
        /// as the first argument, indicating whether the status was set.
        /// Note: this method opens a native dialog and cannot be used to set the emoji status without manual
        /// user interaction. For fully programmatic changes, you should instead use the Bot API method
        /// setUserEmojiStatus after obtaining authorization to do so via the Mini App method
        /// requestEmojiStatusAccess.
        /// </summary>
        public static void SetEmojiStatus(string custom_emoji_id) =>
            InvokeMethodWithParam("setEmojiStatus", custom_emoji_id);

        /// <summary>
        /// A method that shows a native popup requesting permission for the bot to manage user's emoji status.
        /// If an optional callback parameter was passed, the callback function will be called when the popup
        /// is closed and the first argument will be a boolean indicating whether the user granted this access.
        /// </summary>
        public static void RequestEmojiStatusAccess() => InvokeMethod("requestEmojiStatusAccess");

        /// <summary>
        /// A method that displays a native popup prompting the user to download a file specified by the
        /// params argument of type DownloadFileParams. If an optional callback parameter is provided, the
        /// callback function will be called when the popup is closed, with the first argument as a boolean
        /// indicating whether the user accepted the download request.
        /// </summary>
        public static void DownloadFile(DownloadFileParams fileParams) =>
            InvokeMethodWithJsonStringParam("downloadFile", JsonUtility.ToJson(fileParams));
    }
}
