using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/SprintState")]
public class SprintState : PlayerState
{
    public InputAction sprint;
    private float sprintThreshold = 3f;
    public override void Initialize()
    {
        base.Initialize();
        sprint = player.inputReference.InputMaster.Sprint;
        sprint.Enable();    
    }

    public override void EnterState()
    {
        //Debug.Log("Entered Sprint State");
        base.EnterState();
        player.inputReference.InputMaster.Sprint.canceled += OnSprintActivate;
    }
    public override void RunUpdate()
    {
        SetInput();
        if (!player.playerController3D.IsGrounded())
        {
            stateMachine.ChangeState<AirborneState>(this);
            return;
        }
        //if the player stops while sprinting, we probably want to cancel the sprint.. but this is maybe not the way to do that
        if (player.physics.velocity.magnitude < sprintThreshold)
            stateMachine.ChangeState<WalkState>();

    }
    public override void ExitState()
    {
        base.ExitState();
        player.inputReference.InputMaster.Sprint.canceled -= OnSprintActivate;
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void OnSprintActivate(InputAction.CallbackContext obj)
    {
        //Debug.Log("Exiting Sprint State");
        stateMachine.ChangeState<WalkState>();
    }
}