using System;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [Header("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        [SerializeField] private Transform followTarget;
        [SerializeField] private Transform eyeTarget;

        [SerializeField] private List<CameraBehaviour> listOfBehaviourReferences;

        private readonly Dictionary<Type, CameraBehaviour> behaviours = new Dictionary<Type, CameraBehaviour>();
        
        private CameraBehaviour currentCameraBehaviour;
        
        private InputMaster inputMaster;

        private void Awake() {

            foreach (CameraBehaviour cameraBehaviour in listOfBehaviourReferences)
                behaviours[cameraBehaviour.GetType()] = cameraBehaviour;
            
            inputMaster = new InputMaster();
            inputMaster.Enable();
            ChangeBehaviour(behaviours[typeof(WorldBehaviour)]);
        }

        private void OnEnable() {
            EventHandler<StartPuzzleEvent>.RegisterListener(ChangeToPuzzleCamera);
            EventHandler<ExitPuzzleEvent>.RegisterListener(ChangeToWorldCamera);
        }

        private void ChangeToWorldCamera(ExitPuzzleEvent exitPuzzleEvent) => ChangeBehaviour(behaviours[typeof(WorldBehaviour)]);
        private void ChangeToPuzzleCamera(StartPuzzleEvent startPuzzleEvent) {
            
            ChangeBehaviour(behaviours[typeof(PuzzleBehaviour)]);

            PuzzleBehaviour puzzleBehaviour = currentCameraBehaviour as PuzzleBehaviour;

            puzzleBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos.rotation);
            
        }

        private void LateUpdate() => currentCameraBehaviour.Behave();
        
        private void ChangeBehaviour(CameraBehaviour newCameraBehaviour) {
            currentCameraBehaviour = newCameraBehaviour;
            currentCameraBehaviour.Initialize(transform, followTarget);
        }
        
        
        private void OnApplicationFocus(bool hasFocus) => Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        
        [ContextMenu("Auto-assign targets", false,0)]
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

