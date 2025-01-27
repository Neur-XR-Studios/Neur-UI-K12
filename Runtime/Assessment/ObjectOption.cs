using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simulanis.ContentSDK.K12.Assessment
{
    public class ObjectOption : MonoBehaviour
    {
        public int OptionSelected = 1;
        public AssessmentManager AssessmentManager;
        public SlingshotGame SlingshotGame;
        public bool isClicked = false;

        private void OnMouseDown()
        {
            if (!isClicked)
            {
                isClicked = true;
                SelecteAns();
            }
        }
        private void OnMouseUp()
        {
            if (isClicked)
            {
                isClicked = false;
            }
        }
        public void SelecteAns()
        {
            if (!SlingshotGame.isPulling && !SlingshotGame.isProjectileMoving)
            {
                AssessmentManager.AnswerSelectionHandler(OptionSelected);
                SlingshotGame.StartPull(OptionSelected);
                Debug.Log(OptionSelected + "Option");
            }

        }
    }
}
