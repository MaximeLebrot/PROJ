using System;
using UnityEngine;

public class AwayController : MonoBehaviour {

    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private float awayTimer;
    
    private float timeSinceLastInput;

    private Action inputChecker;

    private void Awake() => inputChecker = ReadInput;
    
    private void Update() => inputChecker?.Invoke();

    private void ReadInput() {
        
        if (InputIsZero()) {
            timeSinceLastInput += Time.deltaTime;
        }
        else
            timeSinceLastInput = 0;

        if (timeSinceLastInput > awayTimer == false) 
            return;
        
        EventHandler<AwayFromKeyboardEvent>.FireEvent(null);
        inputChecker = WaitForInput;

    }

    private bool InputIsZero() {
        Vector2 movementInput = inputReference.InputMaster.Movement.ReadValue<Vector2>();
        Vector2 aimInput = inputReference.InputMaster.MoveCamera.ReadValue<Vector2>();
        
        return movementInput + aimInput == Vector2.zero;
    } 

    private void WaitForInput() {
        if (InputIsZero()) 
            return;
        
        EventHandler<AwayFromKeyboardEvent>.FireEvent(null);
        inputChecker = ReadInput;
        timeSinceLastInput = 0;

    }
}
