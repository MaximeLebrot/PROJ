using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Behaviour", fileName = "Idle Behaviour")]
public class IdleBehaviour : CameraBehaviour {

    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float xRotation;
    
    protected override void Behave() {
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + followTarget.rotation * offset, ref velocity, smoothTime, maxSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation * Quaternion.Euler(xRotation, 0, 0), Time.deltaTime);
    }
}

