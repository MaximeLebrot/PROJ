using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NewCamera;
using UnityEngine;
using UnityEngine.InputSystem.Editor;

public class CameraDirector : PersistentSingleton<CameraDirector> {
    
    [SerializeField] private Transform pivotTarget;
    [SerializeField] private Transform character;

    [SerializeField] private List<GameCamera> wantedCameras;

    private Dictionary<Type, GameCamera> gameCameras;

    private GameCamera currentDefaultCamera;
    private GameCamera activeCamera;
    
    private CancellationTokenSource cancellationTokenSource;
    [SerializeField] private LookAndMoveTransitionData smoothResetData;
    
    private void Awake() {

        gameCameras = new Dictionary<Type, GameCamera>();
        
        DontDestroyOnLoad(this);

        foreach (GameCamera gameCamera in wantedCameras) {
            gameCamera.Initialize(transform, pivotTarget, character);
            gameCameras.Add(gameCamera.GetType(), gameCamera);
        }
 
        SwitchCamera<DefaultCamera>();
        
    }
    
    private void LateUpdate() => activeCamera.ExecuteCameraBehaviour();
    
    private void SwitchCamera<T>() where T : GameCamera {
        currentDefaultCamera = activeCamera = gameCameras[typeof(T)];
        activeCamera.ResetCamera();
    }
    
    private void SwitchCamera<T>(T newCamera) where T : GameCamera {
        currentDefaultCamera = activeCamera = gameCameras[newCamera.GetType()];
        activeCamera.ResetCamera();
    }
    
    private void Start() {
        
        (GameMenuController.Instance.RequestOption<ControlMode>() as ControlMode).AddListener((option) => {

            switch (option) {
                case "Default":
                    currentDefaultCamera = gameCameras[typeof(DefaultCamera)];
                    break;
                case "One Hand Mode":
                    currentDefaultCamera = gameCameras[typeof(OneHandCamera)];
                    break;
                case "OneSwitch Mode":
                    currentDefaultCamera = gameCameras[typeof(OneSwitchCamera)];
                    break;
                default:
                        currentDefaultCamera = gameCameras[typeof(DefaultCamera)];
                        break;

            }
            
        });
        
        (GameMenuController.Instance.RequestOption<VoiceControl>() as VoiceControl).AddListener((option) => {

                if (option.Equals("Voice Only") || option.Equals("Voice + Mouse"))
                    SwitchCamera<OneHandCamera>();
                else {
                    SwitchCamera<DefaultCamera>();
                }
            }
        );
    

    }
    
    private void OnEnable() { 
        EventHandler<StartPuzzleEvent>.RegisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        EventHandler<InGameMenuEvent>.RegisterListener(ActivateMenuCamera);
        EventHandler<CameraLookAndMoveToEvent>.RegisterListener(OnCameraLookAndMove);
        EventHandler<CameraLookAtEvent>.RegisterListener(OnCameraLook);
        
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleStart);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<InGameMenuEvent>.UnregisterListener(ActivateMenuCamera);
        EventHandler<CameraLookAndMoveToEvent>.UnregisterListener(OnCameraLookAndMove);
        EventHandler<CameraLookAtEvent>.UnregisterListener(OnCameraLook);
    }

    
    private void OnAwayFromKeyboard(AwayFromKeyboardEvent e) {
        activeCamera.ChangeBehavior<IdleBehaviour>();
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnReturnToKeyboard);
    }

    private void OnReturnToKeyboard(AwayFromKeyboardEvent e) {
        activeCamera.ResetCamera();
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnReturnToKeyboard);
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
    }
    

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
        
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);   
        
        PuzzleCameraBehaviour puzzleCameraBehaviour = activeCamera.ChangeBehavior<PuzzleCameraBehaviour>();
        
        puzzleCameraBehaviour.InitializePuzzleCamera(startPuzzleEvent.info.puzzle);
    }

    
    private void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        activeCamera.ResetCamera();
    }

    private void ActivateMenuCamera(InGameMenuEvent inGameMenuEvent) {
        if (inGameMenuEvent.Activate)
            activeCamera = gameCameras[typeof(NullCamera)];
        else
            SwitchCamera(currentDefaultCamera);
    }
    
    private async Task PlayTransition<T>(CameraTransition<T> transition) where T : TransitionData {
        
        activeCamera = gameCameras[typeof(TransitionCamera)];

        await ((TransitionCamera)activeCamera).PlayTransition(transition);

        SwitchCamera(currentDefaultCamera);
        
    }
    
    private async void OnCameraLookAndMove(CameraLookAndMoveToEvent cameraLookAndMoveToEvent) {
        await PlayTransition(new LookAndMoveTransition(cameraLookAndMoveToEvent.lookAndMoveTransitionData, cameraLookAndMoveToEvent.targetTransform));
    }
    
    private async void OnCameraLook(CameraLookAtEvent cameraLookAtEvent) {
        await PlayTransition(new LookAtEvent(cameraLookAtEvent.transitionData, cameraLookAtEvent.lookAtTarget)); 
    }
    

    #if UNITY_EDITOR
    
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
    
    #endif
    

}


