using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/World Behaviour", fileName = "World Behaviour")]
public class WorldBehaviour : CameraBehaviour {
    
    [SerializeField] private Vector3 offset;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector2 clampValues;
    [SerializeField] private float rotationSpeed;
        
    [SerializeField]
    [Tooltip("The lower the value the faster the camera moves")] 
    private float travelTime;
    private Vector2 input;
    private Vector3 velocity;
    
    private InputMaster inputMaster;

    private void OnEnable() {
        inputMaster = new InputMaster();
        inputMaster.Enable();
    }

    public override void Behave() {
        GetInput();
        RotateCamera();
        MoveCamera();
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
            
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + collisionOffset, ref velocity, travelTime, 100, Time.deltaTime);
            
    }
}
