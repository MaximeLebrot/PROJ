using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Scene Change Camera Behaviour", fileName = "Scene Change Camera Behaviour")]
public class SceneChangeCameraBehaviour : BaseCameraBehaviour {
    public override void ManipulatePivotTarget(CustomInput input) {
        pivotTarget.rotation = pivotTarget.parent.rotation;
    }
}
