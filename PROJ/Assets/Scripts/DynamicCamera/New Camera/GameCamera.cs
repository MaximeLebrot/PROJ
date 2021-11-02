using UnityEditor;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private CameraBehaviourData cameraBehaviourData;
    
    
    [SerializeField] private Transform followTarget;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 xClamp;

    private Vector3 cameraVelocity;

    private Vector2 input;
    
    private void LateUpdate() {

        ReadInput();

        input.x = Mathf.Clamp(input.x, xClamp.x, xClamp.y);
        
        Vector3 calculatedOffset = Collision();
        
        Move(calculatedOffset);
        Rotate();
    }

    private void Move(Vector3 calculatedOffset) {
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + calculatedOffset, ref cameraVelocity, cameraFollowSpeed);
    }

    private void Rotate() {

        Vector3 direction = followTarget.position - transform.position;
        
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }


    private Vector3 Collision() {

        followTarget.rotation = Quaternion.Euler(input.x, input.y, 0);
        
        Vector3 collisionOffset = followTarget.rotation * offset;
        
        if (Physics.SphereCast(followTarget.position, cameraBehaviourData.CollisionRadius, collisionOffset.normalized, out var hitInfo, collisionOffset.magnitude, cameraBehaviourData.CollisionMask))
            collisionOffset = collisionOffset.normalized * hitInfo.distance;

        return collisionOffset;
    }

    private void ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -inputDirection.y * cameraBehaviourData.MouseSensitivity;
        input.y += inputDirection.x * cameraBehaviourData.MouseSensitivity;
        
    }
    

}
