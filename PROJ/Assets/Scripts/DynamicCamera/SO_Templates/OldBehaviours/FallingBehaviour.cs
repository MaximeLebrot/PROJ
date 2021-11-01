using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Falling Behaviour", fileName = "Falling Behaviour")]
public class FallingBehaviour : CameraBehaviour {

    [SerializeField] private float rotationSpeed;

    private Quaternion rot;
    
    protected override void Behave() {
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref velocity, cameraMovementSpeed, 300, Time.deltaTime);
        
        Quaternion direction = quaternion.LookRotation(followTarget.position - transform.position, Vector3.up);
        
    }
    

}
