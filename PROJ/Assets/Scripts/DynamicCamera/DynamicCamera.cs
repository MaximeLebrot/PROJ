using System;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicCamera {
    
    public class DynamicCamera : MonoBehaviour {
        
        [Header("IF PLAYER IS TARGET: Assign an empty transform as a child to the player , not the actual player")] 
        [SerializeField] private Transform followTarget;
        [SerializeField] private Transform eyeTarget;

        [SerializeField] private List<CameraBehaviour> listOfBehaviourReferences;
        [SerializeField] private BehaviourCallback behaviourCallback;

        private readonly Dictionary<Type, CameraBehaviour> behaviours = new Dictionary<Type, CameraBehaviour>();
        
        private CameraBehaviour currentCameraBehaviour;

        public Vector2 input;

        private void Awake() {
            foreach (CameraBehaviour cameraBehaviour in listOfBehaviourReferences)
                behaviours[cameraBehaviour.GetType()] = cameraBehaviour;
            
            ChangeBehaviour(behaviours[typeof(StationaryBehaviour)], followTarget);
        }
        
        private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
            ChangeBehaviour(behaviours[typeof(WalkBehaviour)], followTarget);
        }

        private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            
            ChangeBehaviour(behaviours[typeof(PuzzleBehaviour)], followTarget);

            PuzzleBehaviour puzzleBehaviour = currentCameraBehaviour as PuzzleBehaviour;

            puzzleBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos);
            
        }

        private void OnPlayerStateChange(PlayerStateChangeEvent stateChangeEvent) {
            CameraBehaviour newBehaviour = behaviourCallback.GetCameraBehaviourCallback(stateChangeEvent.newState);

            if (newBehaviour != null)
                ChangeBehaviour(newBehaviour, followTarget);
        }
        
        private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
            ChangeBehaviour(behaviours[typeof(IdleBehaviour)], eyeTarget);
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
        }

        private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
            ChangeBehaviour(behaviours[typeof(WalkBehaviour)], followTarget);
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnReturnToKeyboard);
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        }
        
        private void LateUpdate() => currentCameraBehaviour.ExecuteBehaviour();

        private void ChangeBehaviour(CameraBehaviour newCameraBehaviour, Transform target) {
            currentCameraBehaviour = newCameraBehaviour;
            currentCameraBehaviour.Initialize(transform, target);
        }
        
        private void OnEnable() {
            EventHandler<StartPuzzleEvent>.RegisterListener(OnPuzzleStart);
            EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
            EventHandler<PlayerStateChangeEvent>.RegisterListener(OnPlayerStateChange);
        }

        private void OnDisable() {
            EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
            EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            EventHandler<PlayerStateChangeEvent>.UnregisterListener(OnPlayerStateChange);
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

