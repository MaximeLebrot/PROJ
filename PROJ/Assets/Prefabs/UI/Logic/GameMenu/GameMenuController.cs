using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MenuController {

    [SerializeField] private MenuSettings menuButtons;
    [SerializeField] private GameObject backdrop;
    
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

        ActivateSubMenu(menuButtons);
        onBackInput = Back;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Back() {
        if (inputSuspended)
            return;

        if (pageController.CanMoveUpOneLevel())
            return;
        
        CloseMenu();
        onBackInput = OpenMenu;
        
    }
    
    private void CloseMenu() {
        ActivateComponents(false);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));
    }

    private void ActivateComponents(bool activateComponents) {
        Cursor.lockState = activateComponents ? CursorLockMode.None : CursorLockMode.Locked;
        menuButtons.gameObject.SetActive(activateComponents);
        backdrop.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }
}
