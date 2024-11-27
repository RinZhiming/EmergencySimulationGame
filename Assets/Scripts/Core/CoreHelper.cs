using UnityEngine;

namespace Core
{
    public class CoreHelper : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
