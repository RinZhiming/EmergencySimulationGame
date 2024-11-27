using System;
using UnityEngine;

namespace Core.Game.Medical
{
    public abstract class MedicalCaseBase : MonoBehaviour
    {
        private void Start()
        {
            OnBegin();
        }

        protected abstract void OnBegin();
        protected abstract void OnMedical();
        protected abstract void OnSuccess();
    }
}
