using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameMenuController : PersistentSingleton<GameMenuController> {
    
    [SerializeField] protected ControllerInputReference controllerInputReference;
    private MenuButtons menuButtons;
    [SerializeField] private GameObject backdrop;

    private GraphicRaycaster graphicRaycaster;
    
    private MenuSettingsController menuSettingsController;

    private Action onMenuPressed;
    
    private UIButton activeButton;
  
    
    protected override void InitializeSingleton() {

        menuButtons = GetComponentInChildren<MenuButtons>();
        menuButtons.Initialize();
        
        menuSettingsController = GetComponent<MenuSettingsController>();
        menuSettingsController.Initialize();

        graphicRaycaster = GetComponent<GraphicRaycaster>();
        
        DontDestroyOnLoad(this);
        ActivateComponents(false);

        onMenuPressed = OpenMenu;
    }

    private void OnEnable() {
        EventHandler<StartPuzzleEvent>.RegisterListener(OnPuzzleEnter);
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener(OnPuzzleEnter);
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
    }

    private void OnPuzzleEnter(StartPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed -= HandleInput;
    private void OnPuzzleExit(ExitPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed += HandleInput;

    private void Start() => controllerInputReference.InputMaster.Menu.performed += HandleInput;

    private void HandleInput(InputAction.CallbackContext e) => onMenuPressed?.Invoke();

    private async void OpenMenu() {

        DisableInput();
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(true));
        
        ActivateComponents(true);
        
        await menuButtons.EnableButtons();
        
        Cursor.lockState = CursorLockMode.None;
        EnableInput(CloseMenu);
    }

    //OnClick-event in inspector.
    public async void HandleButtonPressed(UIButton pressedButton) {

        DisableInput();
        
        if (activeButton != null && pressedButton == activeButton) {
            await ResetMenu(pressedButton);
            EnableInput(CloseMenu);
        }
        else if (activeButton != null && pressedButton != activeButton) {
            await OpenAnotherSetting(pressedButton);
            EnableInput(GoBack);
        }
        else {
            activeButton = pressedButton;
            await OpenSetting(pressedButton);
            EnableInput(GoBack);
        }
        
    }

    private async Task OpenSetting(UIButton pressedButton) {
        
        await menuButtons.MoveButtons(pressedButton);
        
        pressedButton.AssociatedMenuSetting.gameObject.SetActive(true);
        
        await pressedButton.AssociatedMenuSetting.ActivatePage();
        
    }
    
    private async Task ResetMenu(UIButton pressedButton) {
        
        await Task.WhenAll(pressedButton.AssociatedMenuSetting.DisablePage(), menuButtons.ResetButtons());
        
        pressedButton.AssociatedMenuSetting.gameObject.SetActive(false);

        activeButton = null;
    }
    
    private async Task ResetMenu(MenuSettings menuSetting) {
        
        await Task.WhenAll(menuSetting.DisablePage(), menuButtons.ResetButtons());
        
        menuSetting.gameObject.SetActive(false);

        activeButton = null;
    }

    
    private async Task OpenAnotherSetting(UIButton pressedButton) {
        
        await Task.WhenAll(activeButton.AssociatedMenuSetting.DisablePage(), menuButtons.MoveButtons(pressedButton), pressedButton.AssociatedMenuSetting.ActivatePage());
        
        pressedButton.AssociatedMenuSetting.gameObject.SetActive(true);
        activeButton.AssociatedMenuSetting.gameObject.SetActive(false);
        activeButton = pressedButton;
    }
    
    private async void GoBack() {
        DisableInput();
        await ResetMenu(activeButton);
        EnableInput(CloseMenu);
        
    }

    private async void CloseMenu() {
        
        DisableInput();
        
        await menuButtons.DisableButtons();
        
        ActivateComponents(false);
        EventHandler<InGameMenuEvent>.FireEvent(new InGameMenuEvent(false));

        EnableInput(OpenMenu);
    }

    public UIMenuItemBase RequestOption<T>() => menuSettingsController.FindRequestedOption<T>();
    
    private void ActivateComponents(bool activateComponents) {
        menuButtons.gameObject.SetActive(activateComponents);
        Cursor.lockState = activateComponents ? CursorLockMode.None : CursorLockMode.Locked;
        backdrop.SetActive(activateComponents);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(activateComponents));
    }

    //Called from scene changer buttons (beta release) / Martin
    public async void SceneChangerCloseMenu(MenuSettings menuSetting) {
        await ResetMenu(menuSetting);
        CloseMenu();
    }

    private void DisableInput() {
        graphicRaycaster.enabled = false;
        onMenuPressed = null;
    }

    private void EnableInput(Action callback) {
        onMenuPressed = callback;
        graphicRaycaster.enabled = true;
    }

    public void Quit() => Application.Quit();
}
