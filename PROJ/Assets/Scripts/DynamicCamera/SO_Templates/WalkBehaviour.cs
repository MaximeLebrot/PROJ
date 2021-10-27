using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Walk Behaviour", fileName = "Walk Behaviour")]
public class WalkBehaviour : OffsetCameraBehaviour {

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector2 clampValues;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] [Tooltip("The lower the value the faster the camera moves")]
    private float targetFollowSpeed;

    private Vector2 input;
    
    public override void Behave() {
        ReadInput();
        RotateCamera();
        MoveCamera();
    }
    
    private void ReadInput() {
        Vector2 cameraInputThisFrame = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x -= cameraInputThisFrame.y * mouseSensitivity;
        input.y += cameraInputThisFrame.x * mouseSensitivity;
        
        input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);
        
    }


    private void RotateCamera() {
        FollowTarget.rotation = Quaternion.Lerp(FollowTarget.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
        Transform.rotation = FollowTarget.localRotation;
    }

    private void MoveCamera() {

        Vector3 collisionOffset = Transform.rotation * Offset;

        collisionOffset = Collision(collisionOffset);

        Transform.position = Vector3.SmoothDamp(Transform.position, FollowTarget.position + collisionOffset, ref Velocity, targetFollowSpeed, 300, Time.deltaTime);

    }

    private Vector3 Collision(Vector3 cameraOffset) {

        if (Physics.SphereCast(FollowTarget.position, collisionRadius, cameraOffset.normalized, out var hitInfo, cameraOffset.magnitude, collisionMask))
            cameraOffset = cameraOffset.normalized * hitInfo.distance;

        return cameraOffset;
    }

    private void CastWhisker() {

        Debug.DrawRay(FollowTarget.position, Transform.position- FollowTarget.position, Color.green);

        Vector3 direction1 = Transform.rotation * new Vector3(Mathf.Cos(25 * Mathf.Deg2Rad), 0, Mathf.Sin(25 * Mathf.Deg2Rad));
        Vector3 direction2 = Transform.rotation * new Vector3(Mathf.Cos(45 * Mathf.Deg2Rad), 0, Mathf.Sin(45 * Mathf.Deg2Rad));

        Debug.DrawRay(FollowTarget.position, (Transform.position- FollowTarget.position) + direction1, Color.red);
        Debug.DrawRay(FollowTarget.position, (Transform.position- FollowTarget.position) + direction2, Color.yellow);
    }
}
