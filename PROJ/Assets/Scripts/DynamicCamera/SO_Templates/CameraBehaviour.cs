using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {
    
    [SerializeField] protected ControllerInputReference inputReference;
    protected Transform followTarget;
    protected Transform transform;
    protected Vector3 velocity;
    [SerializeField]protected DynamicCamera.DynamicCamera thisCamera;

    public virtual void Initialize(Transform objectTransform, Transform target) {
        transform = objectTransform;
        followTarget = target;
        followTarget.localRotation = Quaternion.Euler(Vector3.zero);

    }

    public virtual void Behave() {}

    public virtual async Task BehaveAsync() => await Task.Yield();




    public Vector2 input;
    public Vector2 GetInput()
    {
        return input;
    }
    public virtual void SetInput(Vector2 input)
    {
        this.input = input;
    }

}
