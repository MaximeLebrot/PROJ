using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField]
        [Tooltip("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        private Transform followTarget; 
        
        [SerializeField] private Transform eyeTarget;
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
        private Vector3 followTargetOriginalPosition;
        
        
        private InputMaster inputMaster;
        [SerializeField] [Range(0, 1)] private float minSlopeHeight;
        [SerializeField] [Range(0, 1)] private float maxSlopeHeight;
        [SerializeField] [Range(0, 10)] private float slopeAdjustmentSpeed;

        private void Awake() {
            inputMaster = new InputMaster();
            inputMaster.Enable();
            followTargetOriginalPosition = followTarget.localPosition;
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
            followTarget.rotation = Quaternion.Lerp(followTarget.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
            transform.rotation = followTarget.localRotation;
        }

        private void MoveCamera() {

            Vector3 collisionOffset = transform.rotation * offset;
            
            transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + collisionOffset, ref velocity, travelTime, 100, Time.deltaTime);
            
        }
        
        private void CheckForTerrainHeight() {
            Physics.Raycast(eyeTarget.position, eyeTarget.forward - eyeTarget.up, out var hit, 10,  layerMask);

            float dot = 1 - Vector3.Dot(eyeTarget.up, hit.normal);
            
            Vector3 targetHeight = dot <= maxSlopeHeight && dot >= minSlopeHeight ? Vector3.up : followTargetOriginalPosition;

            followTarget.localPosition = Vector3.Lerp(followTarget.localPosition, targetHeight, slopeAdjustmentSpeed * dot * Time.deltaTime);
        }

        private void OnApplicationFocus(bool hasFocus) => Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        
    }
    
    
    
}

