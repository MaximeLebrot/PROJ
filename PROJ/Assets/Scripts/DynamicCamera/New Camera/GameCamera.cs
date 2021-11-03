using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private CameraBehaviourData cameraBehaviourData;
    [SerializeField] private BehaviourCallback behaviourCallback;

    private CameraBehaviour currentCameraBehaviour;
    private CameraBehaviour previousCameraBehaviour;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector2 clampValues;

    [SerializeField] private OffsetAndCameraSpeed defaultValues;
    [SerializeField] private OffsetAndCameraSpeed idleValues;
    [SerializeField] private OffsetAndCameraSpeed glideValues;
    [SerializeField] private OffsetAndCameraSpeed puzzleValues;
    
    private Vector2 input;
    private Transform thisTransform;

    private readonly Dictionary<Type, CameraBehaviour> lowPriorityBehaviours = new Dictionary<Type, CameraBehaviour>();
    private readonly Dictionary<Type, CameraBehaviour> highPriorityBehaviours = new Dictionary<Type, CameraBehaviour>();

    private void Awake() {
        
        previousCameraBehaviour = currentCameraBehaviour = new CameraBehaviour(transform, followTarget, defaultValues);
        thisTransform = transform;
        
        lowPriorityBehaviours.Add(typeof(IdleBehaviour), new IdleBehaviour(thisTransform, followTarget, idleValues)); //When player is not playing
        lowPriorityBehaviours.Add(typeof(StationaryBehaviour), new StationaryBehaviour(thisTransform, followTarget, defaultValues)); //When player 
        lowPriorityBehaviours.Add(typeof(PuzzleCameraBehaviour), new PuzzleCameraBehaviour(thisTransform, followTarget, puzzleValues )); //When player 
        
        
        highPriorityBehaviours.Add(typeof(GlideState), new GlideCameraBehaviour(thisTransform, followTarget, glideValues));
        highPriorityBehaviours.Add(typeof(WalkState), new CameraBehaviour(transform, followTarget, defaultValues));
    }
    
    private void LateUpdate() {
        ReadInput();
        
        input = currentCameraBehaviour.ClampMovement(input, clampValues);
        
        Vector3 calculatedOffset = currentCameraBehaviour.ExecuteCollision(input, cameraBehaviourData);
        
        thisTransform.position = currentCameraBehaviour.ExecuteMove(calculatedOffset);
        thisTransform.rotation = currentCameraBehaviour.ExecuteRotate();
    }
    
    private void ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -inputDirection.y * cameraBehaviourData.MouseSensitivity;
        input.y += inputDirection.x * cameraBehaviourData.MouseSensitivity;
        
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

    private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(lowPriorityBehaviours[typeof(IdleBehaviour)]);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
    }

    private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(lowPriorityBehaviours[typeof(StationaryBehaviour)]);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnReturnToKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
    }
    private void OnPlayerStateChange(PlayerStateChangeEvent stateChangeEvent) {
        CameraBehaviour newBehaviour = behaviourCallback.GetCameraBehaviourCallback(stateChangeEvent.newState);

        if (newBehaviour != null)
            ChangeBehaviour(newBehaviour);
    }
    
    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        ChangeBehaviour(lowPriorityBehaviours[typeof(StationaryBehaviour)]);
    }

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            
        ChangeBehaviour(highPriorityBehaviours[typeof(PuzzleCameraBehaviour)]);

        PuzzleCameraBehaviour puzzleBehaviour = currentCameraBehaviour as PuzzleCameraBehaviour;

        puzzleBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos);
            
    }
    
    private void ChangeBehaviour(CameraBehaviour newCameraBehaviour) => currentCameraBehaviour = newCameraBehaviour;

    [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            followTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
        } catch (NullReferenceException e) {
            Debug.Log("Couldn't find one or all targets, check if they have the right tag");
        }
    }


}

[System.Serializable]
public struct OffsetAndCameraSpeed {
    public Vector3 offset;
    public float followSpeed;
}