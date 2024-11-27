using System;
using UnityEngine;

namespace Core.Game
{
    public class EventDataBase : ScriptableObject, IEquatable<EventDataBase>
    {
        [SerializeField] private string eventName;

        public string EventName => eventName;

        public bool Equals(EventDataBase other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && eventName == other.eventName;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EventDataBase)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), eventName);
        }
    }
}