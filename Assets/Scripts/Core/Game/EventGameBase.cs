using System;
using UnityEngine;

namespace Core.Game
{
    public abstract class EventGameBase<T> : MonoBehaviour where T : EventDataBase
    {
        [SerializeField] private T eventData;
        public static event Action<EventDataBase> onEventRaised;

        protected void CallAction()
        {
            onEventRaised?.Invoke(eventData);
        }
    }
}