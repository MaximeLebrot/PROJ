using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/GlideState")]
public class GlideState : PlayerState
{

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void EnterState()
    {
        Debug.Log("Entered Glide State");
        player.physics.SetGlide(true);
        base.EnterState();
    }
    public override void RunUpdate()
    {
        xMove = inputMaster.Player.Movement.ReadValue<Vector2>().x;
        zMove = inputMaster.Player.Movement.ReadValue<Vector2>().y;

        Vector3 input =
        Vector3.right * xMove +
        Vector3.forward * zMove;

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<AirborneState>();

        if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
            stateMachine.ChangeState<WalkState>();
    }
    public override void ExitState()
    {
        //Give some sort of lerp in friction/max speed transition
        base.ExitState();
    }
}
