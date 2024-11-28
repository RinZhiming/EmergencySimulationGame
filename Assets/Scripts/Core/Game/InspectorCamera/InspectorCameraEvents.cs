using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Core.Game.InspectorCamera
{
    public static class InspectorCameraEvents
    {
        public static Action<Camera, CinemachineVirtualCameraBase> OnEnter { get; set; }
        public static Action<Camera, CinemachineVirtualCameraBase> OnExit { get; set; }
    }
}