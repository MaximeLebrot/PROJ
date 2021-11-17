using NewCamera;
using UnityEngine;
[System.Serializable]
public class OneSwitchCameraBehaviour : BaseCameraBehaviour
{
    public OneSwitchCameraBehaviour(Transform transform, Transform target, BehaviourData behaviourValues) : base(transform, target, behaviourValues) { }
    
    public override void ManipulatePivotTarget(CustomInput input) {
        target.rotation = target.parent.rotation;
    }
}
