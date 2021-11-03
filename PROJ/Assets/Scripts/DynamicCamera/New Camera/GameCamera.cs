using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private CameraBehaviourData cameraBehaviourData;

    private CameraBehaviour currentCameraBehaviour;
    
    [SerializeField] private Transform followTarget;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 clampValues;

    private Vector3 cameraVelocity;
    private Vector2 input;
    private Transform thisTransform;

    private readonly Dictionary<Type, CameraBehaviour> cameraBehaviours = new Dictionary<Type, CameraBehaviour>();

    private void Awake() {

        float dot = Vector3.Dot(followTarget.forward, transform.forward);
        
        
        
        currentCameraBehaviour = new CameraBehaviour(transform, followTarget);
        thisTransform = transform;
        
        cameraBehaviours.Add(typeof(GlideState), new GlideCameraBehaviour(transform, followTarget));
        cameraBehaviours.Add(typeof(WalkState), new CameraBehaviour(transform, followTarget));
        
    }
    
    private void LateUpdate() {
        ReadInput();

        input = currentCameraBehaviour.ClampMovement(input, clampValues);
        
        Vector3 calculatedOffset = currentCameraBehaviour.ExecuteCollision(input, offset, cameraBehaviourData);
        
        thisTransform.position = currentCameraBehaviour.ExecuteMove(calculatedOffset, cameraFollowSpeed);
        thisTransform.rotation = currentCameraBehaviour.ExecuteRotate();
    }
    
    private void ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -inputDirection.y * cameraBehaviourData.MouseSensitivity;
        input.y += inputDirection.x * cameraBehaviourData.MouseSensitivity;
        
    }


    private void OnEnable() {
        EventHandler<PlayerStateChangeEvent>.RegisterListener((e) => currentCameraBehaviour = cameraBehaviours[typeof(GlideState)]);
        EventHandler<PlayerStateChangeEvent>.RegisterListener((e) => currentCameraBehaviour = cameraBehaviours[typeof(WalkState)]);
    }
}
