using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MenuController {

    public GameObject settingsMenuObject;

    private System.Action onBackInput; 

    protected override void Initialize() {
        onBackInput = OpenMenu;
        
        controllerInputReference.InputMaster.Menu.performed += HandleBackInput;
        ActivateComponents(false);
    }

    private void HandleBackInput(InputAction.CallbackContext e) => onBackInput?.Invoke();

    private void OpenMenu() {
        
        ActivateComponents(true);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(true));
        SwitchPage("MenuButtons");
        onBackInput = Back;
    }

    private void Back() {

        if (inputSuspended)
            return;
        
        menuAnimator.Back();

        if (menuAnimator.InsideSubMenu())
            return; //Player still inside a submenu 
        
        //No more submenus, close game menu
        CloseMenu();
        
        onBackInput = OpenMenu;
    }

    private void CloseMenu() {
        ActivateComponents(false);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));
    }

    private void ActivateComponents(bool activate) {
        Cursor.lockState = activate ? CursorLockMode.None : CursorLockMode.None;
        menuAnimator.EnableAnimator(activate); 
        settingsMenuObject.SetActive(activate);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activate));
    }
}
