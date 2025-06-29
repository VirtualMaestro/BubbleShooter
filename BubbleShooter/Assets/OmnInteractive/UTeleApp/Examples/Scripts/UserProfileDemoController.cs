/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace UTeleApp.Demo
{
    public class UserProfileDemoController : MonoBehaviour
    {
        [SerializeField]
        private Image _profileImage;
        [SerializeField]
        private Text _firstNameText;
        [SerializeField]
        private Text _lastNameText;
        [SerializeField]
        private Text _usernameText;
        [SerializeField]
        private Text _languageCodeText;
        [SerializeField]
        private Text _telegramIdText;

        void Start()
        {

        }

        private void OnEnable()
        {
            // Get the WebAppInitData from UTeleApp (This is unsafe data, you should verify your hash with server telegarm bot app) 
            WebAppInitData webAppData = TelegramWebApp.InitDataUnsafe;
            if (webAppData.user.Equals(default(WebAppUser)))
                return;
            UpdateUserProfile(webAppData.user);
        }

        private void UpdateUserProfile(WebAppUser user)
        {
            // Update name fields
            if (_firstNameText != null)
                _firstNameText.text = user.first_name;
            
            if (_lastNameText != null)
                _lastNameText.text = user.last_name;
            
            if (_usernameText != null)
                _usernameText.text = $"@{user.username}";

            if (_languageCodeText != null)
                _languageCodeText.text = string.IsNullOrEmpty(user.language_code) ? 
                    "No language" : user.language_code.ToUpper();

            // Update Telegram ID
            if (_telegramIdText != null)
                _telegramIdText.text = $"ID: {user.id}";

            // Load profile image if available
            if (_profileImage != null && !string.IsNullOrEmpty(user.photo_url) && _profileImage.sprite == null)
            {
                StartCoroutine(LoadProfileImage(user.photo_url, _profileImage));
            }
        }

        public IEnumerator LoadProfileImage(string photoUrl, Image image)
        {
            // Use a CORS proxy service
            string proxyUrl = $"https://api.allorigins.win/raw?url={Uri.EscapeDataString(photoUrl)}";
            
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(proxyUrl))
            {
                // Set download handler before sending
                request.downloadHandler = new DownloadHandlerTexture();
                
                // Set request to ignore certificate issues (if any)
                request.certificateHandler = new BypassCertificate();

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    if (texture != null)
                    {
                        image.sprite = Sprite.Create(texture, 
                            new Rect(0, 0, texture.width, texture.height), 
                            Vector2.one * 0.5f);
                    }
                }
                else
                {
                    Debug.LogWarning($"Failed to load profile image: {request.error}");
                }
            }
        }

        // Add this helper class to bypass certificate validation
        public class BypassCertificate : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                return true;
            }
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}
