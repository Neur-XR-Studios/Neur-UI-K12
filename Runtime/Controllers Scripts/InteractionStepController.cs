using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
namespace Neur.UI
{
    public class InteractionStepController : MonoBehaviour
    {
        [Space(20)]
        [Header("-----------Add Each Step Initial Event------------")]
        [Space(10)]
        public List<UnityEvent> InteractionStepInitialEvents = new();

        private int StepCount = 1;

        void Start()
        {
            StepCount = 1;
        }
        void StartNextStep()
        {
            if (StepCount < InteractionStepInitialEvents.Count)
            {
                //InteractionStepInitialEvents[StepCount].Invoke();
                //StepCount++;
            }
        }

        #region Event Subscription Handler
        private void OnEnable() //subribed on SPAW event
        {
            EventManager.AddHandler(EVENTS.STEP, StartNextStep);
        }
        private void OnDisable() //unsubribed from SPAW event
        {
            EventManager.RemoveHandler(EVENTS.STEP, StartNextStep);
        }
        #endregion
    }

}
