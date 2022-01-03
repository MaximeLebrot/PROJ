using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/WalkState")]
public class WalkState : PlayerState
{
    public override void Initialize()
    {
        base.Initialize();
    }
    private void LoadInputs()
    {
        if (stateMachine.holdToSprint)
        {
            Debug.Log("hodl to sprint from walk state");
            player.inputReference.InputMaster.Sprint.performed += OnSprintActivate;
        }
        else
        {
            Debug.Log("press to sprint from walk state");
            player.inputReference.InputMaster.Sprint.Enable();
            player.inputReference.InputMaster.Sprint.performed += OnSprintActivate;
        }
    }
    private void UnloadInputs()
    {
        if (stateMachine.holdToSprint)
        {
            player.inputReference.InputMaster.Sprint.performed -= OnSprintActivate;
        }
        else
        {
            player.inputReference.InputMaster.Sprint.Disable();
            player.inputReference.InputMaster.Sprint.performed -= OnSprintActivate;
        }
    }
    public override void EnterState()
    {
        LoadInputs();
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
        UnloadInputs();
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void OnSprintActivate(InputAction.CallbackContext obj)
    {
        Debug.Log("Walk state on sprint activate");
        if (stateMachine.currentState.GetType() == typeof(WalkState))
            stateMachine.ChangeState<SprintState>();
        else
            Debug.Log("current state is not walk, it is " + stateMachine.currentState.GetType());

    }
}
