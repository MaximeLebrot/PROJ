using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewCamera;
using UnityEngine;


public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private GlobalCameraSettings globalCameraSettings;
    
    private BaseCameraBehaviour currentBaseCameraBehaviour;

    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform shoulderPosition;
    [SerializeField] private Vector2 clampValues;
    
    private Transform thisTransform;

    [SerializeField] private BehaviourData defaultValues;
    [SerializeField] private BehaviourData glideValues;
    [SerializeField] private BehaviourData idleValues;
    [SerializeField] private BehaviourData puzzleValues;
    [SerializeField] private BehaviourData oneSwitchValues;
    
    [SerializeField] private bool oneSwitchMode;
    
    private readonly Dictionary<Type, BaseCameraBehaviour> behaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private delegate void BehaviourQueue();
    private event BehaviourQueue behaviourQueue;

    private void Awake() {
        inputReference.Initialize();
        thisTransform = transform;
        
        currentBaseCameraBehaviour = oneSwitchMode ? new OneSwitchCameraBehaviour(transform, followTarget, oneSwitchValues) : new BaseCameraBehaviour(thisTransform, followTarget, defaultValues);
        
        behaviours.Add(typeof(IdleBehaviour), new IdleBehaviour(thisTransform, followTarget, idleValues)); 
        behaviours.Add(typeof(StationaryBehaviour), new StationaryBehaviour(thisTransform, followTarget, defaultValues));  
        behaviours.Add(typeof(PuzzleBaseCameraBehaviour), new PuzzleBaseCameraBehaviour(thisTransform, followTarget, puzzleValues ));
        behaviours.Add(typeof(GlideState), new GlideBaseCameraBehaviour(thisTransform, followTarget, glideValues));
        behaviours.Add(typeof(WalkState), new BaseCameraBehaviour(transform, followTarget, defaultValues));
        behaviours.Add(typeof(OSSpinState), new OneSwitchCameraBehaviour(transform, followTarget, oneSwitchValues));
        behaviours.Add(typeof(OSWalkState), behaviours[typeof(OSSpinState)]);
        behaviours.Add(typeof(OSPuzzleState), behaviours[typeof(PuzzleBaseCameraBehaviour)]);
        
        behaviourQueue = ExecuteCameraBehaviour;
    }
    private void LateUpdate() => behaviourQueue?.Invoke();

    private void ExecuteCameraBehaviour() {
        ReadInput();

        CustomInput input = ReadInput();
        
        currentBaseCameraBehaviour.ManipulatePivotTarget(input, clampValues);
        
        Vector3 calculatedOffset = currentBaseCameraBehaviour.ExecuteCollision(globalCameraSettings);
        
        thisTransform.position = currentBaseCameraBehaviour.ExecuteMove(calculatedOffset);
        thisTransform.rotation = currentBaseCameraBehaviour.ExecuteRotate();
    }
    
    private CustomInput ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();

        CustomInput input = new CustomInput();
        
        input.aim.x = -inputDirection.y * globalCameraSettings.MouseSensitivity;
        input.aim.y = inputDirection.x * globalCameraSettings.MouseSensitivity;
        
        input.movement = inputReference.InputMaster.Movement.ReadValue<Vector2>();

        return input;
    }
    
    private void OnEnable() {
        EventHandler<StartPuzzleEvent>.RegisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        EventHandler<PlayerStateChangeEvent>.RegisterListener(OnPlayerStateChange);
        EventHandler<CameraLookAndMoveToEvent>.RegisterListener(OnLookAndMove);
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<PlayerStateChangeEvent>.UnregisterListener(OnPlayerStateChange);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnLookAndMove);
    }

    private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(behaviours[typeof(IdleBehaviour)]);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
    }

    private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(behaviours[typeof(StationaryBehaviour)]);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnReturnToKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
    }

    private void OnPlayerStateChange(PlayerStateChangeEvent stateChangeEvent) {

        if (behaviours.ContainsKey(stateChangeEvent.newState.GetType()))
            ChangeBehaviour(behaviours[stateChangeEvent.newState.GetType()]);
        
    }

    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        ChangeBehaviour(behaviours[typeof(StationaryBehaviour)]);
    }

    private async void OnLookAndMove(CameraLookAndMoveToEvent lookAndMove) {

        MoveToTransition moveToTransition = new MoveToTransition(ref thisTransform, lookAndMove.endPosition, lookAndMove.moveToEventData);
        LookAtTransition lookAtTransition = new LookAtTransition(ref thisTransform, lookAndMove.endRotation, lookAndMove.lookAtEventData);

        List<Task> transitions = new List<Task>(2) { moveToTransition.Transit(), lookAtTransition.Transit()};

        await PlayTransitions(transitions);
        
        ResetBehindPlayerTransition resetBehindPlayerTransition = new ResetBehindPlayerTransition(ref thisTransform, shoulderPosition.position, shoulderPosition.rotation, .2f);

        await PlayTransition(resetBehindPlayerTransition);
    }

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            
        ChangeBehaviour(behaviours[typeof(PuzzleBaseCameraBehaviour)]);

        PuzzleBaseCameraBehaviour puzzleBaseBehaviour = currentBaseCameraBehaviour as PuzzleBaseCameraBehaviour;

        puzzleBaseBehaviour?.AssignRotation(startPuzzleEvent.info.puzzlePos);
    }
    
    private void ChangeBehaviour(BaseCameraBehaviour newBaseCameraBehaviour) => currentBaseCameraBehaviour = newBaseCameraBehaviour;

    private async Task PlayTransition(CameraTransition cameraTransition) {

        SetBehaviourExecutionActive(false);
        
        await cameraTransition.Transit();

        SetBehaviourExecutionActive(true);
    }

    private async Task PlayTransitions(List<Task> transitions) {
        
        SetBehaviourExecutionActive(false);
        
        await Task.WhenAll(transitions);

        SetBehaviourExecutionActive(true);
    }

    
    private void SetBehaviourExecutionActive(bool isActive) {
        if (isActive) { 
            behaviourQueue = ExecuteCameraBehaviour;
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        }
        else {
            behaviourQueue = null;
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        }
    }
    
    [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            followTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
            shoulderPosition = GameObject.FindWithTag("ShoulderCameraPosition").transform;
        } catch (NullReferenceException e) {
            Debug.Log("Couldn't find one or all targets, check if they have the right tag");
        }
    }
}

public struct CustomInput {
    public Vector2 aim;
    public Vector2 movement;
}
