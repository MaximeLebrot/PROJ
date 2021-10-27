using UnityEngine;

public abstract class MovementBehaviour : OffsetCameraBehaviour
{
    [SerializeField] protected Vector2 clampValues;
    [SerializeField] protected float mouseSensitivity;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float collisionRadius;
    [SerializeField] private LayerMask collisionMask;
    
    protected Vector2 input;
    
    public override void Behave() {
        ReadInput();

        ClampInput();
        RotateCamera();
        MoveCamera();
    }

    protected virtual void ClampInput() => input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);

    private void ReadInput() {
        Vector2 cameraInputThisFrame = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x -= cameraInputThisFrame.y * mouseSensitivity;
        input.y += cameraInputThisFrame.x * mouseSensitivity;
    }
    
    protected virtual void RotateCamera() {
        followTarget.rotation = Quaternion.Lerp(followTarget.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
        transform.rotation = followTarget.localRotation;
    }

    protected virtual void MoveCamera() {

        Vector3 collisionOffset = transform.rotation * offset;

        collisionOffset = Collision(collisionOffset);

        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + collisionOffset, ref velocity, followSpeed, 300, Time.deltaTime);
    }

    protected virtual Vector3 Collision(Vector3 cameraOffset) {

        if (Physics.SphereCast(followTarget.position, collisionRadius, cameraOffset.normalized, out var hitInfo, cameraOffset.magnitude, collisionMask))
            cameraOffset = cameraOffset.normalized * hitInfo.distance;

        return cameraOffset;
    }

    private void CastWhisker() {

        Debug.DrawRay(followTarget.position, transform.position- followTarget.position, Color.green);

        Vector3 direction1 = transform.rotation * new Vector3(Mathf.Cos(25 * Mathf.Deg2Rad), 0, Mathf.Sin(25 * Mathf.Deg2Rad));
        Vector3 direction2 = transform.rotation * new Vector3(Mathf.Cos(45 * Mathf.Deg2Rad), 0, Mathf.Sin(45 * Mathf.Deg2Rad));

        Debug.DrawRay(followTarget.position, (transform.position- followTarget.position) + direction1, Color.red);
        Debug.DrawRay(followTarget.position, (transform.position- followTarget.position) + direction2, Color.yellow);
    }
    
}
