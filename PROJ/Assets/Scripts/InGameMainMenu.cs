using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMainMenu : MainMenuController {

    [SerializeField] private GameObject panel;
    
    private void OnDestroy()
    {
        controllerInputReference.InputMaster.Menu.performed -= CloseMenu;
        controllerInputReference.InputMaster.Menu.performed -= OpenMenu;
    }
    private void Start() {
        controllerInputReference.InputMaster.Menu.performed += OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext e) {
        Cursor.lockState = CursorLockMode.None;
        panel.transform.gameObject.SetActive(true);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(true));
        controllerInputReference.InputMaster.Menu.performed -= OpenMenu;
        controllerInputReference.InputMaster.Menu.performed += CloseMenu;
    }

    private void CloseMenu(InputAction.CallbackContext e) {
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(false));
        controllerInputReference.InputMaster.Menu.performed -= CloseMenu;
        controllerInputReference.InputMaster.Menu.performed += OpenMenu;
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);

    }
    
    
}
