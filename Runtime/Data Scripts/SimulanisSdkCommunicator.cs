using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Simulanis.ContentSDK;
namespace K12.UI
{
    public class SimulanisSdkCommunicator : MonoBehaviour
    {
        public UnityEvent StartTaskUpdate_Event;
        public UnityEvent EndTaskUpdate_Event;
        public ModuleManager moduleManager_Script;

        private int TaskId = 1;

        #region ModuleManager Controls
        void UpdateStartTask()
        {
            TaskId = DataManager.StaticVariables.STEP_COUNT;
            if (TaskId < 1)
            {
                TaskId = 1;
            }
            Invoke(nameof(callInDelay), 0.2f);
        }
        void callInDelay()
        {
            moduleManager_Script.StartTask(TaskId.ToString());

        }
        void UpdateEndTask()
        {
            int taskId = DataManager.StaticVariables.STEP_COUNT - 1;
            if (taskId < 1)
            {
                taskId = 1;
            }
            moduleManager_Script.EndTask(taskId.ToString());
            //TaskId++;
        }
        #endregion

        #region Event Subscription Handler
        private void OnEnable() //subribed on SPAW event
        {
            EventManager.AddHandler(EVENTS.NEXT_STEP, UpdateEndTask);
            EventManager.AddHandler(EVENTS.STEP, UpdateStartTask);
        }
        private void OnDisable() //unsubribed from SPAW event
        {
            EventManager.RemoveHandler(EVENTS.NEXT_STEP, UpdateEndTask);
            EventManager.RemoveHandler(EVENTS.STEP, UpdateStartTask);
        }
        #endregion
    }

}
