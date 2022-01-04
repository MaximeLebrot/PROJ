using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuController : MenuController {

    [SerializeField] private MenuSettings menuButtons;
    [SerializeField] private GameObject backdrop;
    
    protected override void Initialize() {
        DontDestroyOnLoad(this);
        onBackInput = OpenMenu;
        ActivateComponents(false);
    }
    
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
        //Debug.Log(menuButtons);
        menuButtons.gameObject.SetActive(activateComponents);
        backdrop.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }

    //Called from scene changer buttons (beta release) / Martin
    public void SceneChangerCloseMenu() {
        CloseMenu();
        onBackInput = OpenMenu;
    }
}
