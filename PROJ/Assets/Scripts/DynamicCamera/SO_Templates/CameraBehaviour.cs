using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/World Behaviour", fileName = "World Behaviour")]
public abstract class CameraBehaviour : ScriptableObject {

    public Vector3 offset;
    
    protected Transform followTarget;
    protected Transform transform;
    protected Vector3 velocity;
    
    public void AssignTarget(Transform newTarget) => followTarget = newTarget;

    public void Initialize(Transform objectTransform) => transform = objectTransform;

    public virtual void Behave() {}

    public virtual async Task BehaveAsync() => await Task.Yield();

    public virtual Vector3 GetTransitPosition() => transform.position + offset;

}
