
using NewCamera;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Camera/Camera Behaviours/One Hand Camera Behaviour", fileName = "One Hand Camera Behaviour")]
public class OneHandCameraBehaviour : BaseCameraBehaviour {

    public override void ManipulatePivotTarget(CustomInput input) {

        //Musinput
        if (input.aim != Vector2.zero) {
            Vector3 desiredRotation = target.eulerAngles + (Vector3)input.aim;

            if (desiredRotation.x > 180)
                desiredRotation.x -= 360;
            if (desiredRotation.y > 180)
                desiredRotation.y -= 360;

            desiredRotation.x = Mathf.Clamp(desiredRotation.x, behaviourValues.ClampValues.x, behaviourValues.ClampValues.y);

            target.eulerAngles = desiredRotation;
            previousRotation = target.rotation;
        }
        //Diagonal rörelse
        else if (input.movement.y != 0 && input.movement.x != 0) {
            target.rotation = Quaternion.Slerp(target.rotation, target.parent.rotation, Time.deltaTime / behaviourValues.InputDeadZone);
            previousRotation = target.rotation;
        }
        //Bara framåt rörelse, behåll pivotrotationen
        else if (input.movement.y != 0 && input.movement.x == 0)
            target.rotation = previousRotation;
        //Sidledsrörelse, rotera pivotrotation lite varje frame
        else if (input.movement.y == 0 && input.movement.x != 0) {
            target.rotation = Quaternion.Lerp(target.rotation, target.parent.rotation, 0.0000001f);
            previousRotation = target.rotation;
        }
        else {
            target.rotation = previousRotation;
        }

    }
}
