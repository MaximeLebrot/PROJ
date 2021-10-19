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
        base.EnterState();
    }
    public override void RunUpdate()
    {
       player.physics.GlideInput();
        //if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
        if (Input.GetKeyDown(KeyCode.H))
            stateMachine.ChangeState<WalkState>();
    }
    public override void ExitState()
    {
        //Give some sort of lerp in friction/max speed transition
        base.ExitState();
    }
}
