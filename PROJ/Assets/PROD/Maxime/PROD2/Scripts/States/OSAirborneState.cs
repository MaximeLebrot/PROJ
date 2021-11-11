using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/OneSwitchStates/OSAirborneState")]
public class OSAirborneState : PlayerState
{
    private Vector2 forward = new Vector2(0f, 1f);
    public override void Initialize()
    {
        base.Initialize();
    }

    //NOTE this state should NOT have any values, and therefore not call its superstate's EnterState()
    public override void EnterState() 
    {
        Debug.Log("Entered Airborne State");
        player.physics.SetFallingGravity();
    }

    public override void RunUpdate()
    {
        SetInput();

        if (player.playerController3D.IsGrounded())
            LeaveAirborneState();
    }

    public override void ExitState()
    {       
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(forward);
        Debug.Log("flying");
        //player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }

    private void LeaveAirborneState()
    {
        player.physics.SetNormalGravity();

        if (player.physics.velocity.magnitude < player.physics.SurfThreshold)
            stateMachine.ChangeState<OSWalkState>();
        else
            stateMachine.ChangeState<OSGlideState>();

    }
}
