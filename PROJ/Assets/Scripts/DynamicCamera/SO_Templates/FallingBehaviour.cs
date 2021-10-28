using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Falling Behaviour", fileName = "Falling Behaviour")]
public class FallingBehaviour : OffsetCameraBehaviour {
    
    public override void Behave() {
        Transform.position = Vector3.SmoothDamp(Transform.position, FollowTarget.position + Offset, ref Velocity, FollowSpeed, 300, Time.deltaTime);
    }
}
