using System;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField]
        [Tooltip("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        private Transform target;
        
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private Vector3 offset;
        [SerializeField] private Vector2 xClamp;
        [SerializeField] [Range(0, 20)] private float rotationSpeed;
        [SerializeField] private float mouseSensitivity;
        
        [SerializeField]
        [Tooltip("The lower the value the faster the camera moves")] 
        private float travelTime;

        private Vector2 input;
        private Vector3 velocity;
        
        private InputMaster inputMaster;


        private void Awake() {
            inputMaster = new InputMaster();
            inputMaster.Enable();
        }
        
        private void LateUpdate() {
            GetInput();
            RotateCamera();
            MoveCamera();
            
            CheckForTerrainHeight();
            
        }
        
        private void GetInput() {

            input.x -= inputMaster.Player.MoveCamera.ReadValue<Vector2>().y * mouseSensitivity;
            input.y += inputMaster.Player.MoveCamera.ReadValue<Vector2>().x * mouseSensitivity;

            input.x = Mathf.Clamp(input.x, xClamp.x, xClamp.y);
        }
        
        private void RotateCamera() {
            target.localRotation = Quaternion.Lerp(target.localRotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
            transform.rotation = target.localRotation;
        }

        private void MoveCamera() {

            Vector3 collisionOffset = transform.rotation * offset;
            
            transform.position = Vector3.SmoothDamp(transform.position, target.position + collisionOffset, ref velocity, travelTime, 100, Time.deltaTime);

        }

        private void CheckForTerrainHeight() {
            Physics.Raycast(target.position, target.forward + new Vector3(0, -1 ,0), out var hit, 10,  layerMask);
            
            Debug.DrawRay(target.position, target.forward + new Vector3(transform.forward.x, transform.forward.y -.5f, transform.forward.z), Color.red);
        }

        private void OnApplicationFocus(bool hasFocus) {
            Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
    
    
    
}

