/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */


using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp.Demo
{
    public class BiometricManagerDemoController : BaseDemoController
    {
        public Text _infoText;

        public Button _refreshBtn;
        public Button _initBtn;
        public Button _requestAccessBtn;
        public Button _authenticateBtn;
        public Button _openSettingsBtn;

        private BiometricManager biometricManager;

        private void Start()
        {
            RefreshInfo();
            _refreshBtn.onClick.AddListener(RefreshInfo);
            _initBtn.onClick.AddListener(() => biometricManager.Init());
            _requestAccessBtn.onClick.AddListener(() =>
            {
                if (!biometricManager.isInited)
                {
                    TelegramWebApp.ShowAlert("BiometricManager did not inited");
                    return;
                }
                biometricManager.RequestAccess(
                    new BiometricRequestAccessParams { reason = "Request Test" }
                );
            });
            _authenticateBtn.onClick.AddListener(() =>
            {
                if (!biometricManager.isInited)
                {
                    TelegramWebApp.ShowAlert("BiometricManager did not inited");
                    return;
                }
                if (!biometricManager.isAccessGranted)
                {
                    TelegramWebApp.ShowAlert("BiometricManager isAccessGranted = false");
                    return;
                }
                biometricManager.Authenticate(
                    new BiometricAuthenticateParams { reason = "Authenticate Test" }
                );
            });
            _openSettingsBtn.onClick.AddListener(() => biometricManager.OpenSettings());
            TelegramWebAppEvents.OnBiometricManagerUpdated += RefreshInfo;
        }

        public void RefreshInfo()
        {
            biometricManager = TelegramWebApp.BiometricManager;
            _infoText.text = JsonUtility.ToJson(biometricManager);
        }
    }
}
