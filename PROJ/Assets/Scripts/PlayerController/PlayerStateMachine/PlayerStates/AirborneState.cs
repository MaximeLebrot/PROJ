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
        player.playerController3D.InputWalk(inputMaster.Player.Movement.ReadValue<Vector2>());
    }
    private void LeaveAirborneState()
    {
        player.physics.SetNormalGravity();

        if (player.physics.velocity.magnitude > player.physics.SurfThreshold 
            && player.playerController3D.groundHitAngle < player.playerController3D.GlideMinAngle
            && player.playerController3D.groundHitInfo.collider.gameObject.CompareTag("Glideable"))
            stateMachine.ChangeState<GlideState>();
        else
            stateMachine.ChangeState<WalkState>();

    }
}
