using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp.Demo
{

    public class HapticFeedbackDemoController : BaseDemoController
    {
        public Button _impactOccurredLightBtn;
        public Button _impactOccurredMediumBtn;
        public Button _impactOccurredHeavyBtn;
        public Button _impactOccurredRigidBtn;
        public Button _impactOccurredSoftBtn;
        public Button _notificationOccurredErrorBtn;
        public Button _notificationOccurredSuccessBtn;
        public Button _notificationOccurredWarningBtn;
        public Button _selectionChangedBtn;

        // Start is called before the first frame update
        void Start()
        {
            HapticFeedback hapticFeedback = TelegramWebApp.HapticFeedback;
            _impactOccurredLightBtn.onClick.AddListener(() => hapticFeedback.ImpactOccurred(HapticFeedback.HapticStyles.Light));
            _impactOccurredMediumBtn.onClick.AddListener(() => hapticFeedback.ImpactOccurred(HapticFeedback.HapticStyles.Medium));
            _impactOccurredHeavyBtn.onClick.AddListener(() => hapticFeedback.ImpactOccurred(HapticFeedback.HapticStyles.Heavy));
            _impactOccurredRigidBtn.onClick.AddListener(() => hapticFeedback.ImpactOccurred(HapticFeedback.HapticStyles.Rigid));
            _impactOccurredSoftBtn.onClick.AddListener(() => hapticFeedback.ImpactOccurred(HapticFeedback.HapticStyles.Soft));
            _notificationOccurredErrorBtn.onClick.AddListener(() => hapticFeedback.NotificationOccurred(HapticFeedback.NotificationTypes.Error));
            _notificationOccurredSuccessBtn.onClick.AddListener(() => hapticFeedback.NotificationOccurred(HapticFeedback.NotificationTypes.Success));
            _notificationOccurredWarningBtn.onClick.AddListener(() => hapticFeedback.NotificationOccurred(HapticFeedback.NotificationTypes.Warning));
            _selectionChangedBtn.onClick.AddListener(() => hapticFeedback.SelectionChanged());
        }
    }
}