using System;
using UnityEngine;

public class AwayController : MonoBehaviour {

    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private float awayTimer;
    
    private float timeSinceLastInput;

    private Action inputChecker;

    private void Awake() {
        inputChecker = ReadInput;
        
        EventHandler<StartPuzzleEvent>.RegisterListener(TurnAFKCheckOff);
        EventHandler<ExitPuzzleEvent>.RegisterListener(TurnAFKCheckOn);
        EventHandler<InGameMenuEvent>.RegisterListener(TurnAFKCheckOffInMenu);
        
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(TurnAFKCheckOff);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(TurnAFKCheckOn);
        EventHandler<InGameMenuEvent>.UnregisterListener(TurnAFKCheckOffInMenu);
    }

    private void Update() => inputChecker?.Invoke();

    private void TurnAFKCheckOff(StartPuzzleEvent e) => inputChecker = null;
    
    //Fyfan
    private void TurnAFKCheckOffInMenu(InGameMenuEvent e) {
        TurnAFKCheckOff(null);
        EventHandler<InGameMenuEvent>.UnregisterListener(TurnAFKCheckOffInMenu);
        EventHandler<InGameMenuEvent>.RegisterListener(TurnAFKCheckOnOutsideMenu);
    }
    private void TurnAFKCheckOnOutsideMenu(InGameMenuEvent e) {
        TurnAFKCheckOn(null);
        EventHandler<InGameMenuEvent>.UnregisterListener(TurnAFKCheckOnOutsideMenu);
        EventHandler<InGameMenuEvent>.RegisterListener(TurnAFKCheckOffInMenu);
    }

    private void TurnAFKCheckOn(ExitPuzzleEvent e) {
        timeSinceLastInput = 0;
        inputChecker = ReadInput;
    }

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
