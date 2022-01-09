using UnityEngine;

/// <summary>
/// Used for making the camera passive. 
/// </summary>
[CreateAssetMenu(menuName = "Camera/Game Camera/Null Camera", fileName = "New Null Camera")]
public class NullCamera : GameCamera {
    public override void ExecuteCameraBehaviour() {}
}