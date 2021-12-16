using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewCamera;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private GlobalCameraSettings globalCameraSettings;
    
    private BaseCameraBehaviour currentBaseCameraBehaviour;

    [SerializeField] private Transform pivotTarget;
    [SerializeField] private Transform character;
    
    [SerializeField] private List<BaseCameraBehaviour> cameraBehaviours;
    [SerializeField] private Transitioner transitioner;
    
    private Transform thisTransform;
    private bool lockInput;
    
    private readonly Dictionary<Type, BaseCameraBehaviour> behaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private Type previousCameraBehaviour;
    
    private delegate void BehaviourQueue();
    private event BehaviourQueue behaviourQueue;

    private bool oneHandModeIsActive;
    private bool pendingAccessibilityUpdate;

    private void Awake() {
        DontDestroyOnLoad(this);


        inputReference.Initialize();
        transitioner.Initialize();
        thisTransform = transform;
        
        behaviours.Add(typeof(PuzzleCameraBehaviour),  cameraBehaviours[2]);
        behaviours.Add(typeof(BaseCameraBehaviour),  cameraBehaviours[0]);
        behaviours.Add(typeof(IdleBehaviour),  cameraBehaviours[1]);
        behaviours.Add(typeof(OneHandCameraBehaviour),  cameraBehaviours[3]);
        behaviours.Add(typeof(InGameMenuCameraBehaviour),  cameraBehaviours[4]);
        behaviours.Add(typeof(TransportationBegunEvent),  cameraBehaviours[5]);
        
        ChangeBehaviour<BaseCameraBehaviour>();
        
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
        EventHandler<CameraLookAndMoveToEvent>.RegisterListener(OnLookAndMove);
        EventHandler<LockInputEvent>.RegisterListener(LockInput);
        EventHandler<InGameMenuEvent>.RegisterListener(ActivateMenuCamera);
        EventHandler<TransportationBegunEvent>.RegisterListener(OnTransportationEvent);
        EventHandler<SaveSettingsEvent>.RegisterListener(OnSettingsChanged);
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnLookAndMove);
        EventHandler<LockInputEvent>.UnregisterListener(LockInput);
        EventHandler<InGameMenuEvent>.UnregisterListener(ActivateMenuCamera);
        EventHandler<TransportationBegunEvent>.UnregisterListener(OnTransportationEvent);
        EventHandler<SaveSettingsEvent>.UnregisterListener(OnSettingsChanged);
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
    

    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {

        //if (exitPuzzleEvent.success) {
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
            ChangeBehaviour(previousCameraBehaviour);
        //}
    }

    private void OnLookAndMove(CameraLookAndMoveToEvent lookAndMove) {
        if (oneHandModeIsActive)
            ChangeBehaviour<OneHandCameraBehaviour>();
        else
            ChangeBehaviour<BaseCameraBehaviour>();
    }


    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
        
        //PuzzleBehaviour already active.
        if (currentBaseCameraBehaviour.GetType() == typeof(PuzzleCameraBehaviour)) 
            return;

        previousCameraBehaviour = currentBaseCameraBehaviour.GetType();
        
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);   
        
        ChangeBehaviour<PuzzleCameraBehaviour>();
        
        PuzzleCameraBehaviour puzzleBehaviour = currentBaseCameraBehaviour as PuzzleCameraBehaviour;
        
        puzzleBehaviour.AssignRotation(startPuzzleEvent.info.puzzlePos);
    }

    private void ChangeBehaviour<T>() where T : BaseCameraBehaviour {
        
        if(pendingAccessibilityUpdate)
            HandlePendingAccessibilityUpdate();
        else {
            currentBaseCameraBehaviour = behaviours[typeof(T)];
            currentBaseCameraBehaviour.InjectReferences(thisTransform, pivotTarget, character);
            currentBaseCameraBehaviour.EnterBehaviour();
        }
    }

    
    private void ChangeBehaviour(Type type) {
        if (pendingAccessibilityUpdate) {
            HandlePendingAccessibilityUpdate();
        } 
        else {
            currentBaseCameraBehaviour = behaviours[type];
            currentBaseCameraBehaviour.InjectReferences(thisTransform, pivotTarget, character);
            currentBaseCameraBehaviour.EnterBehaviour();
        }
    }
    
    private void LockInput(LockInputEvent lockInputEvent) => lockInput = lockInputEvent.lockInput;

    private void ActivateMenuCamera(InGameMenuEvent inGameMenuEvent) {
        if (inGameMenuEvent.Activate) {
            previousCameraBehaviour = currentBaseCameraBehaviour.GetType();
            ChangeBehaviour<InGameMenuCameraBehaviour>();
        }
        else 
            ChangeBehaviour(previousCameraBehaviour);
    }

    private void OnTransportationEvent(TransportationBegunEvent transportationBegunEvent) {
        previousCameraBehaviour = currentBaseCameraBehaviour.GetType();
        ChangeBehaviour(typeof(TransportationBegunEvent));
        EventHandler<TransportationEndedEvent>.RegisterListener(OnTransportationEvent);
        EventHandler<TransportationBegunEvent>.UnregisterListener(OnTransportationEvent);
        
    }
    
    private void OnTransportationEvent(TransportationEndedEvent transportationBegunEvent) {
        ChangeBehaviour(previousCameraBehaviour);
        EventHandler<TransportationEndedEvent>.UnregisterListener(OnTransportationEvent);
        EventHandler<TransportationBegunEvent>.RegisterListener(OnTransportationEvent);
    }

    private void OnSettingsChanged(SaveSettingsEvent settingsEvent) {

        bool oneHandModeChanged = oneHandModeIsActive == settingsEvent.settingsData.oneHandMode;
        
        //Jesus christ
        if (oneHandModeChanged == false) {

            oneHandModeIsActive = settingsEvent.settingsData.oneHandMode;

            if (currentBaseCameraBehaviour) {
                Type currentBehaviourType = currentBaseCameraBehaviour.GetType();

                if (currentBehaviourType != typeof(BaseCameraBehaviour) || currentBehaviourType != typeof(OneHandCameraBehaviour)) {
                    pendingAccessibilityUpdate = true;
                }
                else 
                    HandlePendingAccessibilityUpdate();
            }
            else 
                HandlePendingAccessibilityUpdate();
            
        }
    }
    private void HandlePendingAccessibilityUpdate() {

        pendingAccessibilityUpdate = false;
        
        if (oneHandModeIsActive)
            ChangeBehaviour<OneHandCameraBehaviour>();
        else
            ChangeBehaviour<BaseCameraBehaviour>();
    }

        [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            pivotTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
            character = GameObject.FindObjectOfType<MetaPlayerController>().transform;
        }
        catch (NullReferenceException e) {
            Debug.Log(e);
        }
    }
       /*------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    
    private async Task PlayTransition<T>(CameraTransition<T> cameraTransition) where T : TransitionData
    {

        SetBehaviourExecutionActive(false);

        await cameraTransition.RunTransition(thisTransform);

        SetBehaviourExecutionActive(true);
    }

    private async Task PlayTransitions(List<Task> transitions)
    {

        SetBehaviourExecutionActive(false);

        await Task.WhenAll(transitions);

        SetBehaviourExecutionActive(true);
    }


    private void SetBehaviourExecutionActive(bool isActive)
    {
        if (isActive)
        {
            behaviourQueue = ExecuteCameraBehaviour;
            EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        }
        else
        {
            behaviourQueue = null;
            EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        }
    }


}

public struct CustomInput {
    public Vector2 aim;
    public Vector2 movement;
}
