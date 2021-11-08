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
    
    private Vector2 input;
    private Transform thisTransform;

    [SerializeField] private BehaviourData defaultValues;
    [SerializeField] private BehaviourData glideValues;
    [SerializeField] private BehaviourData idleValues;
    [SerializeField] private BehaviourData puzzleValues;
    
    private readonly Dictionary<Type, BaseCameraBehaviour> behaviours = new Dictionary<Type, BaseCameraBehaviour>();

    private delegate void BehaviourQueue();

    private event BehaviourQueue behaviourQueue;

    private void Awake() {
        
        thisTransform = transform;
        currentBaseCameraBehaviour = new BaseCameraBehaviour(thisTransform, followTarget, defaultValues);
        
        behaviours.Add(typeof(IdleBehaviour), new IdleBehaviour(thisTransform, followTarget, idleValues)); 
        behaviours.Add(typeof(StationaryBehaviour), new StationaryBehaviour(thisTransform, followTarget, defaultValues));  
        behaviours.Add(typeof(PuzzleBaseCameraBehaviour), new PuzzleBaseCameraBehaviour(thisTransform, followTarget, puzzleValues ));
        behaviours.Add(typeof(GlideState), new GlideBaseCameraBehaviour(thisTransform, followTarget, glideValues));
        behaviours.Add(typeof(WalkState), new BaseCameraBehaviour(transform, followTarget, defaultValues));

        behaviourQueue = ExecuteCameraBehaviour;

    }

    private void LateUpdate() => behaviourQueue?.Invoke();

    private void ExecuteCameraBehaviour() {
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

    private async void OnPuzzleExit(ExitPuzzleEvent exitPuzzleEvent) {

        if (exitPuzzleEvent.success) {

            Vector3 offset = exitPuzzleEvent.info.puzzlePos.position - (Quaternion.Euler(0, 0, 0) * exitPuzzleEvent.info.puzzlePos.forward * 10);
            Quaternion lookDirection = exitPuzzleEvent.info.puzzlePos.rotation;

            //BORDE INTE GÖRA ROTATIONEN HÄR 
            followTarget.parent.rotation = Quaternion.LookRotation(exitPuzzleEvent.info.puzzlePos.position - followTarget.parent.position);
            
            shoulderPosition.localPosition += shoulderPosition.position.x < exitPuzzleEvent.info.puzzlePos.position.x ? shoulderPosition.right : -shoulderPosition.right;

            LookAtTransition rotationTransition = new LookAtTransition(ref thisTransform, shoulderPosition.position, shoulderPosition.rotation.normalized, .001f, .85f, 100f);

            await PlayTransition(rotationTransition);

            LookAtTransition moveTransition = new LookAtTransition(ref thisTransform, offset, lookDirection, 3f, 1f, .3f);

            await PlayTransition(moveTransition);

        }
            
        Debug.Log("Transition done");
        EventHandler<AwayFromKeyboardEvent>.RegisterListener(OnAwayFromKeyboard);
        ChangeBehaviour(behaviours[typeof(StationaryBehaviour)]);
    }

    private void OnPuzzleStart(StartPuzzleEvent startPuzzleEvent) {
            
        EventHandler<AwayFromKeyboardEvent>.UnregisterListener(OnAwayFromKeyboard);
            
        ChangeBehaviour(behaviours[typeof(PuzzleBaseCameraBehaviour)]);

        PuzzleBaseCameraBehaviour puzzleBaseBehaviour = currentBaseCameraBehaviour as PuzzleBaseCameraBehaviour;

        puzzleBaseBehaviour?.AssignRotation(startPuzzleEvent.info.puzzlePos);
    }
    
    private void ChangeBehaviour(BaseCameraBehaviour newBaseCameraBehaviour) => currentBaseCameraBehaviour = newBaseCameraBehaviour;

    private async Task PlayTransition(CameraTransition transition) {
        
        behaviourQueue = null;
        
        await transition.Transit();

        behaviourQueue = ExecuteCameraBehaviour;
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
