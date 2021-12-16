using UnityEngine.InputSystem;
using UnityEngine;
public class MainMenuController : MenuController {
    
    [SerializeField] private MenuSettings first;
    
    protected override void Initialize() {
        controllerInputReference.InputMaster.Anykey.performed += OnAnyKeyPressed;
        controllerInputReference.InputMaster.Menu.performed += GoBack;
    }
    
    private void OnAnyKeyPressed(InputAction.CallbackContext e) {
        controllerInputReference.InputMaster.Anykey.performed -= OnAnyKeyPressed;
        first.gameObject.SetActive(true);
        ActivateSubMenu(first);
    }
    

    private void GoBack(InputAction.CallbackContext e) {
        if (inputSuspended)
            return;

        pageController.CanMoveUpOneLevel();
            
    }
    
}