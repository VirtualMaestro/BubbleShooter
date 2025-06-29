/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;


namespace UTeleApp.Demo
{

    public class ExtraMethodsAndEventsDemoController : BaseDemoController
    {
        [Header("Mics")]
        public Button _requestFullScreenBtn;
        public Button _exitFullscreenBtn;
        public Button _lockOrientationBtn;
        public Button _unlockOrientationBtn;
        public Button _addToHomeScreenBtn;
        public Button _checkHomeScreenStatusBtn;

        // Start is called before the first frame update
        void Start()
        {
            _requestFullScreenBtn.onClick.AddListener(()=> TelegramWebApp.RequestFullscreen());
            _exitFullscreenBtn.onClick.AddListener(() => TelegramWebApp.ExitFullscreen());
            _lockOrientationBtn.onClick.AddListener(() => TelegramWebApp.LockOrientation());
            _unlockOrientationBtn.onClick.AddListener(() => TelegramWebApp.UnlockOrientation());
            _addToHomeScreenBtn.onClick.AddListener(() => TelegramWebApp.AddToHomeScreen());
            _checkHomeScreenStatusBtn.onClick.AddListener(() => TelegramWebApp.CheckHomeScreenStatus());
        }

    }
}
