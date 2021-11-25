using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Behaviour Data/Idle Behaviour Data", fileName = "New Idle Behaviour Data")]
public class IdleBehaviourData : BehaviourData {

    [SerializeField] private AnimationCurve rotationCurve;

    public AnimationCurve RotationCurve => rotationCurve;

}
