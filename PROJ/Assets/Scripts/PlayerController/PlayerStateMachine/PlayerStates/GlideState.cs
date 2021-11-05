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
        player.playerController3D.TransitionSurf(true);
        player.physics.SetGlide(true);
        base.EnterState();
    }
    public override void RunUpdate()
    {
        SetInput();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<AirborneState>();
        
        if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
        {
            stateMachine.ChangeState<WalkState>();
        }
    }
        //NOTE
        //SetGlide(false) & playerController3D.TransitionSurf() would do well in ExitState, but this means that airborne state cannot use the glide camera
        //If this is changed, make sure to remove the SetGlide(false) in WalkState.EnterState 
    public override void ExitState()
    {
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputGlide(inputMaster.Player.Movement.ReadValue<Vector2>());
    }
}
