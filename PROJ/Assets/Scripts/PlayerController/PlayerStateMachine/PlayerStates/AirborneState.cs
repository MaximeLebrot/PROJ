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

        /*if (player.physics.velocity.magnitude < player.physics.SurfThreshold)
        {
            stateMachine.ChangeState<WalkState>();
            return;
        }*/

        if (player.playerController3D.IsGrounded())
        {
            ReturnToPreviousState();
            return;
        }

    }
    public override void ExitState()
    {       
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }

    //No longer needed to differentiate between walk and glide, since glide is removed
    private void ReturnToPreviousState()
    {
        player.physics.RestoreGravity();
        
        if(nextState.GetType() == typeof(SprintState))
            stateMachine.ChangeState<SprintState>();
        else
            stateMachine.ChangeState<WalkState>();
            //Else if**, we probably want some other requirement to remain here, be it speed or glideable material/tag
            
            

    }
}
