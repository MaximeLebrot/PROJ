using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Glide Behaviour", fileName = "Glide Behaviour")]
public class GlideBehaviour : MovementBehaviour {

    [SerializeField] private Vector2 horizontalClamp;
    
    protected override void ClampInput() {
        base.ClampInput();

        input.y = Mathf.Clamp(input.y, horizontalClamp.x, horizontalClamp.y);
    }
}
