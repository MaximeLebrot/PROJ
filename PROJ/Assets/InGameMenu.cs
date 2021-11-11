using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private ControllerInputReference inputReference;
    
    private GameObject internalReferenceToInGameMenu;
    
    private void Start() {
        inputReference.InputMaster.Menu.performed += OpenMenu;
    }
    
    private void OpenMenu(InputAction.CallbackContext e) {
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(true));
        internalReferenceToInGameMenu= Instantiate(inGameMenu);
        inputReference.InputMaster.Menu.performed -= OpenMenu;
        inputReference.InputMaster.Menu.performed += CloseMenu;
    }

    private void CloseMenu(InputAction.CallbackContext e) {
        Destroy(internalReferenceToInGameMenu);
        EventHandler<LockInputEvent>.FireEvent(new LockInputEvent(false));
        inputReference.InputMaster.Menu.performed -= CloseMenu;
        inputReference.InputMaster.Menu.performed += OpenMenu;

    }
}
