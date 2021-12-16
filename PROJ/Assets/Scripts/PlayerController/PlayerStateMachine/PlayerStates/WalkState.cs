using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/WalkState")]
public class WalkState : PlayerState
{
    private InputAction sprint;
    public override void Initialize()
    {
        base.Initialize();
        sprint = player.inputReference.InputMaster.Sprint;
        sprint.Enable();
    }
    public override void EnterState()
    {
        player.inputReference.InputMaster.Sprint.performed += OnSprintActivate;
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
        player.inputReference.InputMaster.Sprint.performed -= OnSprintActivate;
        base.ExitState();
    }
    private void SetInput()
    {
        player.playerController3D.InputWalk(player.inputReference.InputMaster.Movement.ReadValue<Vector2>());
    }
    private void OnSprintActivate(InputAction.CallbackContext obj)
    {
            stateMachine.ChangeState<SprintState>();
    }
}
