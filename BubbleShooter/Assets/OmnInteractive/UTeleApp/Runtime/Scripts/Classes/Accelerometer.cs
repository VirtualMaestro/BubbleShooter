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
    /// This object defines the parameters for starting accelerometer tracking.
    /// </summary>
    public struct AccelerometerStartParams
    {
        /// <summary>
        /// Optional. The refresh rate in milliseconds, with acceptable values ranging from 20 to 1000. 
        /// Set to 1000 by default. Note that refresh_rate may not be supported on all platforms, 
        /// so the actual tracking frequency may differ from the specified value.
        /// </summary>
        public int? refresh_rate;
    }

    /// <summary>
    /// This object provides access to accelerometer data on the device.
    /// </summary>
    public class Accelerometer
    {
        [Serializable]
        private struct AccelerometerData
        {
            public bool isStarted;
            public float x;
            public float y;
            public float z;
        }

        private AccelerometerData data;

        /// <summary>
        /// Indicates whether accelerometer tracking is currently active.
        /// </summary>
        [SerializeField]
        public bool isStarted;

        /// <summary>
        /// The current acceleration in the X-axis, measured in m/s².
        /// </summary>
        [SerializeField]
        public float x;

        /// <summary>
        /// The current acceleration in the Y-axis, measured in m/s².
        /// </summary>
        [SerializeField]
        public float y;

        /// <summary>
        /// The current acceleration in the Z-axis, measured in m/s².
        /// </summary>
        [SerializeField]
        public float z;

        public void UpdateFromJson(string json)
        {
            data = JsonUtility.FromJson<AccelerometerData>(json);
        }

        /// <summary>
        /// Starts tracking accelerometer data using params of type AccelerometerStartParams. 
        /// If an optional callback parameter is provided, the callback function will be called 
        /// with a boolean indicating whether tracking was successfully started.
        /// </summary>
        /// <param name="refreshRate">The refresh rate in milliseconds.</param>
        public Accelerometer Start(AccelerometerStartParams parameters)
        {
            // Validate refresh rate is between 20-1000ms
            if (parameters.refresh_rate.HasValue)
            {
                parameters.refresh_rate = Mathf.Clamp(parameters.refresh_rate.Value, 20, 1000);
            }
            TelegramWebApp.InvokeMethodWithJsonStringParam(
                "Accelerometer.start",
                JsonUtility.ToJson(parameters)
            );
            return this; // Allow chaining
        }

        /// <summary>
        /// Stops tracking accelerometer data. If an optional callback parameter is provided, 
        /// the callback function will be called with a boolean indicating whether tracking was successfully stopped.
        /// </summary>
        public Accelerometer Stop()
        {
            TelegramWebApp.InvokeMethod(
                "Accelerometer.stop"
            );
            return this; // Allow chaining
        }
    }
}
