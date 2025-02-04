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
            ,userGuide
            ,CFinstance;
        public TMP_Text Heading;
        public AssessmentManager assessmentManager;
        public VoiceoverControllerScript audioMute;
        public UIAutomationController UIController;
        public AssessmentTimer Timer;
        public Button mute, home;
        public Slider POVSlider;
        public TMP_Text POV;
        public Camera MainCamera;
        public Transform AssessmentCamTrans;
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
            POVSlider.onValueChanged.AddListener(_ => ShowPOV());
            Timer.ResetTimer();
        }

        public void EnableUserGuide()
        {
            userGuide.SetActive(true);
            CFinstance.SetActive(true);
            this.gameObject.SetActive(true);
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

            MainCamera.transform.position = AssessmentCamTrans.position;
            MainCamera.transform.rotation = AssessmentCamTrans.rotation;

            MainCamera.fieldOfView = 50;
          //  Heading.text = DataManager.StaticVariables.COLUMN_02;
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
            EventManager.Broadcast(EVENTS.CORRECT_HINDI);

        }
        public void DisableHome()
        {
            HomePanel.SetActive(false);
            UIController.UnPause();

        }

        public void ShowPOV()
        {
            MainCamera.fieldOfView = POVSlider.value; // Directly set FOV to slider value
            POV.text = MainCamera.fieldOfView.ToString("F2"); // Format for better readability
        }
    }

}
