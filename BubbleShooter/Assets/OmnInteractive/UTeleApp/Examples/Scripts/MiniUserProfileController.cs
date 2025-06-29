/*
 * Copyright (c) 2024 OmnInteractive Solutions. All rights reserved.
 */

using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp.Demo
{
    public class MiniUserProfileController : MonoBehaviour
    {
        public UserProfileDemoController controller;
        [SerializeField]
        private Image _profileImage;
        [SerializeField]
        private Text _nameText;

        public void Start()
        {
            // Load profile image if available
            WebAppInitData webAppData = TelegramWebApp.InitDataUnsafe;
            WebAppUser user = webAppData.user;
            if (_profileImage != null)
            {
                _nameText.text = user.username ?? user.first_name;
                _profileImage.GetComponent<Button>().onClick.AddListener(() =>
                {
                    controller.Show();
                });

                if(!string.IsNullOrEmpty(user.photo_url))
                {
                    StartCoroutine(controller.LoadProfileImage(user.photo_url, _profileImage));
                }
            }
        }
    }
}
