using UnityEngine;


/// <summary>
/// Used for moving the camera without rotation
/// </summary>
[CreateAssetMenu(menuName = "Camera/Camera Transitions/Move To/Move To Event Data")]
public class MoveToEventData : EventData {
    
    [Tooltip("moveTargetOffset is just an offset, if SetOrigin is NOT set the camera will move to this world position")]
    [Header("READ TOOLTIP")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed;
    
    public Vector3 Offset => offset;
    public float MoveSpeed => moveSpeed;
    
}
