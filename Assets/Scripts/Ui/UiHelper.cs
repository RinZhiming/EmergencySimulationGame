using System;
using UnityEngine;

namespace Ui
{
    public class UiHelper : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
