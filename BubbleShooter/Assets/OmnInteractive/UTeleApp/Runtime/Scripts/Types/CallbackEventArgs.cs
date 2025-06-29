/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// Occurs when the visible section of the Mini App is changed.
    /// eventHandler receives an object with the single field isStateStable.
    /// If isStateStable is true, the resizing of the Mini App is finished.
    /// If it is false, the resizing is ongoing (the user is expanding or collapsing the Mini App or an animated object is playing).
    /// The current value of the visible section’s height is available in this.viewportHeight.
    /// </summary>
    [Serializable]
    public struct ViewportChangedEventArgs
    {
        public bool isStateStable;
    }

    /// <summary>
    /// Occurs when the opened invoice is closed.
    /// eventHandler receives an object with the two fields:
    /// url - invoice link provided and
    /// status - one of the invoice statuses:
    /// - paid - invoice was paid successfully,
    /// - cancelled - user closed this invoice without paying,
    /// - failed - user tried to pay, but the payment was failed,
    /// - pending - the payment is still processing. The bot will receive a service message about a successful payment when the payment is successfully paid.
    /// </summary>
    [Serializable]
    public struct InvoiceClosedEventArgs
    {
        public string url;
        public string status;
    }

    /// <summary>
    /// Occurs when the opened popup is closed.
    /// eventHandler receives an object with the single field button_id - the value of the field id of the pressed button.
    /// If no buttons were pressed, the field button_id will be null.
    /// </summary>
    [Serializable]
    public struct PopUpClosedEventArgs
    {
        public string button_id;
    }

    /// <summary>
    /// Occurs when the QR code scanner catches a code with text data.
    /// eventHandler receives an object with the single field data containing text data from the QR code.
    /// </summary>
    [Serializable]
    public struct QrTextReceivedEventArgs
    {
        public string data;
    }

    /// <summary>
    /// Occurs when the readTextFromClipboard method is called.
    /// eventHandler receives an object with the single field data containing text data from the clipboard.
    /// If the clipboard contains non-text data, the field data will be an empty string.
    /// If the Mini App has no access to the clipboard, the field data will be null.
    /// </summary>
    [Serializable]
    public struct ClipboardTextReceivedEventArgs
    {
        public string data;
    }

    /// <summary>
    /// Occurs when the write permission was requested.
    /// eventHandler receives an object with the single field status containing one of the statuses:
    /// - allowed - user granted write permission to the bot,
    /// - cancelled - user declined this request.
    /// </summary>
    [Serializable]
    public struct WriteAccessRequestedEventArgs
    {
        public string status;
    }

    /// <summary>
    /// Occurs when the user's phone number was requested.
    /// eventHandler receives an object with the single field status containing one of the statuses:
    /// - sent - user shared their phone number with the bot,
    /// - cancelled - user declined this request.
    /// </summary>
    [Serializable]
    public struct ContactRequestedEventArgs
    {
        public string status;
    }

    /// <summary>
    /// Occurs whenever biometric authentication was requested.
    /// eventHandler receives an object with the field isAuthenticated containing a boolean indicating whether the user was authenticated successfully.
    /// If isAuthenticated is true, the field biometricToken will contain the biometric token stored in secure storage on the device.
    /// </summary>
    [Serializable]
    public struct BiometricAuthRequestedEventArgs
    {
        public bool isAuthenticated;
        public string biometricToken;
    }

    /// <summary>
    /// Occurs whenever the biometric token was updated.
    /// eventHandler receives an object with the single field isUpdated, containing a boolean indicating whether the token was updated.
    /// </summary>
    [Serializable]
    public struct BiometricTokenUpdatedEventArgs
    {
        public bool isUpdated;
    }

    /// <summary>
    /// Occurs if a request to enter fullscreen mode fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – Fullscreen mode is not supported on this device or platform.
    /// - ALREADY_FULLSCREEN – The Mini App is already in fullscreen mode.
    /// </summary>
    [Serializable]
    public struct FullscreenFailedEventArgs
    {
        public string error;
    }

    /// <summary>
    /// Occurs after checking the home screen status.
    /// eventHandler receives an object with the field status, which is a string indicating the current home screen status.
    /// Possible values for status are:
    /// - unsupported – the feature is not supported, and it is not possible to add the icon to the home screen,
    /// - unknown – the feature is supported, and the icon can be added, but it is not possible to determine if the icon has already been added,
    /// - added – the icon has already been added to the home screen,
    /// - missed – the icon has not been added to the home screen.
    /// </summary>
    [Serializable]
    public struct HomeScreenCheckedEventArgs
    {
        public string status;
    }

    /// <summary>
    /// Occurs with the specified frequency after calling the start method, sending the current accelerometer data.
    /// eventHandler receives no parameters, the current acceleration values can be received via this.x, this.y and this.z respectively.
    /// </summary>
    [Serializable]
    public struct AccelerometerChangedEventArgs
    {
        public float x;
        public float y;
        public float z;
    }

    /// <summary>
    /// Occurs if a request to start accelerometer tracking fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – Accelerometer tracking is not supported on this device or platform.
    /// </summary>
    [Serializable]
    public struct AccelerometerFailedEventArgs
    {
        public string error;
    }

    /// <summary>
    /// Occurs with the specified frequency after calling the start method, sending the current orientation data.
    /// eventHandler receives no parameters, the current device orientation values can be received via this.alpha, this.beta and this.gamma respectively.
    /// </summary>
    [Serializable]
    public struct DeviceOrientationChangedEventArgs
    {
        public float alpha;
        public float beta;
        public float gamma;
    }

    /// <summary>
    /// Occurs if a request to start device orientation tracking fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – Device orientation tracking is not supported on this device or platform.
    /// </summary>
    [Serializable]
    public struct DeviceOrientationFailedEventArgs
    {
        public string error;
    }

    /// <summary>
    /// Occurs with the specified frequency after calling the start method, sending the current gyroscope data.
    /// eventHandler receives no parameters, the current rotation rates can be received via this.x, this.y and this.z respectively.
    /// </summary>
    [Serializable]
    public struct GyroscopeChangedEventArgs
    {
        public float x;
        public float y;
        public float z;
    }

    /// <summary>
    /// Occurs if a request to start gyroscope tracking fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – Gyroscope tracking is not supported on this device or platform.
    /// </summary>
    [Serializable]
    public struct GyroscopeFailedEventArgs
    {
        public string error;
    }


    /// <summary>
    /// Occurs when location data is requested.
    /// eventHandler receives an object with the single field locationData of type LocationData, containing the current location information.
    /// </summary>
    [Serializable]
    public struct LocationRequestedEventArgs
    {
        public LocationData locationData;
    }

    /// <summary>
    /// Occurs if sharing the message fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – The feature is not supported by the client.
    /// - MESSAGE_EXPIRED – The message could not be retrieved because it has expired.
    /// - MESSAGE_SEND_FAILED – An error occurred while attempting to send the message.
    /// - USER_DECLINED – The user closed the dialog without sharing the message.
    /// - UNKNOWN_ERROR – An unknown error occurred.
    /// </summary>
    [Serializable]
    public struct ShareMessageFailedEventArgs
    {
        public string error;
    }

    /// <summary>
    /// Occurs if setting the emoji status fails.
    /// eventHandler receives an object with the single field error, describing the reason for the failure.
    /// Possible values for error are:
    /// - UNSUPPORTED – The feature is not supported by the client.
    /// - SUGGESTED_EMOJI_INVALID – One or more emoji identifiers are invalid.
    /// - DURATION_INVALID – The specified duration is invalid.
    /// - USER_DECLINED – The user closed the dialog without setting a status.
    /// - SERVER_ERROR – A server error occurred when attempting to set the status.
    /// - UNKNOWN_ERROR – An unknown error occurred.
    /// </summary>
    [Serializable]
    public struct EmojiStatusFailedEventArgs
    {
        public string error;
    }

    /// <summary>
    /// Occurs when the write permission was requested.
    /// eventHandler receives an object with the single field status containing one of the statuses:
    /// - allowed – user granted emoji status permission to the bot,
    /// - cancelled – user declined this request.
    /// </summary>
    [Serializable]
    public struct EmojiStatusAccessRequestedEventArgs
    {
        public string status;
    }

    /// <summary>
    /// Occurs when the user responds to the file download request.
    /// eventHandler receives an object with the single field status containing one of the statuses:
    /// - downloading – the file download has started,
    /// - cancelled – user declined this request.
    /// </summary>
    [Serializable]
    public struct FileDownloadRequestedEventArgs
    {
        public string status;
    }
}
