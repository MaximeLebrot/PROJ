using System.Threading.Tasks;
using UnityEngine;

public abstract class CameraBehaviour : ScriptableObject {
    
    [SerializeField] protected ControllerInputReference inputReference;
    [SerializeField] protected CameraBehaviourData cameraBehaviourData;
    [SerializeField] protected Vector3 offset;
    [SerializeField] protected Vector2 clampValues;
    [SerializeField] protected float cameraMovementSpeed;
    [SerializeField] protected float rotationSpeed;

    protected Transform followTarget;
    protected Vector3 velocity;
    protected Transform transform;


    private Vector2 input;
    
    public virtual void Initialize(Transform objectTransform, Transform target) {
        transform = objectTransform;
        followTarget = target;
        
    }
    
    public void ExecuteBehaviour() {
        ReadInput();
        ClampInput();
        Behave();
    }

    protected virtual void Behave() {
        SmoothCollisionMovement();
        RotateCamera();
    }

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
    
    protected virtual void RotateCamera() {

        /*Vector3 temp = transform.rotation.eulerAngles;
        temp.z = 0;
        Quaternion targetRotation = Quaternion.Euler(temp);*/
        
        //Mulitply Euler with targetRotation
        
        followTarget.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
        transform.rotation = followTarget.rotation;
    }
    
    protected virtual void ClampInput() => input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);


    
    private void ReadInput() {
        Vector2 cameraInputThisFrame = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -cameraInputThisFrame.y * cameraBehaviourData.MouseSensitivity;
        input.y += cameraInputThisFrame.x * cameraBehaviourData.MouseSensitivity;
        
    }
}
