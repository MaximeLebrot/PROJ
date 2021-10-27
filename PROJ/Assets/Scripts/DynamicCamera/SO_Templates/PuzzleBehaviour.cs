using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour (no rotation)", fileName = "Puzzle Behaviour")]
public class PuzzleBehaviour : OffsetCameraBehaviour {
    
    [SerializeField] private Vector3 eulerRotation;
    [SerializeField] private float travelTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;

    private Quaternion puzzleRotation;
    
    public void AssignRotation(Quaternion offsetRotation) => puzzleRotation = offsetRotation;
    
    public override void Behave() {
                
        Transform.position = Vector3.SmoothDamp(Transform.position, FollowTarget.position + Offset, ref Velocity, travelTime, maxSpeed);
        
        Transform.eulerAngles = puzzleRotation * eulerRotation;
    }
}
