using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PlayerStates/AirborneState")]
public class AirborneState : PlayerState
{
    private PlayerState nextState;
    public override void Initialize()
    {
        base.Initialize();
    }

    //NOTE this state should NOT have any values, and therefore not call its superstate's EnterState()
    public override void EnterState(PlayerState previousState)
    {
        nextState = previousState;
        //Debug.Log("Entered Airborne State");
        player.physics.SetFallingGravity();
    }
    public override void RunUpdate()
    {
        SetInput();

        if (player.playerController3D.IsGrounded())
            LeaveAirborneState();
    }
    public override void ExitState()
    {       
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void LeaveAirborneState()
    {
        player.physics.SetNormalGravity();
        
        if(nextState == null || nextState.GetType() == typeof(WalkState))
            stateMachine.ChangeState<WalkState>();
        else
            //Else if**, we probably want some other requirement to remain here, be it speed or glideable material/tag
            stateMachine.ChangeState<GlideState>();
            

    }
}
