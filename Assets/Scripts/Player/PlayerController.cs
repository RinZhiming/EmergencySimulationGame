using System;
using Core.Cutscene;
using GameSystem;
using UnityEngine;

namespace Player
{
    public class PlayerController : CoreBehavior
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera cameraPlayer;
        [SerializeField] private float moveSpeed, runSpeed, rotateSpeed, smoothDampX, smoothDampY;
        private float horizontal, vertical, mouseX, mouseY, rotX, rotY,speed;
        private Vector3 movementDirection, playerVelocity;
        private bool isActive;

        private void Awake()
        {
            CutsceneManager.onCutsceneChanged += SetPlayerState;
        }

        private void OnDestroy()
        {
            CutsceneManager.onCutsceneChanged -= SetPlayerState;
        }

        private void SetPlayerState(bool pause)
        {
            Cursor.lockState = !pause ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !pause;
            cameraPlayer.gameObject.SetActive(!pause);
            isActive = !pause;
        }

        private void Update()
        {
            if (!isActive) return;
            
            Movement();
            Rotate();
            Gravity();
        }

        private void Movement()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            speed = Input.GetKey(KeyCode.LeftShift) && vertical > 0 ? runSpeed : moveSpeed;
            
            movementDirection = cameraPlayer.transform.forward * vertical + cameraPlayer.transform.right * horizontal;
            movementDirection.y = 0;
            movementDirection = movementDirection.normalized;
            characterController.Move(movementDirection * speed * Time.deltaTime);
        }

        private void Rotate()
        {
            mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            rotX -= mouseY;
            rotY += mouseX;
            rotX = Mathf.Clamp(rotX, -90f, 90f);
            var targetRotateCamera = Quaternion.Euler(rotX, rotY, 0);
            var targetRotatePlayer = Quaternion.Euler(0, rotY, 0);
            cameraPlayer.transform.localRotation = Quaternion.Slerp(
                cameraPlayer.transform.localRotation,
                targetRotateCamera, 
                smoothDampY * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                targetRotatePlayer, 
                smoothDampX * Time.deltaTime);
        }

        private void Gravity()
        {
            if (characterController.isGrounded)
            {
                playerVelocity -= Vector3.Project(playerVelocity, Physics.gravity.normalized);
            }
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            
            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }
}
