/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;

namespace UTeleApp
{
    /// <summary>
    /// This object controls the bottom button, which can be displayed at the bottom of the Mini App in the Telegram interface.
    /// </summary>
    [System.Serializable]
    public class BottomButton
    {
        /// <summary>
        /// Current button text.
        /// </summary>
        [SerializeField]
        public string text;

        /// <summary>
        /// Current button color.
        /// </summary>
        [SerializeField]
        public string color;

        /// <summary>
        /// Current button text color.
        /// </summary>
        [SerializeField]
        public string textColor;

        /// <summary>
        /// Shows whether the button is visible. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool isVisible;

        /// <summary>
        /// Shows whether the button is active. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool isActive;

        /// <summary>
        /// Shows whether the button is currently loading. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool isProgressVisible;

        /// <summary>
        /// Shows whether the button has a shine effect. Set to false by default.
        /// </summary>
        [SerializeField]
        public bool hasShineEffect;

        /// <summary>
        /// Position of the secondary button.
        /// </summary>
        [SerializeField]
        public string position;

        /// <summary>
        /// Type of the button.
        /// </summary>
        [SerializeField]
        public string type;

        private string GetClassName()
        {
            return !string.IsNullOrEmpty(type) && type.ToLower() == "secondary" ? "SecondaryButton" : "MainButton";
        }

        /// <summary>
        /// A method to set the button text.
        /// </summary>
        /// <param name="text">Button text.</param>
        public BottomButton SetText(string text)
        {
            this.text = text;
            TelegramWebApp.InvokeMethodWithParam($"{GetClassName()}.setText", text);
            return this;
        }

        /// <summary>
        /// A method to set the button color.
        /// </summary>
        /// <param name="color">Button color in #RRGGBB format.</param>
        public BottomButton SetColor(string color)
        {
            this.color = color;
            TelegramWebApp.InvokeMethodWithParam($"{GetClassName()}.setColor", color);
            return this;
        }

        /// <summary>
        /// A method to set the button text color.
        /// </summary>
        /// <param name="textColor">Button text color in #RRGGBB format.</param>
        public BottomButton SetTextColor(string textColor)
        {
            this.textColor = textColor;
            TelegramWebApp.InvokeMethodWithParam($"{GetClassName()}.setTextColor", textColor);
            return this;
        }

        /// <summary>
        /// A method to show the button.
        /// </summary>
        public BottomButton Show()
        {
            isVisible = true;
            Debug.Log($"{GetClassName()}");
            TelegramWebApp.InvokeMethod($"{GetClassName()}.show");
            return this;
        }

        /// <summary>
        /// A method to hide the button.
        /// </summary>
        public BottomButton Hide()
        {
            isVisible = false;
            TelegramWebApp.InvokeMethod($"{GetClassName()}.hide");
            return this;
        }

        /// <summary>
        /// A method to enable the button.
        /// </summary>
        public BottomButton Enable()
        {
            isActive = true;
            TelegramWebApp.InvokeMethod($"{GetClassName()}.enable");
            return this;
        }

        /// <summary>
        /// A method to disable the button.
        /// </summary>
        public BottomButton Disable()
        {
            isActive = false;
            TelegramWebApp.InvokeMethod($"{GetClassName()}.disable");
            return this;
        }

        /// <summary>
        /// A method to show the loading indicator on the button.
        /// It is recommended to display loading progress if the action tied to the button may take a long time.
        /// By default, the button is disabled while the action is in progress.
        /// If the parameter leaveActive=true is passed, the button remains enabled.
        /// </summary>
        public BottomButton ShowProgress(bool leaveActive = false)
        {
            isProgressVisible = true;
            TelegramWebApp.InvokeMethodWithBoolParam($"{GetClassName()}.showProgress", leaveActive);
            return this;
        }

        /// <summary>
        /// A method to hide the loading indicator on the button.
        /// </summary>
        public BottomButton HideProgress()
        {
            isProgressVisible = false;
            TelegramWebApp.InvokeMethod($"{GetClassName()}.hideProgress");
            return this;
        }

        /// <summary>
        /// A method to set the button parameters.
        /// </summary>
        /// <param name="parameters">Button parameters.</param>
        public BottomButton SetParams(BottomButtonParams parameters)
        {
            TelegramWebApp.InvokeMethodWithJsonStringParam(
                $"{GetClassName()}.setParams",
                JsonUtility.ToJson(parameters)
            );
            return this;
        }
    }

    /// <summary>
    /// Parameters for configuring a bottom button.
    /// </summary>
    [System.Serializable]
    public struct BottomButtonParams
    {
        /// <summary>
        /// Optional. Button text.
        /// </summary>
        public string text;

        /// <summary>
        /// Optional. Button color in #RRGGBB format.
        /// </summary>
        public string color;

        /// <summary>
        /// Optional. Button text color in #RRGGBB format.
        /// </summary>
        public string text_color;

        /// <summary>
        /// Optional. Enable the button.
        /// </summary>
        public bool? is_active;

        /// <summary>
        /// Optional. Show the button.
        /// </summary>
        public bool? is_visible;

        /// <summary>
        /// Optional. Enable shine effect.
        /// </summary>
        public bool? has_shine_effect;

        /// <summary>
        /// Optional. Position of the secondary button.
        /// Supported values:
        /// - 'left', displayed to the left of the main button
        /// - 'right', displayed to the right of the main button
        /// - 'top', displayed above the main button
        /// - 'bottom', displayed below the main button
        /// </summary>
        public string position;
    }
}
