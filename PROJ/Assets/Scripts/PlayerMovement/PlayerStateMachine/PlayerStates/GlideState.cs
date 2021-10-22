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
        //Debug.Log("Entered Glide State");
        player.playerController3D.TransitionSurf();
        player.physics.SetGlide(true);
        base.EnterState();
    }
    public override void RunUpdate()
    {
        SetInput();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<AirborneState>();

        if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
            stateMachine.ChangeState<WalkState>();
    }
    public override void ExitState()
    {
        player.playerController3D.TransitionSurf();
        player.physics.SetGlide(false);
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputGrounded(inputMaster.Player.Movement.ReadValue<Vector2>());
    }
}
