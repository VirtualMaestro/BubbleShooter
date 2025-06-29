using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTeleApp
{
    public class BaseDemoController : MonoBehaviour
    {
        public Button _backBtn;
        public Button _nextBtn;

        protected void Awake()
        {
            if (_backBtn != null)
            {
                _backBtn.onClick.AddListener(() => Back());
            }
            if (_nextBtn != null)
            {
                _nextBtn.onClick.AddListener(() => Next());
            }
        }

        private void Back()
        {
            this.gameObject.SetActive(false);
            Transform t = this.transform.parent.GetChild(this.transform.GetSiblingIndex() - 1);
            if (t != null)
            {
                t.gameObject.SetActive(true);
            }
        }

        private void Next()
        {
            this.gameObject.SetActive(false);
            Transform t = this.transform.parent.GetChild(this.transform.GetSiblingIndex() + 1);
            if (t != null)
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
