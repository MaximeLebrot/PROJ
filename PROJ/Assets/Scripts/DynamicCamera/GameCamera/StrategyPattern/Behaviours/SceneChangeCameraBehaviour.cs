using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Scene Change Camera Behaviour", fileName = "Scene Change Camera Behaviour")]
public class SceneChangeCameraBehaviour : BaseCameraBehaviour {

    private Transform m;
    
    public override void EnterBehaviour() {
        pivotTarget.localRotation = Quaternion.Euler(0,0,0);
        m = FindObjectOfType<CharacterModel>().transform;
    }

    public override void ManipulatePivotTarget(CustomInput input) {
        pivotTarget.rotation = m.rotation;
    }
}
