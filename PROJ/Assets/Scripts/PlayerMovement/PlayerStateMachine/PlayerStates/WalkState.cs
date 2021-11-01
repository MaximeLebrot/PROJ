using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/WalkState")]
public class WalkState : PlayerState
{
    public override void Initialize()
    {;
        base.Initialize();
    }
    public override void EnterState()
    {
        //Debug.Log("Entered Walk State");
        base.EnterState();
        player.playerController3D.TransitionSurf(false);
        player.physics.SetGlide(false);
    }
    public override void RunUpdate()
    {
         SetInput();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<AirborneState>();

        if (player.physics.velocity.magnitude > player.physics.SurfThreshold + 1)
            stateMachine.ChangeState<GlideState>();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputGrounded(inputMaster.Player.Movement.ReadValue<Vector2>());
    }
}
