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

    [SerializeField] private List<CompositeCameraBehaviour> cameraBehaviours;
    
    private Transform thisTransform;
    private bool lockInput;
    
    private readonly Dictionary<Type, BaseCameraBehaviour> behaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private delegate void BehaviourQueue();
    private event BehaviourQueue behaviourQueue;

    private void Awake() {
        inputReference.Initialize();
        thisTransform = transform;
        
        foreach (CompositeCameraBehaviour newBehaviour in cameraBehaviours) {
            
            if(newBehaviour.HasMultipleCallbackTypes())
                AddCallbacks(newBehaviour.GetCallbackTypes(), newBehaviour.cameraBehaviour);
            else
                AddCallback(newBehaviour.GetCallbackType(0), newBehaviour.cameraBehaviour);

            newBehaviour.cameraBehaviour.InjectReferences(thisTransform, followTarget);
        }

        currentBaseCameraBehaviour = behaviours[typeof(BaseCameraBehaviour)];
        
        behaviourQueue = ExecuteCameraBehaviour;
    }
    private void LateUpdate() => behaviourQueue?.Invoke();

    private void ExecuteCameraBehaviour() {
        if (lockInput)
            return;
        
        ReadInput();

        CustomInput input = ReadInput();
        
        currentBaseCameraBehaviour.ManipulatePivotTarget(input);
        
        Vector3 calculatedOffset = currentBaseCameraBehaviour.ExecuteCollision(globalCameraSettings);
        
        thisTransform.position = currentBaseCameraBehaviour.ExecuteMove(calculatedOffset);
        thisTransform.rotation = currentBaseCameraBehaviour.ExecuteRotate();
    }

    private void AddCallback(Type type, BaseCameraBehaviour cameraBehaviour) {
        behaviours.Add(type, cameraBehaviour);
    }

    private void AddCallbacks(Type[] types, BaseCameraBehaviour cameraBehaviour) {
        foreach(Type type in types)
            AddCallback(type, cameraBehaviour);
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
        EventHandler<LockInputEvent>.RegisterListener(LockInput);
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<PlayerStateChangeEvent>.UnregisterListener(OnPlayerStateChange);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnLookAndMove);
        EventHandler<LockInputEvent>.UnregisterListener(LockInput);
    }

    private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(behaviours[typeof(IdleBehaviour)]);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
    }

    private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour(behaviours[typeof(BaseCameraBehaviour)]);
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
            
        ChangeBehaviour(behaviours[typeof(PuzzleCameraBehaviour)]);

        PuzzleCameraBehaviour puzzleBehaviour = currentBaseCameraBehaviour as PuzzleCameraBehaviour;

        puzzleBehaviour?.AssignRotation(startPuzzleEvent.info.puzzlePos);
    }
    
    private void ChangeBehaviour(BaseCameraBehaviour newBaseCameraBehaviour) {
        currentBaseCameraBehaviour = newBaseCameraBehaviour;
        currentBaseCameraBehaviour.EnterBehaviour();
    }

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
    
    private void LockInput(LockInputEvent lockInputEvent) => lockInput = lockInputEvent.lockInput;

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
