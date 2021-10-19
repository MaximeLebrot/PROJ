using System;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask collisionMask;

        [SerializeField] private Vector3 offset;
        [SerializeField] [Range(0, 20)] private float followSpeed;
        [SerializeField] [Range(0, 20)] private float rotationSpeed;
        [SerializeField] private float mouseSensitivity;
        
        private Vector2 input;

        private InputMaster inputMaster;

        private void Awake() {
            inputMaster = new InputMaster();
            inputMaster.Enable();
        }
        
        private void LateUpdate() {
            GetInput();
            RotateCamera();
            MoveCamera();
        }
        
        private void GetInput() {

            input.x -= inputMaster.Player.MoveCamera.ReadValue<Vector2>().y * mouseSensitivity;
            input.y += inputMaster.Player.MoveCamera.ReadValue<Vector2>().x * mouseSensitivity;

            input.x = Mathf.Clamp(input.x, -40, 60);
        }
        
        private void RotateCamera() {
            target.localRotation = Quaternion.Lerp(target.localRotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
            transform.rotation = target.localRotation;
        }

        private void MoveCamera() {

            Vector3 collisionOffset = transform.rotation * offset;
            
            transform.position = Vector3.Slerp(transform.position, target.position + collisionOffset, followSpeed * Time.deltaTime);    

        }

        private void OnApplicationFocus(bool hasFocus) {
            Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
    
    
    
}

