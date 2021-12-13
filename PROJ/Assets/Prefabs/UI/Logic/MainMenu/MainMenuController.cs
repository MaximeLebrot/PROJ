using UnityEngine.InputSystem;
using UnityEngine;
public class MainMenuController : MenuController {
    
    protected override void Initialize() {
        controllerInputReference.InputMaster.Anykey.performed += OnAnyKeyPressed;
        controllerInputReference.InputMaster.Menu.performed += GoBack;
        
        Debug.Log(menuAnimator);
    }
    
    private void OnAnyKeyPressed(InputAction.CallbackContext e) {
        controllerInputReference.InputMaster.Anykey.performed -= OnAnyKeyPressed;
        
        menuAnimator.SetTrigger("AnyKeyPressed");
    }
    

    private void GoBack(InputAction.CallbackContext e) {
        if (inputSuspended)
            return;
        
        menuAnimator.Back();
    }
    
}