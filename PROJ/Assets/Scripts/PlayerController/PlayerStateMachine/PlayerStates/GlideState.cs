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
        player.animator.SetBool("surfing", true);
        player.glideParticle.Play();
        player.physics.SetGlide(true);
        base.EnterState();
    }
    public override void RunUpdate()
    {
        SetInput();

        if (!player.playerController3D.IsGrounded())
            stateMachine.ChangeState<AirborneState>(this);
        
        if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
        {
            stateMachine.ChangeState<WalkState>();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        player.animator.SetBool("surfing", false);
        player.glideParticle.Stop();
    }
    private void SetInput()
    {
        player.playerController3D.InputGlide(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
}
