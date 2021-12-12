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

    [SerializeField] private List<BaseCameraBehaviour> cameraBehaviours;
    [SerializeField] private Transitioner transitioner;
    
    private Transform thisTransform;
    private bool lockInput;
    
    private readonly Dictionary<Type, BaseCameraBehaviour> behaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private delegate void BehaviourQueue();
    private event BehaviourQueue behaviourQueue;

    private void Awake() {
        inputReference.Initialize();
        transitioner.Initialize();
        thisTransform = transform;
        
        behaviours.Add(typeof(PuzzleCameraBehaviour),  cameraBehaviours[2]);
        behaviours.Add(typeof(BaseCameraBehaviour),  cameraBehaviours[0]);
        behaviours.Add(typeof(IdleBehaviour),  cameraBehaviours[1]);
        behaviours.Add(typeof(WalkState),  cameraBehaviours[0]);
        behaviours.Add(typeof(OneHandCameraBehaviour),  cameraBehaviours[3]);
        behaviours.Add(typeof(InGameMenuCameraBehaviour),  cameraBehaviours[4]);
        behaviours.Add(typeof(TransportationBegunEvent),  cameraBehaviours[5]);
        
        currentBaseCameraBehaviour = behaviours[typeof(BaseCameraBehaviour)];
        
        currentBaseCameraBehaviour.InjectReferences(thisTransform, followTarget);
        
        behaviourQueue = ExecuteCameraBehaviour;
    }
    private void LateUpdate() => behaviourQueue?.Invoke();

    private void ExecuteCameraBehaviour() {
        CustomInput input = new CustomInput();
        
        if (lockInput == false) 
            input = ReadInput();
        
        currentBaseCameraBehaviour.ManipulatePivotTarget(input);
        
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
        EventHandler<LockInputEvent>.RegisterListener(LockInput);
        EventHandler<SaveSettingsEvent>.RegisterListener(UpdateSettings);
        EventHandler<InGameMenuEvent>.RegisterListener(ActivateMenuCamera);
        EventHandler<TransportationBegunEvent>.RegisterListener(OnTransportationEvent);
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<PlayerStateChangeEvent>.UnregisterListener(OnPlayerStateChange);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnLookAndMove);
        EventHandler<LockInputEvent>.UnregisterListener(LockInput);
        EventHandler<SaveSettingsEvent>.UnregisterListener(UpdateSettings);
        EventHandler<InGameMenuEvent>.UnregisterListener(ActivateMenuCamera);
        EventHandler<TransportationBegunEvent>.UnregisterListener(OnTransportationEvent);
    }

    private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour<IdleBehaviour>();
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
    }

    private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
        ChangeBehaviour<BaseCameraBehaviour>();
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnReturnToKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
    }

    private void OnPlayerStateChange(PlayerStateChangeEvent stateChangeEvent) {
        if (behaviours.ContainsKey(stateChangeEvent.newState.GetType()))
            ChangeBehaviour(stateChangeEvent.newState.GetType());
    }

    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        ChangeBehaviour<BaseCameraBehaviour>();
    }

    private void OnLookAndMove(CameraLookAndMoveToEvent lookAndMove) {
        
    }

    private void UpdateSettings(SaveSettingsEvent saveEvent) {

        bool oneHandMode = saveEvent.settingsData.oneHandMode;
        
        if(oneHandMode)
            ChangeBehaviour<OneHandCameraBehaviour>();

    }

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        
        ChangeBehaviour<PuzzleCameraBehaviour>();

        PuzzleCameraBehaviour puzzleBehaviour = currentBaseCameraBehaviour as PuzzleCameraBehaviour;

        puzzleBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos);
    }
    
    private void ChangeBehaviour<T>() where T : BaseCameraBehaviour {
        currentBaseCameraBehaviour = behaviours[typeof(T)];
        currentBaseCameraBehaviour.InjectReferences(thisTransform, followTarget);
        currentBaseCameraBehaviour.EnterBehaviour();
    }

    
    private void ChangeBehaviour(Type type) {
        currentBaseCameraBehaviour = behaviours[type];
        currentBaseCameraBehaviour.InjectReferences(thisTransform, followTarget);
        currentBaseCameraBehaviour.EnterBehaviour();
    }
    
    private async Task PlayTransition<T>(CameraTransition<T> cameraTransition) where T : TransitionData {

        SetBehaviourExecutionActive(false);
        
        await cameraTransition.RunTransition(thisTransform);

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

    private void ActivateMenuCamera(InGameMenuEvent inGameMenuEvent) {
        if(inGameMenuEvent.Activate)
            ChangeBehaviour<InGameMenuCameraBehaviour>();
        else 
            ChangeBehaviour<BaseCameraBehaviour>();
    }

    private void OnTransportationEvent(TransportationBegunEvent transportationBegunEvent) {
        ChangeBehaviour(typeof(TransportationBegunEvent));
        EventHandler<TransportationEndedEvent>.RegisterListener(OnTransportationEvent);
        EventHandler<TransportationBegunEvent>.UnregisterListener(OnTransportationEvent);
        
    }
    
    private void OnTransportationEvent(TransportationEndedEvent transportationBegunEvent) {
        ChangeBehaviour<BaseCameraBehaviour>();
        EventHandler<TransportationEndedEvent>.UnregisterListener(OnTransportationEvent);
        EventHandler<TransportationBegunEvent>.RegisterListener(OnTransportationEvent);
    }
    
    [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            followTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
            shoulderPosition = GameObject.FindWithTag("ShoulderCameraPosition").transform;
        } catch (NullReferenceException e) {
            Debug.Log("Couldn't find one or all targets, check if they have the right tag");
            Debug.Log(e);
        }
    }
}

public struct CustomInput {
    public Vector2 aim;
    public Vector2 movement;
}
