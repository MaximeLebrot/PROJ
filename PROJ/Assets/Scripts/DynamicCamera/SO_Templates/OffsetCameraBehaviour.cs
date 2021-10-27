using UnityEngine;

public abstract class OffsetCameraBehaviour : CameraBehaviour {

    [SerializeField] private Vector3 offset;
    [Tooltip("The lower the value the faster the camera moves")]
    [SerializeField] private float followSpeed;
    public Vector3 Offset => offset;
    public float FollowSpeed => followSpeed;
    
}
