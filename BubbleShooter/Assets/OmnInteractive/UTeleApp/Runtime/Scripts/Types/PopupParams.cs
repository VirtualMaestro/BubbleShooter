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
    /// Represents the parameters for displaying a popup.
    /// </summary>
    [Serializable]
    public struct PopupParams
    {
        /// <summary>
        /// The title of the popup.
        /// </summary>
        public string title;

        /// <summary>
        /// The message displayed in the popup.
        /// </summary>
        public string message;

        /// <summary>
        /// The buttons available in the popup.
        /// </summary>
        public PopupBotton[] bottons;
    }

    /// <summary>
    /// Represents a button in the popup.
    /// </summary>
    [Serializable]
    public struct PopupBotton
    {
        /// <summary>
        /// The unique identifier for the button.
        /// </summary>
        public string id;

        /// <summary>
        /// The type of the button (e.g., "default", "cancel").
        /// </summary>
        public string type;

        /// <summary>
        /// The text displayed on the button.
        /// </summary>
        public string text;
    }
}
