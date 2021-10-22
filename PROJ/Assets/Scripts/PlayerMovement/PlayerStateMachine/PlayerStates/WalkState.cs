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
        player.physics.SetGlide(false);
        base.EnterState();
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
        xMove = inputMaster.Player.Movement.ReadValue<Vector2>().x;
        zMove = inputMaster.Player.Movement.ReadValue<Vector2>().y;

        Vector3 input =
        Vector3.right * xMove +
        Vector3.forward * zMove;

        player.playerController3D.InputGrounded(input);
    }
}
