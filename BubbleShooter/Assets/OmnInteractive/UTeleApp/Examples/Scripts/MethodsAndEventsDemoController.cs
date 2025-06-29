/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;


namespace UTeleApp.Demo
{

    public class MethodsAndEventsDemoController : BaseDemoController
    {
        // Basic Functions
        [Header("Basic")]
        public Button _readyBtn;
        public Button _expandBtn;
        public Button _closeBtn;
        public Button _isVersionAtLeastBtn;
        public Text _isVersionAtLeastText;
        public Button _enableClosingConfirmationBtn;
        public Button _disableClosingConfirmationBtn;
        public Button _enableVerticalSwipesBtn;
        public Button _disableVerticalSwipesBtn;
        public Button _showConfirmBtn;
        public Button _showAlertBtn;
        // Buttons
        [Header("Buttons")]
        public Button _showMainBtn;
        public Button _hideMainBtn;
        public Button _setMainBtnTextBtn;
        public Button _showBackBtn;
        public Button _hideBackBtn;
        public Button _showSecondaryBtn;
        public Button _hideSecondaryBtn;
        public Button _setSecondaryBtnTextBtn;
        public Button _showSettingsBtn;
        public Button _hideSettingsBtn;

        // Links
        [Header("Links")]
        public Button _openLinkBtn;
        public Button _openTelegramLinkBtn;
        public Button _openInvoiceLinkBtn;
        // 
        [Header("Mics")]
        public Button _shareToStoryBtn;
        public Button _showPopupBtn;
        public Button _showScanQrPopupBtn;
        public Button _readTextFromClipboardBtn;
        public Button _requestWriteAccessBtn;
        public Button _requestContactBtn;

        // Start is called before the first frame update
        void Start()
        {
            _readyBtn.onClick.AddListener(TelegramWebApp.Ready);
            _expandBtn.onClick.AddListener(TelegramWebApp.Expand);
            _closeBtn.onClick.AddListener(TelegramWebApp.Close);
            _isVersionAtLeastBtn.onClick.AddListener(()=> {
                bool result = TelegramWebApp.IsVersionAtLeast(TelegramWebApp.Version);
                _isVersionAtLeastText.text = result.ToString();
            });
            _enableClosingConfirmationBtn.onClick.AddListener(TelegramWebApp.EnableClosingConfirmation);
            _disableClosingConfirmationBtn.onClick.AddListener(TelegramWebApp.DisableClosingConfirmation);
            _enableVerticalSwipesBtn.onClick.AddListener(TelegramWebApp.EnableVerticalSwipes);
            _disableVerticalSwipesBtn.onClick.AddListener(TelegramWebApp.DisableVerticalSwipes);

            _showConfirmBtn.onClick.AddListener(() => TelegramWebApp.ShowConfirm("Confirm"));
            _showAlertBtn.onClick.AddListener(() => TelegramWebApp.ShowAlert("Alert"));

            _showMainBtn.onClick.AddListener(()=> TelegramWebApp.MainButton.Show());
            _hideMainBtn.onClick.AddListener(()=>TelegramWebApp.MainButton.Hide());
            _setMainBtnTextBtn.onClick.AddListener(() => TelegramWebApp.MainButton.SetText("MainBtn[Unity]"));
            TelegramWebAppEvents.OnMainButtonClicked += () => { TelegramWebApp.ShowConfirm("Clicked Main Btn"); };

            _showBackBtn.onClick.AddListener(() => TelegramWebApp.BackButton.Show());
            _hideBackBtn.onClick.AddListener(() => TelegramWebApp.BackButton.Hide());
            TelegramWebAppEvents.OnBackButtonClicked += () => { TelegramWebApp.ShowConfirm("Clicked Back Btn"); };

            _showSecondaryBtn.onClick.AddListener(() => TelegramWebApp.SecondaryButton.Show());
            _hideSecondaryBtn.onClick.AddListener(() => TelegramWebApp.SecondaryButton.Hide());
            _setSecondaryBtnTextBtn.onClick.AddListener(() => TelegramWebApp.SecondaryButton.SetText("SecBtn[Unity]"));
            TelegramWebAppEvents.OnSecondaryButtonClicked += () => { TelegramWebApp.ShowConfirm("Clicked Sec Btn"); };

            _showSettingsBtn.onClick.AddListener(() => TelegramWebApp.SettingsButton.Show());
            _hideSettingsBtn.onClick.AddListener(() => TelegramWebApp.SettingsButton.Hide());
            TelegramWebAppEvents.OnSettingsButtonClicked += () => { TelegramWebApp.ShowConfirm("Clicked Settings Btn"); };

            _openLinkBtn.onClick.AddListener(() => TelegramWebApp.OpenLink("https://omninteractive.net/"));
            _openTelegramLinkBtn.onClick.AddListener(() => TelegramWebApp.OpenTelegramLink("https://t.me/tapps_bot"));

            _shareToStoryBtn.onClick.AddListener(() => TelegramWebApp.ShareToStory("https://omninteractive.net/"));
            _showPopupBtn.onClick.AddListener(() => TelegramWebApp.ShowPopup(new PopupParams() { title = "Show PopUp[Unity]", message = "Test Message" }));
            _showScanQrPopupBtn.onClick.AddListener(() => TelegramWebApp.ShowScanQrPopup(new ScanQrPopupParams() { text = "Scan QR Code[Unity]" }));
            TelegramWebAppEvents.OnQrTextReceived += ((text) => TelegramWebApp.ShowConfirm("OnQrTextReceived :" + text));
            TelegramWebAppEvents.OnScanQrPopupClosed += (() => TelegramWebApp.ShowConfirm("OnScanQrPopupClosed"));

            _readTextFromClipboardBtn.onClick.AddListener(() => TelegramWebApp.ReadTextFromClipboard());
            TelegramWebAppEvents.OnClipboardTextReceived += ((text) => TelegramWebApp.ShowConfirm("OnClipboardTextReceived :" + text));
            _requestWriteAccessBtn.onClick.AddListener(() => TelegramWebApp.RequestWriteAccess());
            _requestContactBtn.onClick.AddListener(() => TelegramWebApp.RequestContact());
        }

    }
}
