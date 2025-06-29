/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object defines the parameters for starting gyroscope tracking.
    /// </summary>
    public struct GyroscopeStartParams
    {
        /// <summary>
        /// Optional. The refresh rate in milliseconds, with acceptable values ranging from 20 to 1000. 
        /// Set to 1000 by default. Note that refresh_rate may not be supported on all platforms, 
        /// so the actual tracking frequency may differ from the specified value.
        /// </summary>
        public int? refresh_rate; // Refresh rate for gyroscope data
    }

    /// <summary>
    /// This object provides access to gyroscope data on the device.
    /// </summary>
    public class Gyroscope
    {
        /// <summary>
        /// Indicates whether gyroscope tracking is currently active.
        /// </summary>
        [SerializeField]
        public bool isStarted;

        /// <summary>
        /// The current rotation rate around the X-axis, measured in rad/s.
        /// </summary>
        [SerializeField]
        public float x;

        /// <summary>
        /// The current rotation rate around the Y-axis, measured in rad/s.
        /// </summary>
        [SerializeField]
        public float y;

        /// <summary>
        /// The current rotation rate around the Z-axis, measured in rad/s.
        /// </summary>
        [SerializeField]
        public float z;

        /// <summary>
        /// Starts tracking gyroscope data using params of type GyroscopeStartParams. 
        /// If an optional callback parameter is provided, the callback function will be called 
        /// with a boolean indicating whether tracking was successfully started.
        /// </summary>
        public Gyroscope Start(GyroscopeStartParams parameters)
        {
            TelegramWebApp.InvokeMethodWithJsonStringParam(
                "Gyroscope.start",
                JsonUtility.ToJson(parameters)
            );
            return this; // Allow chaining
        }

        /// <summary>
        /// Stops tracking gyroscope data. If an optional callback parameter is provided, 
        /// the callback function will be called with a boolean indicating whether tracking was successfully stopped.
        /// </summary>
        public Gyroscope Stop()
        {
            TelegramWebApp.InvokeMethod("Gyroscope.stop");
            return this; // Allow chaining
        }
    }
}