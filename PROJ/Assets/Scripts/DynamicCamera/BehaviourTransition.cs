using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Transitions/Behaviour Transition", fileName = "Behaviour Transition")]
public class BehaviourTransition : CameraBehaviour {

    public CameraBehaviour from;
    public CameraBehaviour to;
    
    public override void Behave() {
        throw new System.NotImplementedException();
    }
}
