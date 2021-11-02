using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Recenter Behaviour", fileName = "Recenter Behaviour")]
public class RecenterBehaviour : CameraBehaviour {
    protected override void RotateCamera() {
        followTarget.eulerAngles  = followTarget.parent.rotation * offset;
        base.RotateCamera();
    }
}
