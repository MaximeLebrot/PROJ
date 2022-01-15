using System;
using System.Collections.Generic;
using NewCamera;
using UnityEngine;

public abstract class GameCamera : ScriptableObject {

    [SerializeField] private BaseCameraBehaviour defaultCameraBehavior;
    [Space(10)]
    [SerializeField] private List<BaseCameraBehaviour> wantedBehaviors;
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private GlobalCameraSettings globalCameraSettings;
    
    private Dictionary<Type, BaseCameraBehaviour> cameraBehaviors;

    private BaseCameraBehaviour currentCameraBehavior;

    protected Transform Transform;
    
    public virtual void Initialize(Transform transform, Transform pivotTarget, Transform character) {
        
        cameraBehaviors = new Dictionary<Type, BaseCameraBehaviour>();
        
        this.Transform = transform;

        foreach (BaseCameraBehaviour behavior in wantedBehaviors) {
            behavior.InjectReferences(transform, pivotTarget, character);
            cameraBehaviors.Add(behavior.GetType(), behavior);
        }
        
        defaultCameraBehavior.InjectReferences(transform, pivotTarget, character);
        cameraBehaviors.Add(defaultCameraBehavior.GetType(), defaultCameraBehavior);
    }

    
    public void ResetCamera() => currentCameraBehavior = defaultCameraBehavior;
    
    /// <summary>
    /// Runs this cameras logic. Call in Update/LateUpdate
    /// </summary>
    public virtual void ExecuteCameraBehaviour() {
        
        CustomInput input = ReadInput();
        
        currentCameraBehavior.ManipulatePivotTarget(input);
        
        Vector3 calculatedOffset = currentCameraBehavior.ExecuteCollision(globalCameraSettings);
        
        Transform.position = currentCameraBehavior.ExecuteMove(calculatedOffset);
        Transform.rotation = currentCameraBehavior.ExecuteRotate();
    }
    
    
    /// <summary>
    /// Use to switch from one behavior to another without injecting references
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public T ChangeBehavior<T>() where T : BaseCameraBehaviour {
        currentCameraBehavior = cameraBehaviors[typeof(T)];
        currentCameraBehavior.EnterBehaviour();
        return currentCameraBehavior as T;
    }
    
    
    private CustomInput ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();

        CustomInput input = new CustomInput();
        
        input.aim.x = -inputDirection.y * MouseSensitivity.Sensitivity;
        input.aim.y = inputDirection.x * MouseSensitivity.Sensitivity;
        
        input.movement = inputReference.InputMaster.Movement.ReadValue<Vector2>();
        
        return input;
    }
}

public struct CustomInput {
    public Vector2 aim;
    public Vector2 movement;
}