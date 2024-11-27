using UnityEngine;

namespace Utilities
{
    public static class CanvasGroupExtension
    {
        public static void SetActive(this CanvasGroup canvasGroup, bool active)
        {
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
            canvasGroup.alpha = active ? 1 : 0;
        }
    }
}
