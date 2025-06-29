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
    /// Represents the parameters for theming the Mini App.
    /// Mini Apps can adjust the appearance of the interface to match the Telegram user's app in real time.
    /// This object contains the user's current theme settings.
    /// </summary>
    [Serializable]
    public struct ThemeParams
    {
        /// <summary>
        /// Optional. Background color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-bg-color).
        /// </summary>
        public string bg_color;

        /// <summary>
        /// Optional. Main text color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-text-color).
        /// </summary>
        public string text_color;

        /// <summary>
        /// Optional. Hint text color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-hint-color).
        /// </summary>
        public string hint_color;

        /// <summary>
        /// Optional. Link color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-link-color).
        /// </summary>
        public string link_color;

        /// <summary>
        /// Optional. Button color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-button-color).
        /// </summary>
        public string button_color;

        /// <summary>
        /// Optional. Button text color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-button-text-color).
        /// </summary>
        public string button_text_color;

        /// <summary>
        /// Optional. Bot API 6.1+ Secondary background color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-secondary-bg-color).
        /// </summary>
        public string secondary_bg_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Header background color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-header-bg-color).
        /// </summary>
        public string header_bg_color;

        /// <summary>
        /// Optional. Bot API 7.10+ Bottom background color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-bottom-bar-bg-color).
        /// </summary>
        public string bottom_bar_bg_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Accent text color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-accent-text-color).
        /// </summary>
        public string accent_text_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Background color for the section in the #RRGGBB format.
        /// It is recommended to use this in conjunction with secondary_bg_color.
        /// Also available as the CSS variable var(--tg-theme-section-bg-color).
        /// </summary>
        public string section_bg_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Header text color for the section in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-section-header-text-color).
        /// </summary>
        public string section_header_text_color;

        /// <summary>
        /// Optional. Bot API 7.6+ Section separator color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-section-separator-color).
        /// </summary>
        public string section_separator_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Subtitle text color in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-subtitle-text-color).
        /// </summary>
        public string subtitle_text_color;

        /// <summary>
        /// Optional. Bot API 7.0+ Text color for destructive actions in the #RRGGBB format.
        /// Also available as the CSS variable var(--tg-theme-destructive-text-color).
        /// </summary>
        public string destructive_text_color;
    }
}
