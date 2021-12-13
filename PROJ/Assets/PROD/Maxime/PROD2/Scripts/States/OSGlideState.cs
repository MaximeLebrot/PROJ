using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSGlideState")]
public class OSGlideState : PlayerState
{
    private Vector2 forward = new Vector2(0f, 1f);
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void EnterState()
    {
        Debug.Log("Entered Glide State");
        base.EnterState();
    }

    public override void RunUpdate()
    {
        GlideForward();

        if (player.inputReference.OneSwitchInputMaster.OnlyButton.triggered)
            stateMachine.ChangeState<OSSpinState>();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<OSAirborneState>();
        
        /*if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
            stateMachine.ChangeState<OSWalkState>();*/
    }
        //NOTE
        //SetGlide(false) & playerController3D.TransitionSurf() would do well in ExitState, but this means that airborne state cannot use the glide camera
        //If this is changed, make sure to remove the SetGlide(false) in WalkState.EnterState 
    public override void ExitState()
    {
        base.ExitState();
    }

    private void GlideForward()
    {
        //player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
        player.playerController3D.InputWalk(forward);
    }
}
