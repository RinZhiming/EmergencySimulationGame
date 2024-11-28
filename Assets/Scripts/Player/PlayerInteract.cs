using System;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float distance;
        [SerializeField] private Transform eyes;
        [SerializeField] private LayerMask interactLayer;
        [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

        private void FixedUpdate()
        {
            Interact();
        }

        private void Interact()
        {
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, distance, interactLayer))
            {
                if (hit.collider)
                {
                    PlayerInteractEvents.OnHit?.Invoke(hit.collider);
                    if (Input.GetKeyDown(interactKey))
                    {
                        PlayerInteractEvents.OnInteract?.Invoke(hit.collider);
                    }
                }
                else
                {
                }
            }
        }
    }
}