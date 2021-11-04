using UnityEngine;

namespace CameraBehaviours {

    [CreateAssetMenu(menuName = "Camera/Camera Behaviours/Idle Rotate Behaviour (no free movement)", fileName = "Idle Rotate Behaviour")]
    public class IdleRotateBehaviour : CameraBehaviour {

        [SerializeField] private float smoothTime;
        [SerializeField] private float maxSpeed;

        protected override void Behave() => transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + followTarget.rotation * offset, ref velocity, smoothTime, maxSpeed);

    }
}