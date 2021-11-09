using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSWalkState")]
public class OSWalkState : PlayerState
{
    private Vector2 forward = new Vector2(0f, 1f);

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void EnterState()
    {
        Debug.Log("Entered Walk State");
        base.EnterState();
        player.physics.SetGlide(false);
    }

    public override void RunUpdate()
    {
        WalkForward();

        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSSpinState>();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<OSAirborneState>();

        if (player.physics.velocity.magnitude > player.physics.SurfThreshold + 1 && player.playerController3D.groundHitAngle < player.playerController3D.GlideMinAngle)
            stateMachine.ChangeState<OSGlideState>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void WalkForward()
    {
        //player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
        Debug.Log("Walking");
        player.playerController3D.InputWalk(forward);
    }
}
