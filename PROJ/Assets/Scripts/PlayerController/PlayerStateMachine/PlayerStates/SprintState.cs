using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/SprintState")]
public class SprintState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }

    private void LoadInputs()
    {
        if (stateMachine.holdToSprint)
        {
            player.inputReference.InputMaster.Sprint.canceled += OnSprintPress;
        }
        else
        {
            player.inputReference.InputMaster.Sprint.performed += OnSprintPress;
            player.inputReference.InputMaster.Sprint.Enable();
        }
    }
    private void UnloadInputs()
    {
        if (stateMachine.holdToSprint)
        {
            player.inputReference.InputMaster.Sprint.canceled -= OnSprintPress;
        }
        else
        {
            player.inputReference.InputMaster.Sprint.Disable();
            player.inputReference.InputMaster.Sprint.performed -= OnSprintPress;
        }
    }
    public override void EnterState()
    {
        player.animator.SetTrigger("Sprint");
        base.EnterState();
        LoadInputs();
    }
    public override void RunUpdate()
    {
        SetInput();
        if (!player.playerController3D.IsGrounded())
        {
            stateMachine.ChangeState<AirborneState>(this);
            return;
        }
    }
    public override void ExitState()
    {

        UnloadInputs();   
        player.animator.SetTrigger("Walk");
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void OnSprintPress(InputAction.CallbackContext obj)
    {
        stateMachine.ChangeState<WalkState>();      
    }
}