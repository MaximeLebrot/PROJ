using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MenuController {

    public GameObject gameObject;
    
    protected override void Initialize() {
        controllerInputReference.InputMaster.Menu.performed += OpenMenu;
        menuAnimator.EnableAnimator(false);
        gameObject.SetActive(false);
        
    }
    
    private void OpenMenu(InputAction.CallbackContext e) {
        gameObject.SetActive(true);
        menuAnimator.EnableAnimator(true);
        SwitchPage("MenuButtons");
        Cursor.lockState = CursorLockMode.None;
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(true));
        controllerInputReference.InputMaster.Menu.performed -= OpenMenu;
        controllerInputReference.InputMaster.Menu.performed += CloseMenu;
    }

    private void CloseMenu(InputAction.CallbackContext e) {

        bool insideSubMenu = menuAnimator.Back();

        if (insideSubMenu) {
            
        }

        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(false));
        controllerInputReference.InputMaster.Menu.performed -= CloseMenu;
        controllerInputReference.InputMaster.Menu.performed += OpenMenu;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
