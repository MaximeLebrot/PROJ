using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Behaviour", fileName = "Idle Behaviour")]
public class IdleBehaviour : CameraBehaviour {

    [SerializeField] private CameraBehaviour worldBehaviour;
    [SerializeField] private float smoothTime;
    [SerializeField] private float maxSpeed;
    
    public override void Behave() => transform.position = Vector3.SmoothDamp(transform.position, worldBehaviour.offset, ref velocity, smoothTime, maxSpeed);
    
}

