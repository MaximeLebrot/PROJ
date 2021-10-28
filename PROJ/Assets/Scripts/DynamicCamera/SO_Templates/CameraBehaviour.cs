using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {
    
    [SerializeField] protected ControllerInputReference inputReference;
    protected Transform followTarget;
    protected Transform transform;
    protected Vector3 velocity;

    public virtual void Initialize(Transform objectTransform, Transform target) {
        transform = objectTransform;
        followTarget = target;
    }

    public virtual void Behave() {}

    public virtual async Task BehaveAsync() => await Task.Yield();
    
}
