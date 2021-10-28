using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Glide Behaviour", fileName = "Glide Behaviour")]
public class GlideBehaviour : MovementBehaviour {

    [SerializeField] private Vector2 horizontalClamp;
    protected override void ClampInput() {
        base.ClampInput();

        input.y = Mathf.Clamp(input.y, transform.forward.x + horizontalClamp.x, transform.forward.y + horizontalClamp.y);
    }

    protected override void RotateCamera() {
        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.parent.rotation, rotationSpeed * Time.deltaTime);
    }
    
}
