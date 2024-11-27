using UnityEngine;

namespace Core.Game.Event
{
    public class TriggerObject : EventGameBase<TriggerData>
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CallAction();
                gameObject.SetActive(false);
            }
        }
    }
}
