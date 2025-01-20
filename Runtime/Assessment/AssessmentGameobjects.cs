using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K12.Assessment
{
    public class AssessmentGameobjects : MonoBehaviour
    {
        public GameObject AssessmentPanel
            ,AssessmentUICanvas
            ,ObjectForAssessment
            ,BoundingBoxes
            ,LandingMenu
            ,AssessmentBoard;
        public AssessmentManager assessmentManager;


        private void Awake()
        {
            if(assessmentManager is null)
            {
                assessmentManager = FindObjectOfType<AssessmentManager>();
            }
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
    }

}
