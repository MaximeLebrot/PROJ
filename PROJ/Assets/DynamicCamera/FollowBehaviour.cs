using UnityEngine;

public class FollowBehaviour : CameraBehaviour {

    private const string XRotationInputName = "Mouse Y";
    private const string YRotationInputName = "Mouse X";
    
    private Vector2 input;
    
    private readonly Vector3 offset;
    private readonly float mouseSensitivity;
    private readonly float speed;
    
    
    public FollowBehaviour(Vector3 offset, float mouseSensitivity, float speed) {
        this.mouseSensitivity = mouseSensitivity;
        this.offset = offset;
        this.speed = speed;
    }
    
    public override void Behave(Transform cameraTransform, Transform target) {
        GetInput();
        RotateCamera(cameraTransform);
        MoveCamera(cameraTransform, target);
    }
    
    
    private void GetInput() {
        input.x -= Input.GetAxis(XRotationInputName) * mouseSensitivity * Time.deltaTime;
        input.y += Input.GetAxis(YRotationInputName) * mouseSensitivity * Time.deltaTime;
    }
        
    private void RotateCamera(Transform transform) => transform.rotation = Quaternion.Euler(input.x, input.y, 0);

    private void MoveCamera(Transform transform, Transform target) {
        Vector3 offsetPosition = transform.rotation * offset;
        
        transform.position = Vector3.Slerp(transform.position, target.position + offsetPosition, speed * Time.deltaTime);
    }
}
