using System;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField]
        [Tooltip("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        private Transform followTarget;

        [SerializeField] private Transform eyeTarget;
        [SerializeField] private CameraBehaviour cameraBehaviour;
        
        private InputMaster inputMaster;

        private void Awake() {
            inputMaster = new InputMaster();
            inputMaster.Enable();
            cameraBehaviour.Initialize(transform);
            cameraBehaviour.AssignTarget(followTarget);
            
        }

        private void OnEnable() {
            EventHandler<StartPuzzleEvent>.RegisterListener(puzzle => Debug.Log("hi"));
        }

        private void LateUpdate() => cameraBehaviour.Behave();

        private CameraBehaviour ChangeBehaviour(CameraBehaviour newCameraBehaviour) {
            cameraBehaviour = newCameraBehaviour;
            return cameraBehaviour;
        }
        
        
        private void OnApplicationFocus(bool hasFocus) => Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;

        [ContextMenu("Auto-assign targets")]
        public void AssignTargets() {
            try {
                followTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
                eyeTarget = GameObject.FindWithTag("EyeTarget").transform;
            } catch (NullReferenceException e) {

                Debug.Log("Couldn't find one or all targets, check if they have the right tag");
            }
            
            
        }
        
    }
    
    
    
}

