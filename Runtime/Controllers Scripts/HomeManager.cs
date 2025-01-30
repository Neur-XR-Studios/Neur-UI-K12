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

        public List<Button> UnPauseNoButton;
        public List<Button> PauseHomeButton;
        public List<Button> HomeYesButton;
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

            foreach (Button btn in UnPauseNoButton)
            {
                btn.onClick.AddListener(() => UnPauseNo());
            }
            foreach (Button btn in HomeYesButton)
            {
                btn.onClick.AddListener(() => ChangeScene());
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


        public void UnPauseNo()
        {
            UIAutomationController.UnPause();
        }
        public void ChangeScene()
        {
            UIAutomationController.ChangeScene();
        }
    }
}


