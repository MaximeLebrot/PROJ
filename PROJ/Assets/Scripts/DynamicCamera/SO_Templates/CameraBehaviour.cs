using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {
    
    [SerializeField] protected ControllerInputReference inputReference;
    [SerializeField] protected CameraBehaviourData cameraBehaviourData;
    
    protected Transform followTarget;
    protected Transform transform;
    protected Vector3 velocity;
    protected Vector3 offset;
    
    protected float cameraMovementSpeed;

    protected Vector2 input;
    
    public virtual void Initialize(Transform objectTransform, Transform target) {
        transform = objectTransform;
        followTarget = target;
        
    }
    
    public void ExecuteBehaviour() {
        ReadInput();
        Behave();
    }

    protected virtual void Behave() {}

    protected virtual async Task BehaveAsync() => await Task.Yield();

    protected void SmoothCollisionMovement() {

        Vector3 collisionOffset = transform.rotation * offset;

        collisionOffset = Collision(collisionOffset);

        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + collisionOffset, ref velocity, cameraMovementSpeed, 300, Time.deltaTime);
    }

    protected virtual Vector3 Collision(Vector3 cameraOffset) {

        if (Physics.SphereCast(followTarget.position, cameraBehaviourData.CollisionRadius, cameraOffset.normalized, out var hitInfo, cameraOffset.magnitude, cameraBehaviourData.CollisionMask))
            cameraOffset = cameraOffset.normalized * hitInfo.distance;

        return cameraOffset;
    }
    
    private void ReadInput() {
        Vector2 cameraInputThisFrame = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();

        input.x = -cameraInputThisFrame.y * cameraBehaviourData.MouseSensitivity;
        input.y = cameraInputThisFrame.x * cameraBehaviourData.MouseSensitivity;
    }
}
