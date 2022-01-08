using UnityEngine;

public class GameMenuController : MenuController {

    [SerializeField] private MenuButtons menuButtons;
    [SerializeField] private GameObject backdrop;
    
    protected override void Initialize() {
        menuButtons.SetActive(false, true);
        DontDestroyOnLoad(this);
        onBackInput = OpenMenu;
        ActivateComponents(false);
    }
    
    private void OpenMenu() {
        ActivateComponents(true);
        
        menuButtons.SetActive(true, false);
        
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(true));
        
        onBackInput = Back;
        
        Cursor.lockState = CursorLockMode.None;
    }

    private void Back() {
        
        if (pageController.InputSuspended) 
            return;
        
        if (pageController.IsPageActive()) {
            pageController.ResetPages();
            return;
        }
        
        CloseMenu();
        onBackInput = OpenMenu;
    }
    
    private void CloseMenu() {
        ActivateComponents(false);
        menuButtons.SetActive(false, true);
        //pageController.ResetPages();
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));
    }

    private void ActivateComponents(bool activateComponents) {
        Cursor.lockState = activateComponents ? CursorLockMode.None : CursorLockMode.Locked;
        backdrop.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }

    //Called from scene changer buttons (beta release) / Martin
    public void SceneChangerCloseMenu() {
        CloseMenu();
        onBackInput = OpenMenu;
    }

    public void Quit() => Application.Quit();
}
