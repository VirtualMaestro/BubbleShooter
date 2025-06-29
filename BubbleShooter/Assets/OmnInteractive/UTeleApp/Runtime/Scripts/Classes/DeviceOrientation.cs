/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object defines the parameters for starting device orientation tracking.
    /// </summary>
    public struct DeviceOrientationStartParams
    {
        /// <summary>
        /// Optional. The refresh rate in milliseconds, with acceptable values ranging from 20 to 1000.
        /// Set to 1000 by default. Note that refresh_rate may not be supported on all platforms,
        /// so the actual tracking frequency may differ from the specified value.
        /// </summary>
        public int? refresh_rate; // Refresh rate for orientation data

        /// <summary>
        /// Optional. Pass true to receive absolute orientation data, allowing you to determine
        /// the device's attitude relative to magnetic north. Use this option if implementing
        /// features like a compass in your app. If relative data is sufficient, pass false.
        /// Set to false by default.
        /// </summary>
        public bool? need_absolute;
    }

    /// <summary>
    /// This object provides access to orientation data on the device.
    /// </summary>
    public class DeviceOrientation
    {
        /// <summary>
        /// Indicates whether device orientation tracking is currently active.
        /// </summary>
        [SerializeField]
        public bool isStarted;

        /// <summary>
        /// A boolean that indicates whether or not the device is providing orientation data in absolute values.
        /// </summary>
        [SerializeField]
        public bool absolute;

        /// <summary>
        /// The rotation around the Z-axis, measured in radians.
        /// </summary>
        [SerializeField]
        public float alpha;

        /// <summary>
        /// The rotation around the X-axis, measured in radians.
        /// </summary>
        [SerializeField]
        public float beta;

        /// <summary>
        /// The rotation around the Y-axis, measured in radians.
        /// </summary>
        [SerializeField]
        public float gamma;

        /// <summary>
        /// Starts tracking device orientation data using params of type DeviceOrientationStartParams.
        /// If an optional callback parameter is provided, the callback function will be called with
        /// a boolean indicating whether tracking was successfully started.
        /// </summary>
        /// <param name="refreshRate">The refresh rate for orientation data.</param>
        /// <param name="need_absolute">Indicates whether absolute orientation data is needed.</param>
        public void Start(DeviceOrientationStartParams parameters)
        {
            TelegramWebApp.InvokeMethodWithJsonStringParam(
                "DeviceOrientation.start",
                JsonUtility.ToJson(parameters)
            );
        }

        /// <summary>
        /// Stops tracking device orientation data. If an optional callback parameter is provided,
        /// the callback function will be called with a boolean indicating whether tracking was successfully stopped.
        /// </summary>
        public void Stop()
        {
            TelegramWebApp.InvokeMethod("DeviceOrientation.stop");
        }
    }
}
