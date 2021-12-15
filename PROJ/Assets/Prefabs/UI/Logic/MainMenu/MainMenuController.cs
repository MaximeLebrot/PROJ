using UnityEngine.InputSystem;
using UnityEngine;
public class MainMenuController : MenuController {
    
    protected override void Initialize() {
        controllerInputReference.InputMaster.Anykey.performed += OnAnyKeyPressed;
        controllerInputReference.InputMaster.Menu.performed += GoBack;

    }
    
    private void OnAnyKeyPressed(InputAction.CallbackContext e) {
        controllerInputReference.InputMaster.Anykey.performed -= OnAnyKeyPressed;
    }
    

    private void GoBack(InputAction.CallbackContext e) {
        if (inputSuspended)
            return;
    }
    
}