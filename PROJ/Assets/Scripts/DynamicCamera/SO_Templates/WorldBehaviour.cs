using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/World Behaviour", fileName = "World Behaviour")]
public class WorldBehaviour : CameraBehaviour {
    
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector2 clampValues;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] [Tooltip("The lower the value the faster the camera moves")] private float travelTime;
    
    private Vector2 input;
    private Vector3 velocity;
    
    private InputMaster inputMaster;
    
    private void OnEnable() {
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    public override void Behave() {
        GetInput();
        MoveCamera();
        RotateCamera();
    }
    private void GetInput() {
        input.x -= inputMaster.Player.MoveCamera.ReadValue<Vector2>().y * mouseSensitivity;
        input.y += inputMaster.Player.MoveCamera.ReadValue<Vector2>().x * mouseSensitivity;

        input.x = Mathf.Clamp(input.x, clampValues.x, clampValues.y);
    }

    
    private void RotateCamera() {
        followTarget.rotation = Quaternion.Lerp(followTarget.rotation, Quaternion.Euler(input.x, input.y, 0), rotationSpeed * Time.deltaTime);
        transform.rotation = followTarget.localRotation;
    }

    private void MoveCamera() {
        
        Vector3 collisionOffset = transform.rotation * offset;
        
        collisionOffset = Collision(collisionOffset);
        
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + collisionOffset, ref velocity, travelTime, 100, Time.deltaTime);
            
    }

    private Vector3 Collision(Vector3 cameraOffset) {

        if (Physics.SphereCast(followTarget.position, collisionRadius, cameraOffset.normalized, out var hitInfo, cameraOffset.magnitude, collisionMask)) 
            cameraOffset = cameraOffset.normalized * hitInfo.distance;
        
        return cameraOffset;
    }
    
    
}
