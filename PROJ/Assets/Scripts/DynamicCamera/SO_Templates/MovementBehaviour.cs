using UnityEngine;

public abstract class MovementBehaviour : OffsetCameraBehaviour
{
    [SerializeField] protected Vector2 clampValues;
    [SerializeField] protected float mouseSensitivity;
    [SerializeField] protected float rotationSpeed;
    
    protected override void Behave() {
        ClampInput();
        RotateCamera();
        SmoothCollisionMovement();
    }

    protected virtual void ClampInput() => input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);


 //musdelta varje frame f�r att generera en LITEN ro
    protected virtual void RotateCamera() {

        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = 0;
        Quaternion targetRotation = Quaternion.Euler(temp);

        followTarget.rotation = Quaternion.Lerp(transform.rotation, targetRotation * Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
        transform.rotation = followTarget.rotation;
    }
    
}
