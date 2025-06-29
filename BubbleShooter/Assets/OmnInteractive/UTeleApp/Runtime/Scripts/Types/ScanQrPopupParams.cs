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
    /// Represents the parameters for displaying a QR code scan popup.
    /// </summary>
    [Serializable]
    public struct ScanQrPopupParams
    {
        /// <summary>
        /// The text to be displayed in the QR code scan popup.
        /// </summary>
        public string text;
    }

}
