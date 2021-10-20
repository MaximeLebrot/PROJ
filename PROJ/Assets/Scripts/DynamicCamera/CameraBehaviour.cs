using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {

    protected Transform followTarget;
    protected Transform transform;
    public void AssignTarget(Transform newTarget) => followTarget = newTarget;

    public void Initialize(Transform objectTransform) => transform = objectTransform;
    
    public abstract void Behave();

}
