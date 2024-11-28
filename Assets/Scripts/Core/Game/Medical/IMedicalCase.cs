using System;
using UnityEngine;

namespace Core.Game.Medical
{
    public interface IMedicalCase
    {
        public void OnBegin();
        public void OnMedical();
        public void OnSuccess();
    }
}
