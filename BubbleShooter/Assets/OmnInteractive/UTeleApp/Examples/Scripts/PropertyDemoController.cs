/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp.Demo
{
    public class PropertyDemoController : BaseDemoController
    {
        public Text _initDataText;
        public Text _initDataUnsafeText;
        public Text _versionText;
        public Text _platformText;
        public Text _themeParamsText;
        public Text _colorSchemeText;
        public Text _headerColorText;
        public Text _backgroundColorText;
        public Text _viewportHeightText;
        public Text _viewportStableHeightText;
        public Text _isExpandedText;
        public Text _isActiveText;
        public Text _isClosingConfirmationEnabledText;
        public Text _isVerticalSwipesEnabledText;

        // Start is called before the first frame update
        void Start()
        {
            _initDataText.text = TelegramWebApp.InitData;
            _initDataUnsafeText.text = JsonUtility.ToJson(TelegramWebApp.InitDataUnsafe);
            _versionText.text = TelegramWebApp.Version;
            _platformText.text = TelegramWebApp.Platform;
            _themeParamsText.text = JsonUtility.ToJson(TelegramWebApp.ThemeParams);
            _colorSchemeText.text = TelegramWebApp.ColorScheme;
            _headerColorText.text = TelegramWebApp.HeaderColor;
            _backgroundColorText.text = TelegramWebApp.BackgroundColor;
            _viewportHeightText.text = TelegramWebApp.ViewportHeight.ToString();
            _viewportStableHeightText.text = TelegramWebApp.ViewportStableHeight.ToString();
            _isExpandedText.text = TelegramWebApp.IsExpanded.ToString();
            _isActiveText.text = TelegramWebApp.IsActive.ToString();
            _isClosingConfirmationEnabledText.text =
                TelegramWebApp.IsClosingConfirmationEnabled.ToString();
            _isVerticalSwipesEnabledText.text = TelegramWebApp.IsVerticalSwipesEnabled.ToString();
        }
    }
}
