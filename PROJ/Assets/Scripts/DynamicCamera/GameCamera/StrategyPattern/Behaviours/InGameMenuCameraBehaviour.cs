using NewCamera;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Behaviours/In game Menu Camera Behaviour", fileName = "In game Menu Camera Behaviour")]
public class InGameMenuCameraBehaviour : BaseCameraBehaviour {

    private Quaternion menuRotation;
    
    public override void EnterBehaviour() => menuRotation = Quaternion.Inverse(Quaternion.LookRotation(pivotTarget.position - thisTransform.position));

    /*public override Quaternion ExecuteRotate() {
        
        Quaternion targetRotation = Quaternion.LookRotation(pivotTarget.position - thisTransform.position);

        Vector3 euler = targetRotation.eulerAngles;

        euler.x = 0;
        euler.z = 0;
        
        return Quaternion.Lerp(thisTransform.rotation, Quaternion.Euler(euler) * Quaternion.Euler(BehaviourData<MenuData>().EulerRotation), Time.deltaTime * behaviourValues.RotationSpeed);
    }*/

    /*public override void ManipulatePivotTarget(CustomInput input) {
        pivotTarget.rotation = menuRotation;
    }*/
}
