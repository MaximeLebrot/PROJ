using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/SprintState")]
public class SprintState : PlayerState
{
    private InputAction sprint;
    private bool holdToSprint = true;
    public override void Initialize()
    {
        base.Initialize();
        EventHandler<SaveSettingsEvent>.RegisterListener(OnSaveSettings);
        sprint = player.inputReference.InputMaster.Sprint;
        sprint.Enable();
    }
    private void LoadInputs()
    {
        //Debug.Log("Load inputs");
        if (holdToSprint)
            player.inputReference.InputMaster.Sprint.canceled += OnSprintActivate;
        else
            player.inputReference.InputMaster.Sprint.performed += OnSprintActivate;
    }
    private void UnloadInputs()
    {
        //Debug.Log("Unload inputs");
        if(holdToSprint)
            player.inputReference.InputMaster.Sprint.canceled -= OnSprintActivate;
        else
            player.inputReference.InputMaster.Sprint.performed -= OnSprintActivate;
    }
    public override void EnterState()
    {
        LoadInputs();
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
    private void OnSaveSettings(SaveSettingsEvent eve)
    {
        holdToSprint = eve.settingsData.holdToSprint;
    }
}