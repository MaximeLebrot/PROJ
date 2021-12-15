using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MenuController {

    public MenuSettings settingsMenuObject;

    private System.Action onBackInput;
    
    protected override void Initialize() {
        DontDestroyOnLoad(this);
        onBackInput = OpenMenu;
        controllerInputReference.InputMaster.Menu.performed += HandleBackInput;
        ActivateComponents(false);
    }

    private void HandleBackInput(InputAction.CallbackContext e) => onBackInput?.Invoke();

    private void OpenMenu() {
        
        ActivateComponents(true);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(true));

        ActivateSubMenu(settingsMenuObject);
        
        Cursor.lockState = CursorLockMode.None;
        onBackInput = Back;
    }

    private void Back() {

        if (inputSuspended)
            return;

        //No more submenus
        if (subMenuDepth.Count < 1) {
            CloseMenu();
            onBackInput = OpenMenu;
            
            return;
        }
        
        //Inside submenu
        ActivateSubMenu(subMenuDepth.Pop());
    }

    private void CloseMenu() {
        ActivateComponents(false);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));
    }

    private void ActivateComponents(bool activateComponents) {
        Cursor.lockState = activateComponents ? CursorLockMode.None : CursorLockMode.Locked;
        settingsMenuObject.gameObject.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }
}
