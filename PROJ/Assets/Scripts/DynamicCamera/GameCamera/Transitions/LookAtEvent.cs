using UnityEngine;

public class LookAtEvent : CameraTransition<LookAtTransitionData> {

    public LookAtEvent(LookAtTransitionData transitionData, Transform endTarget) : base(transitionData, endTarget) { }
    
    protected override void Transition(Transform transform, Vector3 startPosition, Quaternion startRotation, float animationCurveTime) {
        
        Quaternion lookRotation = Quaternion.LookRotation((endTarget.position - transform.position).normalized);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, transitionData.LookAtBehavior.Evaluate(animationCurveTime));
    }
}
