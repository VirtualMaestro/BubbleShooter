/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using System;
using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object controls location access on the device. Before the first use of this object, it needs to be initialized using the init method.
    /// </summary>
    public class LocationManager
    {
        /// <summary>
        /// Shows whether the LocationManager object has been initialized.
        /// </summary>
        [field: SerializeField]
        public bool isInited { get; private set; }

        /// <summary>
        /// Shows whether location services are available on the current device.
        /// </summary>
        [field: SerializeField]
        public bool isLocationAvailable { get; private set; }

        /// <summary>
        /// Shows whether permission to use location has been requested.
        /// </summary>
        [field: SerializeField]
        public bool isAccessRequested { get; private set; }

        /// <summary>
        /// Shows whether permission to use location has been granted.
        /// </summary>
        [field: SerializeField]
        public bool isAccessGranted { get; private set; }

        /// <summary>
        /// Shows whether location tracking is currently active.
        /// </summary>
        [SerializeField]
        public bool isStarted;

        /// <summary>
        /// The current latitude, in degrees.
        /// </summary>
        [SerializeField]
        public float latitude;

        /// <summary>
        /// The current longitude, in degrees.
        /// </summary>
        [SerializeField]
        public float longitude;

        /// <summary>
        /// The accuracy of the location data in meters.
        /// </summary>
        [SerializeField]
        public float accuracy;

        /// <summary>
        /// A method that initializes the LocationManager object. It should be called before the object's first use.
        /// If an optional callback parameter is provided, the callback function will be called when the object is initialized.
        /// </summary>
        public void Init()
        {
            TelegramWebApp.InvokeMethod("LocationManager.init");
        }

        /// <summary>
        /// A method that requests location data. The callback function will be called with null as the first argument if access to location was not granted,
        /// or an object of type LocationData as the first argument if access was successful.
        /// </summary>
        public void GetLocation()
        {
            TelegramWebApp.InvokeMethod("LocationManager.getLocation");
        }

        /// <summary>
        /// A method that opens the location access settings for bots. Useful when you need to request location access from users who haven't granted it yet.
        /// Note that this method can be called only in response to user interaction with the Mini App interface (e.g., a click inside the Mini App or on the main button).
        /// </summary>
        public void OpenSettings()
        {
            TelegramWebApp.InvokeMethod("LocationManager.openSettings");
        }
    }

    /// <summary>
    /// This object contains data about the current location.
    /// </summary>
    public class LocationData
    {
        /// <summary>
        /// Latitude in degrees.
        /// </summary>
        public float latitude;

        /// <summary>
        /// Longitude in degrees.
        /// </summary>
        public float longitude;

        /// <summary>
        /// Altitude above sea level in meters. null if altitude data is not available on the device.
        /// </summary>
        public float? altitude;

        /// <summary>
        /// The direction the device is moving in degrees (0 = North, 90 = East, 180 = South, 270 = West).
        /// null if course data is not available on the device.
        /// </summary>
        public float? course;

        /// <summary>
        /// The speed of the device in m/s. null if speed data is not available on the device.
        /// </summary>
        public float? speed;

        /// <summary>
        /// Accuracy of the latitude and longitude values in meters. null if horizontal accuracy data is not available on the device.
        /// </summary>
        public float? horizontal_accuracy;

        /// <summary>
        /// Accuracy of the altitude value in meters. null if vertical accuracy data is not available on the device.
        /// </summary>
        public float? vertical_accuracy;

        /// <summary>
        /// Accuracy of the course value in degrees. null if course accuracy data is not available on the device.
        /// </summary>
        public float? course_accuracy;

        /// <summary>
        /// Accuracy of the speed value in m/s. null if speed accuracy data is not available on the device.
        /// </summary>
        public float? speed_accuracy;
    }
}
