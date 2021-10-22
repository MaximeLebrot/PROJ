using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {
    
    [SerializeField] protected Vector3 offset;
    protected Transform followTarget;
    protected Transform transform;
    public void AssignTarget(Transform newTarget) => followTarget = newTarget;

    public void Initialize(Transform objectTransform) => transform = objectTransform;

    public virtual void Behave() {}

    public virtual async Task BehaveAsync() => await Task.Yield();

    public virtual Vector3 GetTransitPosition() => transform.position + offset;

}
