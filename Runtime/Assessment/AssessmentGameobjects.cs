using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using K12.UI;
using UnityEngine.UI;
namespace K12.Assessment
{
    public class AssessmentGameobjects : MonoBehaviour
    {
        public GameObject AssessmentPanel
            ,AssessmentUICanvas
            ,ObjectForAssessment
            ,BoundingBoxes
            ,LandingMenu
            ,AssessmentBoard
            ,HomePanel;
        public AssessmentManager assessmentManager;
        public VoiceoverControllerScript audioMute;
        public UIAutomationController UIController;
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
            mute.onClick.AddListener(() => MuteUnMute(mute));
            home.onClick.AddListener(() => EnableHome());
            
        }

        public void EnableAssessment()
        {
            AssessmentPanel.gameObject.SetActive(true);
            AssessmentUICanvas.gameObject.SetActive(true);
            ObjectForAssessment.gameObject.SetActive(true);
            AssessmentBoard.gameObject.SetActive(true);
            BoundingBoxes.gameObject.SetActive(true);
            LandingMenu.gameObject.SetActive(false);
            assessmentManager.InitializeAssessment();
        }
        public void DisableAssessment()
        {
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
