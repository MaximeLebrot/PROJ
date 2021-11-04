using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwayController : MonoBehaviour {

    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private float awayTimer;
    
    private float timeSinceLastInput;

    private Action InputChecker;

    private void Awake() => InputChecker = ReadInput;
    
    private void Update() => InputChecker?.Invoke();

    private void ReadInput() {
        
        if (InputIsZero()) {
            timeSinceLastInput += Time.deltaTime;
        }
        else
            timeSinceLastInput = 0;

        if (timeSinceLastInput > awayTimer) {
            Debug.Log("Changing TO AFK");
            EventHandler<AwayFromKeyboardEvent>.FireEvent(null);
            InputChecker = WaitForInput;
        }
            
    }

    private bool InputIsZero() {
        Vector2 movementInput = inputReference.InputMaster.Movement.ReadValue<Vector2>();
        Vector2 aimInput = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        return movementInput + aimInput == Vector2.zero;
    } 

    private void WaitForInput() {
        if (InputIsZero()) return;
        
        EventHandler<AwayFromKeyboardEvent>.FireEvent(null);
        InputChecker = ReadInput;
        timeSinceLastInput = 0;

    }
}
