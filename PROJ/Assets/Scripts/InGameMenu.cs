using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private GameObject panel;
    [SerializeField] private ControllerInputReference inputReference;
    [SerializeField] private SettingsMenu settingsMenu;

    public SettingsMenu SettingsMenu => settingsMenu;

    private void Awake() {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Start() {
        inputReference.InputMaster.Menu.performed += OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext e) {
        Cursor.lockState = CursorLockMode.None;
        panel.transform.gameObject.SetActive(true);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(true));
        inputReference.InputMaster.Menu.performed -= OpenMenu;
        inputReference.InputMaster.Menu.performed += CloseMenu;
    }

    private void CloseMenu(InputAction.CallbackContext e) {
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(false));
        inputReference.InputMaster.Menu.performed -= CloseMenu;
        inputReference.InputMaster.Menu.performed += OpenMenu;
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);

    }
    
    
}
