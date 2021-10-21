using System;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [SerializeField]
        [Tooltip("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        private Transform followTarget;

        [SerializeField] private Transform eyeTarget;
        [SerializeField] private CameraBehaviour puzzleCamera;
        [SerializeField] private CameraBehaviour worldBehaviourCamera;
        
        private CameraBehaviour currentCameraBehaviour;
        
        
        private InputMaster inputMaster;

        private void Awake() {
            inputMaster = new InputMaster();
            inputMaster.Enable();
            currentCameraBehaviour = worldBehaviourCamera;
            currentCameraBehaviour.Initialize(transform);
            currentCameraBehaviour.AssignTarget(followTarget);
        }

        private void OnEnable() {
            EventHandler<StartPuzzleEvent>.RegisterListener(ChangeToPuzzleCamera);
            EventHandler<ExitPuzzleEvent>.RegisterListener(ChangeToWorldCamera);
        }

        private void ChangeToWorldCamera(ExitPuzzleEvent exitPuzzleEvent) => ChangeBehaviour(worldBehaviourCamera);
        private void ChangeToPuzzleCamera(StartPuzzleEvent startPuzzleEvent) => ChangeBehaviour(puzzleCamera);
        
        private void LateUpdate() => currentCameraBehaviour.Behave();
        
        private void ChangeBehaviour(CameraBehaviour newCameraBehaviour) {
            currentCameraBehaviour = newCameraBehaviour;
            currentCameraBehaviour.Initialize(transform);
            currentCameraBehaviour.AssignTarget(followTarget);
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

