using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulanis.ContentSDK.K12.UI;
using UnityEngine.UI;
using TMPro;
namespace Simulanis.ContentSDK.K12.Assessment
{
    public class AssessmentGameobjects : MonoBehaviour
    {
        public GameObject AssessmentPanel
            ,AssessmentUICanvas
            ,ObjectForAssessment
            ,BoundingBoxes
            ,LandingMenu
            ,AssessmentBoard
            ,HomePanel
            ,userGuide;
        public TMP_Text Heading;
        public AssessmentManager assessmentManager;
        public VoiceoverControllerScript audioMute;
        public UIAutomationController UIController;
        public AssessmentTimer Timer;
        public Button mute, home;
        private void Awake()
        {
            if(assessmentManager is null)
            {
                assessmentManager = FindObjectOfType<AssessmentManager>();
            }
            if (audioMute is null)
            {
                audioMute = FindObjectOfType<VoiceoverControllerScript>();
            }
            if (UIController is null)
            {
                UIController = FindObjectOfType<UIAutomationController>();
            }
            if (Timer is null)
            {
                Timer = FindObjectOfType<AssessmentTimer>();
            }
            mute.onClick.AddListener(() => MuteUnMute(mute));
            home.onClick.AddListener(() => EnableHome());
            Timer.ResetTimer();
        }

        public void EnableUserGuide()
        {
            userGuide.SetActive(true);
            LandingMenu.gameObject.SetActive(false);
        }
        public void EnableAssessment()
        {
            Timer.StartTimer();
            AssessmentPanel.gameObject.SetActive(true);
            AssessmentUICanvas.gameObject.SetActive(true);
            ObjectForAssessment.gameObject.SetActive(true);
            AssessmentBoard.gameObject.SetActive(true);
            BoundingBoxes.gameObject.SetActive(true);
            assessmentManager.InitializeAssessment();
            userGuide.SetActive(false);
            Heading.text = DataManager.StaticVariables.COLUMN_02;
        }
        public void DisableAssessment()
        {
            Timer.StopTimer();
            AssessmentPanel.gameObject.SetActive(false);
            AssessmentUICanvas.gameObject.SetActive(false);
            ObjectForAssessment.gameObject.SetActive(false);
            BoundingBoxes.gameObject.SetActive(false);
            LandingMenu.gameObject.SetActive(true);
            AssessmentBoard.gameObject.SetActive(false);

        }


        public void MuteUnMute(Button button)
        {
            audioMute.MuteButtonClickHandler(button);
        }
        public void EnableHome()
        {
            HomePanel.SetActive(true);
            UIController.Pause();
        }
        public void DisableHome()
        {
            HomePanel.SetActive(false);
            UIController.UnPause();

        }

    }

}
