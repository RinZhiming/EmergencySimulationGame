using UnityEngine;

namespace Utilities
{
    public static class TransformExtension
    {
        public static void ClearChild(this Transform transform)
        {
            if (!transform) return;
            if (transform.childCount <= 0) return;

            for (var i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i)) continue;
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}