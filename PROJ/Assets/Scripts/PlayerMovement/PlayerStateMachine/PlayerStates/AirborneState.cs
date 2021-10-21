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
    }
    public override void RunUpdate()
    {   
        stateMachine.ChangeState<WalkState>();
    }
    public override void ExitState()
    {
         if (player.physics.velocity.magnitude < player.physics.SurfThreshold - 1)
        base.ExitState();
    }

}
