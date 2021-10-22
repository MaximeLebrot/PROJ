using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/AirborneState")]
public class AirborneState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }

    //NOTE this state should NOT have any values, and therefore not call its superstate's EnterState()
    public override void EnterState()
    {
        //Debug.Log("Entered Airborne State");
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
        xMove = inputMaster.Player.Movement.ReadValue<Vector2>().x;
        zMove = inputMaster.Player.Movement.ReadValue<Vector2>().y;

        Vector3 input =
        Vector3.right * xMove +
        Vector3.forward * zMove;

        player.playerController3D.InputGrounded(input * player.physics.AirControl);
    }
    private void LeaveAirborneState()
    {
        if (player.physics.velocity.magnitude < player.physics.SurfThreshold)
            stateMachine.ChangeState<WalkState>();
        else
            stateMachine.ChangeState<GlideState>();

    }
}
