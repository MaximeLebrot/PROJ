using System;
using System.Collections.Generic;
using System.Threading;
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

    private bool behaviorExecutionIsAllowedToRun;
    
    private bool oneHandModeIsActive;
    private bool oneSwitchModeActive;

    private CancellationTokenSource cancellationTokenSource;
    
    private void Awake() {
        behaviorExecutionIsAllowedToRun = true;
        DontDestroyOnLoad(this);
        
        inputReference.Initialize();
        transitioner.Initialize();
        thisTransform = transform;
        
        behaviours.Add(typeof(BaseCameraBehaviour),  cameraBehaviours[0]);
        behaviours.Add(typeof(IdleBehaviour),  cameraBehaviours[1]);
        behaviours.Add(typeof(PuzzleCameraBehaviour),  cameraBehaviours[2]);
        behaviours.Add(typeof(OneHandCameraBehaviour),  cameraBehaviours[3]);
        behaviours.Add(typeof(InGameMenuCameraBehaviour),  cameraBehaviours[4]);
        behaviours.Add(typeof(TransportationBegunEvent),  cameraBehaviours[5]);
        behaviours.Add(typeof(SceneChangeCameraBehaviour),  cameraBehaviours[6]);
        
        ChangeBehaviour<BaseCameraBehaviour>();
        
        
    }
    
    private void LateUpdate() {
        if(behaviorExecutionIsAllowedToRun)
            ExecuteCameraBehaviour();
    }

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
        EventHandler<SceneChangeEvent>.RegisterListener(OnSceneChange);
        EventHandler<CameraLookAndMoveToEvent>.RegisterListener(OnCameraLookAndMove);
        EventHandler<CameraLookAtEvent>.RegisterListener(OnCameraLook);
        
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
        EventHandler<SceneChangeEvent>.UnregisterListener(OnSceneChange);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnCameraLookAndMove);
        EventHandler<CameraLookAtEvent>.UnregisterListener(OnCameraLook);
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
        
        puzzleBehaviour.InitializePuzzleCamera(startPuzzleEvent.info.puzzle);
    }

    private void ChangeBehaviour<T>() where T : BaseCameraBehaviour {
        currentBaseCameraBehaviour = behaviours[typeof(T)];
        currentBaseCameraBehaviour.InjectReferences(thisTransform, pivotTarget, character);
        currentBaseCameraBehaviour.EnterBehaviour();
    }
    
    
    private void ChangeBehaviour(Type type) {
        currentBaseCameraBehaviour = behaviours[type]; 
        currentBaseCameraBehaviour.InjectReferences(thisTransform, pivotTarget, character);
        currentBaseCameraBehaviour.EnterBehaviour();
    }
    
    private void LockInput(LockInputEvent lockInputEvent) => lockInput = lockInputEvent.lockInput;

    private void ActivateMenuCamera(InGameMenuEvent inGameMenuEvent) {
        if (inGameMenuEvent.Activate) {
            previousCameraBehaviour = currentBaseCameraBehaviour.GetType();
        }
        else 
            ChangeBehaviour(previousCameraBehaviour);
    }

    private async void OnCameraLookAndMove(CameraLookAndMoveToEvent cameraLookAndMoveToEvent) {
        await PlayTransition(new LookAndMoveTransition(cameraLookAndMoveToEvent.lookAndMoveTransitionData, cameraLookAndMoveToEvent.targetTransform));
    }

    private async void OnCameraLook(CameraLookAtEvent cameraLookAtEvent) {
        await PlayTransition(new LookAtEvent(cameraLookAtEvent.transitionData, cameraLookAtEvent.lookAtTarget)); 
    }
    
    private async Task PlayTransition<T>(CameraTransition<T> cameraTransition) where T : TransitionData {
        behaviorExecutionIsAllowedToRun = false;

        cancellationTokenSource = new CancellationTokenSource();
        
        await cameraTransition.RunTransition(thisTransform, cancellationTokenSource.Token);
        
        behaviorExecutionIsAllowedToRun = true;
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

        bool oneHandModeChanged = oneHandModeIsActive != settingsEvent.settingsData.oneHandMode;
        bool oneSwitchModeChanged = oneSwitchModeActive != settingsEvent.settingsData.oneSwitchMode;
        
        //Jesus christ
        if (oneHandModeChanged || oneSwitchModeChanged) {
            oneHandModeIsActive = settingsEvent.settingsData.oneHandMode;
            oneSwitchModeActive = settingsEvent.settingsData.oneSwitchMode;
            HandlePendingAccessibilityUpdate();
        }
    }

    private void HandlePendingAccessibilityUpdate() {

        if (oneHandModeIsActive || oneSwitchModeActive)
            ChangeBehaviour<OneHandCameraBehaviour>();
        else
            ChangeBehaviour<BaseCameraBehaviour>();

        previousCameraBehaviour = currentBaseCameraBehaviour.GetType();

    }
    
    private void OnSceneChange(SceneChangeEvent onSettingsChanged) {
        previousCameraBehaviour = currentBaseCameraBehaviour.GetType();
        EventHandler<SceneLoadedEvent>.RegisterListener(OnSceneLoaded);
        EventHandler<SceneChangeEvent>.UnregisterListener(OnSceneChange);
        ChangeBehaviour<SceneChangeCameraBehaviour>();
    }

    private void OnSceneLoaded(SceneLoadedEvent sceneLoadedEvent) {
        ChangeBehaviour(previousCameraBehaviour);
        EventHandler<SceneChangeEvent>.RegisterListener(OnSceneChange);
        EventHandler<SceneLoadedEvent>.UnregisterListener(OnSceneLoaded);
    }

    private void OnApplicationQuit() {
        cancellationTokenSource.Cancel();
    }

    [ContextMenu("Auto-assign targets", false,0)]
    public void AssignTargets() {
        try {
            pivotTarget = GameObject.FindWithTag("CameraFollowTarget").transform;
            character = FindObjectOfType<MetaPlayerController>().transform;
        }
        catch (NullReferenceException e) {
            Debug.Log(e);
        }
    }
       /*------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
    
       
       
}

public struct CustomInput {
    public Vector2 aim;
    public Vector2 movement;
}
