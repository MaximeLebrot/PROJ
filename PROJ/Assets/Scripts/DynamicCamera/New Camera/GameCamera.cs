using UnityEditor;
using UnityEngine;



public class GameCamera : MonoBehaviour {
    
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private CameraBehaviourData cameraBehaviourData;

    private CameraBehaviour cameraBehaviour;
    
    [SerializeField] private Transform followTarget;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector2 xClamp;

    private Vector3 cameraVelocity;

    private Vector2 input;
    private Transform thisTransform;

    private void Awake() {
        cameraBehaviour = new CameraBehaviour(transform, followTarget);
        thisTransform = transform;
    }
    
    private void LateUpdate() {

        ReadInput();

        input.x = Mathf.Clamp(input.x, xClamp.x, xClamp.y);

        
        Vector3 calculatedOffset = cameraBehaviour.ExecuteCollision(input, offset, cameraBehaviourData);
        
        thisTransform.position = cameraBehaviour.ExecuteMove(offset, cameraFollowSpeed);
        thisTransform.rotation = cameraBehaviour.ExecuteRotate();
    }
    


    private void ReadInput() {
        Vector2 inputDirection = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        input.x += -inputDirection.y * cameraBehaviourData.MouseSensitivity;
        input.y += inputDirection.x * cameraBehaviourData.MouseSensitivity;
        
    }
    

}
