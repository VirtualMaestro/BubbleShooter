/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object controls the Settings item in the context menu of the Mini App in the Telegram interface.
    /// </summary>
    [System.Serializable]
    public class SettingsButton
    {
        /// <summary>
        /// Shows whether the context menu item is visible. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool isVisible;

        /// <summary>
        /// A method to make the Settings item in the context menu visible.
        /// </summary>
        public SettingsButton Show()
        {
            isVisible = true;
            TelegramWebApp.InvokeMethod("SettingsButton.show");
            return this;
        }

        /// <summary>
        /// A method to hide the Settings item in the context menu.
        /// </summary>
        public SettingsButton Hide()
        {
            isVisible = false;
            TelegramWebApp.InvokeMethod("SettingsButton.hide");
            return this;
        }
    }
}