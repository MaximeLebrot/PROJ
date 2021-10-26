using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Puzzle Behaviour (no rotation)", fileName = "Puzzle Behaviour")]
public class PuzzleBehaviour : OffsetCameraBehaviour {
    
    [SerializeField] private Vector3 eulerRotation;
    [SerializeField] private float travelTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;

    private Transform puzzleTransform;

    public void AssignRotation(Transform puzzleTransform) => this.puzzleTransform = puzzleTransform;

    public override void Behave() {
                
        Transform.position = Vector3.SmoothDamp(Transform.position, FollowTarget.position + puzzleTransform.localRotation * Offset, ref Velocity, travelTime, maxSpeed);

        Transform.eulerAngles = puzzleTransform.localEulerAngles + eulerRotation;
    }
}
