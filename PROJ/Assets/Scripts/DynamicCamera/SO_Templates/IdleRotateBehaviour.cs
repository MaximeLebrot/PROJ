using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Rotate Behaviour (no free movement)", fileName = "Idle Rotate Behaviour")]
public class IdleRotateBehaviour : OffsetCameraBehaviour {
    
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    
    public override void Behave() => transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + followTarget.rotation * offset, ref velocity, smoothTime, maxSpeed);

}
