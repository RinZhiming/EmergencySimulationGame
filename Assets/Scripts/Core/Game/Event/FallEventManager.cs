using System;
using Core.Cutscene;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Game.Event
{
    public class FallEventManager : MonoBehaviour
    {
        [SerializeField] private EventDataBase eventData;
        [SerializeField] private PlayableDirector cutscene;
        
        private void Awake()
        {
            TriggerObject.onEventRaised += OnEventRaised;
        }

        private void OnDestroy()
        {
            TriggerObject.onEventRaised -= OnEventRaised;
        }
        
        private void OnEventRaised(EventDataBase eventObj)
        {
            if (eventObj.EventName == eventData.EventName)
            {
                CutsceneManager.PlayCutscene(cutscene, false);
            }
        }
    }
}