using System.Collections.Generic;
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

    private async void OpenMenu() {
        
        ActivateComponents(true);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(true));

        await settingsMenuObject.GetComponent<FadeGroup>().Fade(FadeMode.FadeIn);
        
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
        SwitchSubMenu(subMenuDepth.Pop());
    }

    private void CloseMenu() {
        ActivateComponents(false);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));
    }

    private void ActivateComponents(bool activateComponents) {
        Cursor.lockState = activateComponents ? CursorLockMode.None : CursorLockMode.Locked;
        settingsMenuObject.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }
}
