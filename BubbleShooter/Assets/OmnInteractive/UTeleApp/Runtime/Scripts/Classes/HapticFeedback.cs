/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object controls haptic feedback.
    /// </summary>
    public class HapticFeedback
    {
        public static class HapticStyles
        {
            public const string Light = "light"; // Indicates a collision between small or lightweight UI objects.
            public const string Medium = "medium"; // Indicates a collision between medium-sized or medium-weight UI objects.
            public const string Heavy = "heavy"; // Indicates a collision between large or heavyweight UI objects.
            public const string Rigid = "rigid"; // Indicates a collision between hard or inflexible UI objects.
            public const string Soft = "soft"; // Indicates a collision between soft or flexible UI objects.
        }

        public static class NotificationTypes
        {
            public const string Error = "error"; // Indicates that a task or action has failed.
            public const string Success = "success"; // Indicates that a task or action has completed successfully.
            public const string Warning = "warning"; // Indicates that a task or action produced a warning.
        }

        /// <summary>
        /// Shows whether haptic feedback is available on the device.
        /// </summary>
        [SerializeField]
        public bool isAvailable;

        /// <summary>
        /// Shows whether haptic feedback is currently enabled.
        /// </summary>
        [SerializeField]
        public bool isEnabled;

        /// <summary>
        /// Bot API 6.1+ A method tells that an impact occurred. 
        /// The Telegram app may play the appropriate haptics based on style value passed.
        /// </summary>
        /// <param name="style">The style of the impact. Can be one of the following values: 
        /// light, medium, heavy, rigid, soft.</param>
        public void ImpactOccurred(string style = HapticStyles.Light)
        {
            TelegramWebApp.InvokeMethodWithParam("HapticFeedback.impactOccurred", style);
        }

        /// <summary>
        /// Bot API 6.1+ A method tells that a task or action has succeeded, failed, or produced a warning. 
        /// The Telegram app may play the appropriate haptics based on type value passed.
        /// </summary>
        /// <param name="type">The type of notification. Can be one of the following values: 
        /// error, success, warning.</param>
        public void NotificationOccurred(string type = NotificationTypes.Success) // Default to success
        {
            TelegramWebApp.InvokeMethodWithParam("HapticFeedback.notificationOccurred", type);
        }

        /// <summary>
        /// Bot API 6.1+ A method tells that the user has changed a selection. 
        /// The Telegram app may play the appropriate haptics.
        /// Do not use this feedback when the user makes or confirms a selection; 
        /// use it only when the selection changes.
        /// </summary>
        public void SelectionChanged()
        {
            TelegramWebApp.InvokeMethod("HapticFeedback.selectionChanged");
        }
    }
}
