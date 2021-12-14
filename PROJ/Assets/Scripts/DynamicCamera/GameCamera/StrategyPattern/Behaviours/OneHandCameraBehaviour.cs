
using NewCamera;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Camera/Camera Behaviours/One Hand Camera Behaviour", fileName = "One Hand Camera Behaviour")]
public class OneHandCameraBehaviour : BaseCameraBehaviour {

    public override void ManipulatePivotTarget(CustomInput input) {

        if (input.movement.x != 0) {
            target.eulerAngles = target.rotation * input.movement;
        }

        Vector3 xRotation = new Vector3(10, target.eulerAngles.y, 0);
        
        target.eulerAngles = Vector3.Lerp(target.eulerAngles , xRotation, Time.deltaTime);
        previousRotation = target.rotation;

    }
}
