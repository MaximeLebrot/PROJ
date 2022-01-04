using UnityEngine;

public class LookAndMoveTransition : CameraTransition<LookAndMoveTransitionData> {
   
    private Vector3 referenceVelocity;
    
    public LookAndMoveTransition(LookAndMoveTransitionData transitionData, Transform endTarget) : base(transitionData, endTarget) {}
    
    protected override void Transition(Transform transform, Vector3 startPosition, Quaternion startRotation, float animationCurveValue) {
        
        transform.position = Vector3.Slerp(startPosition, endTarget.position, transitionData.MoveBehavior.Evaluate(animationCurveValue));
        transform.rotation = Quaternion.Slerp(startRotation, endTarget.rotation, transitionData.LookBehavior.Evaluate(animationCurveValue));
    }
}
