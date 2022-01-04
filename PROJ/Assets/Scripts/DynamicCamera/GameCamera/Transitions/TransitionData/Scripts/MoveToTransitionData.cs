using UnityEngine;

/// <summary>
/// Used for moving the camera without rotation
/// </summary>
[CreateAssetMenu(menuName = "Camera/Camera Transitions/Move To/Move To Event Data")]
public class MoveToTransitionData : TransitionData {
    
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed;
    
    public Vector3 Offset => offset;
    public float MoveSpeed => moveSpeed;

    
}
