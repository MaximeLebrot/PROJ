using UnityEngine;


[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Stationary Behaviour", fileName = "Stationary Behaviour")]
public class StationaryBehaviour : OffsetCameraBehaviour {

    protected override void Behave() {
        SmoothCollisionMovement();
    }
    
}
