using System;
using System.Collections.Generic;
using NewCamera;
using UnityEditor;
using UnityEngine;


public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private GlobalCameraSettings globalCameraSettings;
    
    private BaseCameraBehaviour currentBaseCameraBehaviour;

    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector2 clampValues;
    
    private Vector2 input;
    private Transform thisTransform;

    [SerializeField] private BehaviourData defaultValues;
    [SerializeField] private BehaviourData glideValues;
    [SerializeField] private BehaviourData idleValues;
    [SerializeField] private BehaviourData puzzleValues;
    


    private readonly Dictionary<Type, BaseCameraBehaviour> lowPriorityBehaviours = new Dictionary<Type, BaseCameraBehaviour>();
    private readonly Dictionary<Type, BaseCameraBehaviour> highPriorityBehaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private void Awake() {

        Cursor.lockState = CursorLockMode.Locked;

        thisTransform = transform;
        currentBaseCameraBehaviour = new BaseCameraBehaviour(thisTransform, followTarget, defaultValues);
        
        lowPriorityBehaviours.Add(typeof(IdleBehaviour), new IdleBehaviour(thisTransform, followTarget, idleValues)); //When player is not playing
        lowPriorityBehaviours.Add(typeof(StationaryBehaviour), new StationaryBehaviour(thisTransform, followTarget, defaultValues)); //When player 
        lowPriorityBehaviours.Add(typeof(PuzzleBaseCameraBehaviour), new PuzzleBaseCameraBehaviour(thisTransform, followTarget, puzzleValues )); //When player 
        
        highPriorityBehaviours.Add(typeof(GlideState), new GlideBaseCameraBehaviour(thisTransform, followTarget, glideValues));
        highPriorityBehaviours.Add(typeof(WalkState), new BaseCameraBehaviour(transform, followTarget, defaultValues));
    }
    
    private void LateUpdate() {
        ReadInput();
        
        input = currentBaseCameraBehaviour.ClampMovement(input, clampValues);
        
        Vector3 calculatedOffset = currentBaseCameraBehaviour.ExecuteCollision(input, globalCameraSettings);
        
        thisTransform.position = currentBaseCameraBehaviour.ExecuteMove(calculatedOffset);
        thisTransform.rotation = currentBaseCameraBehaviour.ExecuteRotate();
    }
    
    private void ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -inputDirection.y * globalCameraSettings.MouseSensitivity;
        input.y += inputDirection.x * globalCameraSettings.MouseSensitivity;
        
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

        if (highPriorityBehaviours.ContainsKey(stateChangeEvent.newState.GetType()))
            ChangeBehaviour(highPriorityBehaviours[stateChangeEvent.newState.GetType()]);
        
    }

    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        ChangeBehaviour(lowPriorityBehaviours[typeof(StationaryBehaviour)]);
    }

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            
        ChangeBehaviour(lowPriorityBehaviours[typeof(PuzzleBaseCameraBehaviour)]);

        PuzzleBaseCameraBehaviour puzzleBaseBehaviour = currentBaseCameraBehaviour as PuzzleBaseCameraBehaviour;

        puzzleBaseBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos);
            
    }
    
    private void ChangeBehaviour(BaseCameraBehaviour newBaseCameraBehaviour) => currentBaseCameraBehaviour = newBaseCameraBehaviour;

    [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            followTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
           // globalCameraSettings = AssetDatabase.LoadAssetAtPath<GlobalCameraSettings>("Assets/Scripts/DynamicCamera/New Camera/InGameReferences/GlobalCameraSettings.asset");
          //  inputReference = AssetDatabase.LoadAssetAtPath<ControllerInputReference>("Assets/Scripts/DynamicCamera/Controller Input Reference.asset");
            
        } catch (NullReferenceException e) {
            Debug.Log("Couldn't find one or all targets, check if they have the right tag");
        }
    }
    
}
