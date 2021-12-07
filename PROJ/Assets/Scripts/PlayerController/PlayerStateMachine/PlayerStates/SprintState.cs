using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/SprintState")]
public class SprintState : PlayerState
{
    private InputAction sprint; 
    public override void Initialize()
    {
        base.Initialize();
        sprint = player.inputReference.InputMaster.Sprint;
        sprint.Enable();      
    }
    private void SetupInputs()
    {
        //Hold to Sprint
        player.inputReference.InputMaster.Sprint.canceled += OnSprintActivate;
        //Press to sprint
        //player.inputReference.InputMaster.Sprint.performed += OnSprintActivate;
    }
    private void UnloadInputs()
    {
        player.inputReference.InputMaster.Sprint.canceled -= OnSprintActivate;
    }
    public override void EnterState()
    {
        Debug.Log("Entered Sprint State");
        SetupInputs();
        player.animator.SetTrigger("Sprint");
        base.EnterState();
        
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
        Debug.Log("Exiting Sprint State");
        player.animator.SetTrigger("Walk");
        base.ExitState();
        UnloadInputs();   
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void OnSprintActivate(InputAction.CallbackContext obj)
    {
        stateMachine.ChangeState<WalkState>();
        
    }
}