/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object controls the back button, which can be displayed in the header of the Mini App in the Telegram interface.
    /// </summary>
    [System.Serializable]
    public class BackButton
    {
        /// <summary>
        /// Shows whether the button is visible. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool isVisible;

        /// <summary>
        /// A method to make the button active and visible.
        /// </summary>
        public BackButton Show()
        {
            isVisible = true;
            TelegramWebApp.InvokeMethod("BackButton.show");
            return this;
        }

        /// <summary>
        /// A method to hide the button.
        /// </summary>
        public BackButton Hide()
        {
            isVisible = false;
            TelegramWebApp.InvokeMethod("BackButton.hide");
            return this;
        }
    }
}