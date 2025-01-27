using Simulanis.ContentSDK.K12.Assessment;
using Simulanis.ContentSDK.K12.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Simulanis.ContentSDK.K12.UI
{
    public class HomeManager : MonoBehaviour
    {
        public List<Button> homeButtons;
        public List<Button> backButtons;
        public GameObject HomePanel;
        public UIAutomationController UIAutomationController;

        private void Awake()
        {
            if (UIAutomationController is null)
            {
                UIAutomationController = FindObjectOfType<UIAutomationController>();
            }
            Initialize();
        }


        private void Initialize()
        {
            foreach (Button btn in homeButtons)
            {
                btn.onClick.AddListener(() => EnableHome());
            }

            foreach (Button btn in backButtons)
            {
                btn.onClick.AddListener(() => Back());
            }
        }

        public void EnableHome()
        {
            HomePanel.SetActive(true);
            UIAutomationController.Pause();
        }

        public void Back()
        {
            HomePanel.SetActive(false);
            UIAutomationController.UnPause();

        }
    }
}


