using System;
using System.Collections.Generic;
using UnityEngine;

namespace K12.UI
{
    public enum EVENTS
    {
        SPAWN,
        STEP,
        WELCOME,
        PROMPT,
        ENABLE_HIGHLIGHT,
        DISABLE_HIGHLIGHT,
        NEXT_STEP,
        UPDATE_UI,
        UPDATE_CF_UI,
        ENABLE_FINAL_UI,
        CHANGE_LANGUAGE,
        CORRECT_HINDI
    }
    public class EventManager : MonoBehaviour // Everything here controlled By Events
    {
        private static Dictionary<EVENTS, Action> eventTable = new();

        public static void AddHandler(EVENTS gameEvent, Action action)
        {
            if (!eventTable.ContainsKey(gameEvent)) eventTable[gameEvent] = action;
            else eventTable[gameEvent] += action;
        }

        public static void RemoveHandler(EVENTS gameEvent, Action action)
        {
            if (eventTable[gameEvent] != null)
                eventTable[gameEvent] -= action;
            if (eventTable[gameEvent] == null)
                eventTable.Remove(gameEvent);
        }

        public static void Broadcast(EVENTS gameEvent)
        {
            if (eventTable.ContainsKey(gameEvent) && eventTable[gameEvent] != null)
                eventTable[gameEvent]();
        }
    }

}
