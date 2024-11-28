using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Core.Game.InspectorCamera
{
    public class InspectorCameraManager : MonoBehaviour
    {
        private Camera currentCamera;
        private CinemachineVirtualCameraBase currentVirtualCamera;
        private Vector3 mousePosition;
        
        private void Awake()
        {
            InspectorCameraEvents.OnEnter += OnInspectorCameraEnter;
            InspectorCameraEvents.OnExit += OnInspectorCameraExit;
        }

        private void OnDestroy()
        {
            InspectorCameraEvents.OnEnter -= OnInspectorCameraEnter;
            InspectorCameraEvents.OnExit -= OnInspectorCameraExit;
        }

        private void Update()
        {
            if (!currentCamera || !currentVirtualCamera) return;
            
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            mousePosition = GetMousePosition();
            
            
        }

        private void OnInspectorCameraEnter(Camera cam, CinemachineVirtualCameraBase cinemachineVirtualCameraBase)
        {
            if (cam == currentCamera || currentVirtualCamera == cinemachineVirtualCameraBase) return;
            
            currentCamera = cam;
            currentVirtualCamera = cinemachineVirtualCameraBase;
            
            currentCamera.gameObject.SetActive(true);
            currentVirtualCamera.Priority = 1;
        }

        private void OnInspectorCameraExit(Camera cam, CinemachineVirtualCameraBase cinemachineVirtualCameraBase)
        {
            if(currentCamera != cam || currentVirtualCamera != cinemachineVirtualCameraBase) return;

            currentCamera.gameObject.SetActive(false);
            currentVirtualCamera.Priority = -1;
            
            currentCamera = null;
            currentVirtualCamera = null;
        }

        private Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}