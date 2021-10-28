using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Behaviour", fileName = "Idle Behaviour")]
public class IdleBehaviour : OffsetCameraBehaviour {

    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float xRotation;
    
    
    public override void Behave() {
        Transform.position = Vector3.SmoothDamp(Transform.position, FollowTarget.position + FollowTarget.rotation * Offset, ref Velocity, smoothTime, maxSpeed);
        Transform.rotation = Quaternion.Lerp(Transform.rotation, FollowTarget.rotation * Quaternion.Euler(xRotation, 0, 0), Time.deltaTime);

    }
}

