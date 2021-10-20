using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {

    public Transform target;

    public void AssignTarget(Transform newTarget) => target = newTarget;
    
    public abstract void Behave();

}
