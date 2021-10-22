using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour (no rotation)", fileName = "Puzzle Behaviour")]
public class PuzzleBehaviour : CameraBehaviour {
    
    [SerializeField] private Vector3 eulerRotation;
    [SerializeField] private float travelTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    
    private Vector3 velocity;
    
    public override void Behave() {
                
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref velocity, travelTime, maxSpeed);
        
        transform.eulerAngles = eulerRotation;
    }
}
