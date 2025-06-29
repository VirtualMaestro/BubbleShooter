/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// Static class for handling Telegram Web App events.
    /// </summary>
    public static class TelegramWebAppEvents
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        /// <summary>
        /// Registers a callback for a specific event.
        /// </summary>
        /// <param name="eventName">The name of the event to register for.</param>
        /// <param name="callback">The callback to invoke when the event occurs.</param>
        [DllImport("__Internal")]
        private static extern void RegisterEventCallback(string eventName, Action callback);

        /// <summary>
        /// Unregisters a callback for a specific event.
        /// </summary>
        /// <param name="eventName">The name of the event to unregister from.</param>
        [DllImport("__Internal")]
        private static extern void UnregisterEventCallback(string eventName);

        /// <summary>
        /// Registers a callback for a specific event with a JSON string parameter.
        /// </summary>
        /// <param name="eventName">The name of the event to register for.</param>
        /// <param name="callback">The callback to invoke when the event occurs, with a JSON string parameter.</param>
        [DllImport("__Internal")]
        private static extern void RegisterEventCallbackWithJsonString(
            string eventName,
            Action<string> callback
        );
#else
        /// <summary>
        /// Logs a warning for unsupported platforms.
        /// </summary>
        private static void LogUnsupportedPlatform()
        {
            Debug.LogWarning(TelegramWebApp.UNSUPPORTED_PLATFORM_MESSAGE);
        }

        /// <summary>
        /// Registers a callback for a specific event (unsupported platform).
        /// </summary>
        /// <param name="eventName">The name of the event to register for.</param>
        /// <param name="callback">The callback to invoke when the event occurs.</param>
        private static void RegisterEventCallback(string eventName, Action callback) =>
            LogUnsupportedPlatform();

        /// <summary>
        /// Unregisters a callback for a specific event (unsupported platform).
        /// </summary>
        /// <param name="eventName">The name of the event to unregister from.</param>
        private static void UnregisterEventCallback(string eventName) => LogUnsupportedPlatform();

        /// <summary>
        /// Registers a callback for a specific event with a JSON string parameter (unsupported platform).
        /// </summary>
        /// <param name="eventName">The name of the event to register for.</param>
        /// <param name="callback">The callback to invoke when the event occurs, with a JSON string parameter.</param>
        private static void RegisterEventCallbackWithJsonString(
            string eventName,
            Action<string> callback
        ) => LogUnsupportedPlatform();
#endif

        /// <summary>
        /// Event triggered when activated .
        /// </summary>
        public static event Action OnActivated;

        /// <summary>
        /// Event triggered when deactivated .
        /// </summary>
        public static event Action OnDeactivated;

        /// <summary>
        /// Event triggered when the theme changes.
        /// </summary>
        public static event Action OnThemeChanged;

        /// <summary>
        /// Event triggered when the viewport changes.
        /// </summary>
        public static event Action<ViewportChangedEventArgs> OnViewportChanged;

        /// <summary>
        /// Event triggered when safeAreaChanged.
        /// </summary>
        public static event Action OnSafeAreaChanged;

        /// <summary>
        /// Event triggered when contentSafeAreaChanged.
        /// </summary>
        public static event Action OnContentSafeAreaChanged;

        /// <summary>
        /// Event triggered when the main button is clicked.
        /// </summary>
        public static event Action OnMainButtonClicked;

        /// <summary>
        /// Event triggered when the secondary button is clicked.
        /// </summary>
        public static event Action OnSecondaryButtonClicked;

        /// <summary>
        /// Event triggered when the back button is clicked.
        /// </summary>
        public static event Action OnBackButtonClicked;

        /// <summary>
        /// Event triggered when the settings button is clicked.
        /// </summary>
        public static event Action OnSettingsButtonClicked;

        /// <summary>
        /// Event triggered when an invoice is closed.
        /// </summary>
        public static event Action<InvoiceClosedEventArgs> OnInvoiceClosed;

        /// <summary>
        /// Event triggered when a popup is closed.
        /// </summary>
        public static event Action<PopUpClosedEventArgs> OnPopupClosed;

        /// <summary>
        /// Event triggered when QR text is received.
        /// </summary>
        public static event Action<QrTextReceivedEventArgs> OnQrTextReceived;

        /// <summary>
        /// Event triggered when the QR scanner popup is closed.
        /// </summary>
        public static event Action OnScanQrPopupClosed;

        /// <summary>
        /// Event triggered when text is received from the clipboard.
        /// </summary>
        public static event Action<ClipboardTextReceivedEventArgs> OnClipboardTextReceived;

        /// <summary>
        /// Event triggered when write access is requested.
        /// </summary>
        public static event Action<WriteAccessRequestedEventArgs> OnWriteAccessRequested;

        /// <summary>
        /// Event triggered when a contact is requested.
        /// </summary>
        public static event Action<ContactRequestedEventArgs> OnContactRequested;

        /// <summary>
        /// Event triggered when the biometric manager is updated.
        /// </summary>
        public static event Action OnBiometricManagerUpdated;

        /// <summary>
        /// Event triggered when biometric authentication is requested.
        /// </summary>
        public static event Action<BiometricAuthRequestedEventArgs> OnBiometricAuthRequested;

        /// <summary>
        /// Event triggered when a biometric token is updated.
        /// </summary>
        public static event Action<BiometricTokenUpdatedEventArgs> OnBiometricTokenUpdated;

        /// <summary>
        /// Event triggered when the fullscreen state changes.
        /// </summary>
        public static event Action OnFullscreenChanged;

        /// <summary>
        /// Event triggered when entering fullscreen fails.
        /// </summary>
        public static event Action<FullscreenFailedEventArgs> OnFullscreenFailed;

        /// <summary>
        /// Event triggered when the Mini App is added to the home screen.
        /// </summary>
        public static event Action OnHomeScreenAdded;

        /// <summary>
        /// Event triggered after checking the home screen status.
        /// </summary>
        public static event Action<HomeScreenCheckedEventArgs> OnHomeScreenChecked;

        /// <summary>
        /// Event triggered when accelerometer tracking starts.
        /// </summary>
        public static event Action OnAccelerometerStarted;

        /// <summary>
        /// Event triggered when accelerometer tracking stops.
        /// </summary>
        public static event Action OnAccelerometerStopped;

        /// <summary>
        /// Event triggered when accelerometer data changes.
        /// </summary>
        public static event Action OnAccelerometerChanged;

        /// <summary>
        /// Event triggered when accelerometer tracking fails.
        /// </summary>
        public static event Action<AccelerometerFailedEventArgs> OnAccelerometerFailed;

        /// <summary>
        /// Event triggered when device orientation tracking starts.
        /// </summary>
        public static event Action OnDeviceOrientationStarted;

        /// <summary>
        /// Event triggered when device orientation tracking stops.
        /// </summary>
        public static event Action OnDeviceOrientationStopped;

        /// <summary>
        /// Event triggered when device orientation data changes.
        /// </summary>
        public static event Action OnDeviceOrientationChanged;

        /// <summary>
        /// Event triggered when device orientation tracking fails.
        /// </summary>
        public static event Action<DeviceOrientationFailedEventArgs> OnDeviceOrientationFailed;

        /// <summary>
        /// Event triggered when gyroscope tracking starts.
        /// </summary>
        public static event Action OnGyroscopeStarted;

        /// <summary>
        /// Event triggered when gyroscope tracking stops.
        /// </summary>
        public static event Action OnGyroscopeStopped;

        /// <summary>
        /// Event triggered when gyroscope data changes.
        /// </summary>
        public static event Action OnGyroscopeChanged;

        /// <summary>
        /// Event triggered when gyroscope tracking fails.
        /// </summary>
        public static event Action<GyroscopeFailedEventArgs> OnGyroscopeFailed;

        /// <summary>
        /// Event triggered when the location manager is updated.
        /// </summary>
        public static event Action OnLocationManagerUpdated;

        /// <summary>
        /// Event triggered when location data is requested.
        /// </summary>
        public static event Action OnLocationRequested;

        /// <summary>
        /// Event triggered when a message is successfully shared.
        /// </summary>
        public static event Action OnShareMessageSent;

        /// <summary>
        /// Event triggered when sharing a message fails.
        /// </summary>
        public static event Action<ShareMessageFailedEventArgs> OnShareMessageFailed;

        /// <summary>
        /// Event triggered when the emoji status is successfully set.
        /// </summary>
        public static event Action OnEmojiStatusSet;

        /// <summary>
        /// Event triggered when setting the emoji status fails.
        /// </summary>
        public static event Action<EmojiStatusFailedEventArgs> OnEmojiStatusFailed;

        /// <summary>
        /// Event triggered when the write permission for emoji status is requested.
        /// </summary>
        public static event Action<EmojiStatusAccessRequestedEventArgs> OnEmojiStatusAccessRequested;

        /// <summary>
        /// Event triggered when a file download request is made.
        /// </summary>
        public static event Action<FileDownloadRequestedEventArgs> OnFileDownloadRequested;

        /// <summary>
        /// Static constructor to register event callbacks.
        /// </summary>
        static TelegramWebAppEvents()
        {
            RegisterEventCallbacks();
        }

        /// <summary>
        /// Registers all event callbacks.
        /// </summary>
        private static void RegisterEventCallbacks()
        {
            RegisterEventCallback("activated", OnActivatedStatic);
            RegisterEventCallback("deactivated", OnDeactivatedStatic);
            RegisterEventCallback("themeChanged", OnThemeChangedStatic);
            RegisterEventCallbackWithJsonString("viewportChanged", OnViewportChangedStatic);
            RegisterEventCallback("safeAreaChanged", OnActivatedStatic);
            RegisterEventCallback("contentSafeAreaChanged", OnContentSafeAreaChangedStatic);
            RegisterEventCallback("mainButtonClicked", OnMainButtonClickedStatic);
            RegisterEventCallback("secondaryButtonClicked", OnSecondaryButtonClickedStatic);
            RegisterEventCallback("backButtonClicked", OnBackButtonClickedStatic);
            RegisterEventCallback("settingsButtonClicked", OnSettingsButtonClickedStatic);
            RegisterEventCallbackWithJsonString("invoiceClosed", OnInvoiceClosedStatic);
            RegisterEventCallbackWithJsonString("popupClosed", OnPopupClosedStatic);
            RegisterEventCallbackWithJsonString("qrTextReceived", OnQrTextReceivedStatic);
            RegisterEventCallback("scanQrPopupClosed", OnScanQrPopupClosedStatic);
            RegisterEventCallbackWithJsonString(
                "clipboardTextReceived",
                OnClipboardTextReceivedStatic
            );
            RegisterEventCallbackWithJsonString(
                "writeAccessRequested",
                OnWriteAccessRequestedStatic
            );
            RegisterEventCallbackWithJsonString("contactRequested", OnContactRequestedStatic);
            RegisterEventCallback("biometricManagerUpdated", OnBiometricManagerUpdatedStatic);
            RegisterEventCallbackWithJsonString(
                "biometricAuthRequested",
                OnBiometricAuthRequestedStatic
            );
            RegisterEventCallbackWithJsonString(
                "biometricTokenUpdated",
                OnBiometricTokenUpdatedStatic
            );
            RegisterEventCallback("fullscreenChanged", OnFullscreenChangedStatic);
            RegisterEventCallbackWithJsonString("fullscreenFailed", OnFullscreenFailedStatic);
            RegisterEventCallback("homeScreenAdded", OnHomeScreenAddedStatic);
            RegisterEventCallbackWithJsonString("homeScreenChecked", OnHomeScreenCheckedStatic);
            RegisterEventCallback("accelerometerStarted", OnAccelerometerStartedStatic);
            RegisterEventCallback("accelerometerStopped", OnAccelerometerStoppedStatic);
            RegisterEventCallback("accelerometerChanged", OnAccelerometerChangedStatic);
            RegisterEventCallbackWithJsonString("accelerometerFailed", OnAccelerometerFailedStatic);
            RegisterEventCallback("deviceOrientationStarted", OnDeviceOrientationStartedStatic);
            RegisterEventCallback("deviceOrientationStopped", OnDeviceOrientationStoppedStatic);
            RegisterEventCallback("deviceOrientationChanged", OnDeviceOrientationChangedStatic);
            RegisterEventCallbackWithJsonString(
                "deviceOrientationFailed",
                OnDeviceOrientationFailedStatic
            );
            RegisterEventCallback("gyroscopeStarted", OnGyroscopeStartedStatic);
            RegisterEventCallback("gyroscopeStopped", OnGyroscopeStoppedStatic);
            RegisterEventCallback("gyroscopeChanged", OnGyroscopeChangedStatic);
            RegisterEventCallbackWithJsonString("gyroscopeFailed", OnGyroscopeFailedStatic);
            RegisterEventCallback("locationManagerUpdated", OnLocationManagerUpdatedStatic);
            RegisterEventCallbackWithJsonString("locationRequested", OnLocationRequestedStatic);
            RegisterEventCallback("shareMessageSent", OnShareMessageSentStatic);
            RegisterEventCallbackWithJsonString("shareMessageFailed", OnShareMessageFailedStatic);
            RegisterEventCallback("emojiStatusSet", OnEmojiStatusSetStatic);
            RegisterEventCallbackWithJsonString("emojiStatusFailed", OnEmojiStatusFailedStatic);
            RegisterEventCallbackWithJsonString(
                "emojiStatusAccessRequested",
                OnEmojiStatusAccessRequestedStatic
            );
            RegisterEventCallbackWithJsonString(
                "fileDownloadRequested",
                OnFileDownloadRequestedStatic
            );
        }

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnActivatedStatic() => OnActivated?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnDeactivatedStatic() => OnDeactivated?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnThemeChangedStatic() => OnThemeChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnViewportChangedStatic(string data) =>
            OnViewportChanged?.Invoke(JsonUtility.FromJson<ViewportChangedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnSafeAreaChangedStatic() => OnSafeAreaChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnContentSafeAreaChangedStatic() => OnContentSafeAreaChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnMainButtonClickedStatic() => OnMainButtonClicked?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnSecondaryButtonClickedStatic() => OnSecondaryButtonClicked?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnBackButtonClickedStatic() => OnBackButtonClicked?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnSettingsButtonClickedStatic() => OnSettingsButtonClicked?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnInvoiceClosedStatic(string data) =>
            OnInvoiceClosed?.Invoke(JsonUtility.FromJson<InvoiceClosedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnPopupClosedStatic(string data) =>
            OnPopupClosed?.Invoke(JsonUtility.FromJson<PopUpClosedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnQrTextReceivedStatic(string data) =>
            OnQrTextReceived?.Invoke(JsonUtility.FromJson<QrTextReceivedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnScanQrPopupClosedStatic() => OnScanQrPopupClosed?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnClipboardTextReceivedStatic(string data) =>
            OnClipboardTextReceived?.Invoke(
                JsonUtility.FromJson<ClipboardTextReceivedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWriteAccessRequestedStatic(string data) =>
            OnWriteAccessRequested?.Invoke(
                JsonUtility.FromJson<WriteAccessRequestedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnContactRequestedStatic(string data) =>
            OnContactRequested?.Invoke(JsonUtility.FromJson<ContactRequestedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnBiometricManagerUpdatedStatic() =>
            OnBiometricManagerUpdated?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnBiometricAuthRequestedStatic(string data) =>
            OnBiometricAuthRequested?.Invoke(
                JsonUtility.FromJson<BiometricAuthRequestedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnBiometricTokenUpdatedStatic(string data) =>
            OnBiometricTokenUpdated?.Invoke(
                JsonUtility.FromJson<BiometricTokenUpdatedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnFullscreenChangedStatic() => OnFullscreenChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnFullscreenFailedStatic(string data) =>
            OnFullscreenFailed?.Invoke(JsonUtility.FromJson<FullscreenFailedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnHomeScreenAddedStatic() => OnHomeScreenAdded?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnHomeScreenCheckedStatic(string data) =>
            OnHomeScreenChecked?.Invoke(JsonUtility.FromJson<HomeScreenCheckedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnAccelerometerStartedStatic() => OnAccelerometerStarted?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnAccelerometerStoppedStatic() => OnAccelerometerStopped?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnAccelerometerChangedStatic() => OnAccelerometerChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnAccelerometerFailedStatic(string data) =>
            OnAccelerometerFailed?.Invoke(
                JsonUtility.FromJson<AccelerometerFailedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnDeviceOrientationStartedStatic() =>
            OnDeviceOrientationStarted?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnDeviceOrientationStoppedStatic() =>
            OnDeviceOrientationStopped?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnDeviceOrientationChangedStatic() =>
            OnDeviceOrientationChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnDeviceOrientationFailedStatic(string data) =>
            OnDeviceOrientationFailed?.Invoke(
                JsonUtility.FromJson<DeviceOrientationFailedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnGyroscopeStartedStatic() => OnGyroscopeStarted?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnGyroscopeStoppedStatic() => OnGyroscopeStopped?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGyroscopeChangedStatic() => OnGyroscopeChanged?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGyroscopeFailedStatic(string data) =>
            OnGyroscopeFailed?.Invoke(JsonUtility.FromJson<GyroscopeFailedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnLocationManagerUpdatedStatic() => OnLocationManagerUpdated?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnLocationRequestedStatic(string data) => OnLocationRequested?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnShareMessageSentStatic() => OnShareMessageSent?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnShareMessageFailedStatic(string data) =>
            OnShareMessageFailed?.Invoke(JsonUtility.FromJson<ShareMessageFailedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action))]
        private static void OnEmojiStatusSetStatic() => OnEmojiStatusSet?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnEmojiStatusFailedStatic(string data) =>
            OnEmojiStatusFailed?.Invoke(JsonUtility.FromJson<EmojiStatusFailedEventArgs>(data));

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnEmojiStatusAccessRequestedStatic(string data) =>
            OnEmojiStatusAccessRequested?.Invoke(
                JsonUtility.FromJson<EmojiStatusAccessRequestedEventArgs>(data)
            );

        [AOT.MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnFileDownloadRequestedStatic(string data) =>
            OnFileDownloadRequested?.Invoke(
                JsonUtility.FromJson<FileDownloadRequestedEventArgs>(data)
            );

        /// <summary>
        /// Registers an event with a callback.
        /// </summary>
        /// <param name="eventName">The name of the event to register.</param>
        /// <param name="callback">The callback to invoke when the event occurs.</param>
        public static void RegisterEvent(string eventName, Action callback) =>
            RegisterEventCallback(eventName, callback);

        /// <summary>
        /// Registers an event with a callback that takes a parameter of type T.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="eventName">The name of the event to register.</param>
        /// <param name="callback">The callback to invoke when the event occurs, with a parameter of type T.</param>
        public static void RegisterEvent<T>(string eventName, Action<T> callback) =>
            RegisterEventCallbackWithJsonString(
                eventName,
                data => callback(JsonUtility.FromJson<T>(data))
            );

        /// <summary>
        /// Unregisters an event.
        /// </summary>
        /// <param name="eventName">The name of the event to unregister.</param>
        public static void UnregisterEvent(string eventName) => UnregisterEventCallback(eventName);
    }
}
