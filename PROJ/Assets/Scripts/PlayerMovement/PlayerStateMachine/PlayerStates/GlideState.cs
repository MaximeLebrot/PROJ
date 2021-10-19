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
        player.physics.GlideInput();

        if (Input.GetKeyDown(KeyCode.H))
            stateMachine.ChangeState<WalkState>();
    }
    public override void ExitState()
    {
        //Give some sort of lerp in friction/max speed transition
        base.ExitState();
    }
}
