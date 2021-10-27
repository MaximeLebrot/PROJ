using UnityEngine;

public abstract class OffsetCameraBehaviour : CameraBehaviour {

    [SerializeField] private Vector3 offset;
    
    public Vector3 Offset => offset;
    
}
