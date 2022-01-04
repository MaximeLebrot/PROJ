using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Transitions/Look AND Move/Look AND Move Transition Data")]  
public class LookAndMoveTransitionData : TransitionData {

    [SerializeField] private AnimationCurve lookBehavior;
    [SerializeField] private AnimationCurve moveBehavior;
  
    public AnimationCurve MoveBehavior => moveBehavior;
    public AnimationCurve LookBehavior => lookBehavior;
    
}
