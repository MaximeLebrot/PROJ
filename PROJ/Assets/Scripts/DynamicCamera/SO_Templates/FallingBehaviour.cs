using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Falling Behaviour", fileName = "Falling Behaviour")]
public class FallingBehaviour : OffsetCameraBehaviour {
    
    public override void Behave() {
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref velocity, followSpeed, 300, Time.deltaTime);
    }
}
