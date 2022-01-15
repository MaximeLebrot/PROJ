using System;
using NewCamera;
using UnityEngine;


/// <summary>
/// Null object design pattern, used for loading a GameCamera with a passive behavior that does nothing.
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "Camera/Camera Behaviours/Null Behaviour", fileName = "Null Behaviour")]
public class NullCameraBehaviour : BaseCameraBehaviour {
    
    public override void EnterBehaviour() { }

    public override Vector3 ExecuteCollision(GlobalCameraSettings data) => pivotTarget.rotation * behaviourValues.Offset;

    public override Quaternion ExecuteRotate() => Quaternion.identity;

    public override Vector3 ExecuteMove(Vector3 calculatedOffset) => thisTransform.position;
}
