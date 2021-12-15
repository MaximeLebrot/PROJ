using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSWalkState")]
public class OSWalkState : PlayerState
{
    private Vector2 forward = new Vector2(0f, 1f);

    public override void Initialize() => base.Initialize();

    public override void EnterState() => base.EnterState();

    public override void RunUpdate()
    {
        WalkForward();

        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSSpinState>();
    }

    public override void ExitState() => base.ExitState();

    private void WalkForward() => player.playerController3D.InputWalk(forward);
}
