using UnityEngine;

public class MoveToEvent : CameraTransition<MoveToTransitionData> {
    
    private Vector3 referenceVelocity;
    public MoveToEvent(MoveToTransitionData transitionData, Transform endTarget) : base(transitionData, endTarget) {}
    
    protected override void Transition(Transform transform, Vector3 startPosition, Quaternion startRotation, float animationCurveTime) {
        transform.position = Vector3.SmoothDamp(startPosition, endTarget.position, ref referenceVelocity, transitionData.MoveSpeed);
    }
}
    

