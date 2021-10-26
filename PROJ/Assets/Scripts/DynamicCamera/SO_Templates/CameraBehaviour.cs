using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {

    protected Transform FollowTarget;
    protected Transform Transform;
    protected Vector3 Velocity;
    
    public void Initialize(Transform objectTransform, Transform target) {
        Transform = objectTransform;
        FollowTarget = target;
    }

    public virtual void Behave() {}

    public virtual async Task BehaveAsync() => await Task.Yield();
    
}
