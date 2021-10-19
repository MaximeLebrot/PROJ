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
        Debug.Log("Entered Walk State");
        player.physics.SetGlide(false);
        base.EnterState();
    }
    public override void RunUpdate()
    {
       //player.physics.WalkInput();
        if (player.physics.velocity.magnitude > player.physics.SurfThreshold + 1)
       //if(Input.GetKeyDown(KeyCode.H))
            stateMachine.ChangeState<GlideState>();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
