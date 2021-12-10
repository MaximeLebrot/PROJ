
using NewCamera;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Camera/Camera Behaviours/One Hand Camera Behaviour", fileName = "One Hand Camera Behaviour")]
public class OneHandCameraBehaviour : BaseCameraBehaviour {
    
    public override void ManipulatePivotTarget(CustomInput input) {
        //If no input or movement.x is 0 OR lower than input dead zone (for controllers). 
        if (input.aim == Vector2.zero && (input.movement.x == 0 || Mathf.Abs(input.movement.x) < behaviourValues.InputDeadZone)) {
            target.rotation = previousRotation;
            return;
        }
            
        Vector3 desiredRotation  = target.eulerAngles + (Vector3)input.aim;
            
        if (desiredRotation.x > 180)
            desiredRotation.x -= 360;
        if (desiredRotation.y > 180)
            desiredRotation.y -= 360;
            
        desiredRotation.x = Mathf.Clamp(desiredRotation.x, behaviourValues.ClampValues.x, behaviourValues.ClampValues.y);
            
        target.eulerAngles = desiredRotation;
        previousRotation = target.rotation;
    }
}
