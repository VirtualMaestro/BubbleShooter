/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp.Demo
{
    public class ExtraSystemDemoController : BaseDemoController
    {
        [Header("Accelerometer")]
        public Text _acceInfoText;
        public Button _acceRefreshBtn;
        public Button _acceStartBtn;
        public Button _acceStopBtn;

        [Header("DeviceOrientation")]
        public Text _doInfoText;
        public Button _doRefreshBtn;
        public Button _doStartBtn;
        public Button _doStopBtn;

        [Header("Gyroscope")]
        public Text _gyInfoText;
        public Button _gyRefreshBtn;
        public Button _gyStartBtn;
        public Button _gyStopBtn;

        private Accelerometer accelerometer;
        private DeviceOrientation deviceOrientation;
        private Gyroscope gyroscope;

        private void Start()
        {
            RefreshAccelerometerInfo();
            _acceRefreshBtn.onClick.AddListener(RefreshAccelerometerInfo);
            _acceStartBtn.onClick.AddListener(() => accelerometer.Start(new AccelerometerStartParams()));
            _acceStopBtn.onClick.AddListener(() => accelerometer.Stop());
            TelegramWebAppEvents.OnAccelerometerChanged += RefreshAccelerometerInfo;

            RefreshDeviceOrientationInfo();
            _acceRefreshBtn.onClick.AddListener(RefreshDeviceOrientationInfo);
            _doStartBtn.onClick.AddListener(() => deviceOrientation.Start(new DeviceOrientationStartParams()));
            _doStopBtn.onClick.AddListener(() => deviceOrientation.Stop());
            TelegramWebAppEvents.OnDeviceOrientationChanged += RefreshDeviceOrientationInfo;

            RefreshGyroscopeInfo();
            _gyRefreshBtn.onClick.AddListener(RefreshGyroscopeInfo);
            _gyStartBtn.onClick.AddListener(() => gyroscope.Start(new GyroscopeStartParams()));
            _gyStopBtn.onClick.AddListener(() => gyroscope.Stop());
            TelegramWebAppEvents.OnGyroscopeChanged += RefreshGyroscopeInfo;
        }

        public void RefreshAccelerometerInfo()
        {
            accelerometer = TelegramWebApp.Accelerometer;
            _acceInfoText.text = JsonUtility.ToJson(accelerometer);
        }

        public void RefreshDeviceOrientationInfo()
        {
            deviceOrientation = TelegramWebApp.DeviceOrientation;
            _doInfoText.text = JsonUtility.ToJson(deviceOrientation);
        }
        public void RefreshGyroscopeInfo()
        {
            gyroscope = TelegramWebApp.Gyroscope;
            _gyInfoText.text = JsonUtility.ToJson(gyroscope);
        }

    }
}
