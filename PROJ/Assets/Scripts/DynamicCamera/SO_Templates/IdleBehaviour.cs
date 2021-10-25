using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Behaviour", fileName = "Idle Behaviour")]
public class IdleBehaviour : CameraBehaviour {

    [SerializeField] private OffsetCameraBehaviour worldBehaviour;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    
    public override void Behave() => Transform.position = Vector3.SmoothDamp(Transform.position, worldBehaviour.Offset, ref Velocity, smoothTime, maxSpeed);
    
}

