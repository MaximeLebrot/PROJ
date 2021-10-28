using UnityEngine;

public abstract class OffsetCameraBehaviour : CameraBehaviour {

    [SerializeField] protected Vector3 offset;
    [Tooltip("The lower the value the faster the camera moves")]
    [SerializeField] protected float followSpeed;
    
}
