using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    
    [SerializeField] protected ControllerInputReference controllerInputReference;

    protected bool inputSuspended;
    protected PageController pageController;
    
    private GraphicRaycaster graphicRaycaster;
    
    protected Action onBackInput;

    private static MenuController instance;

    public static MenuController Instance => instance;
    
    protected void Awake() {

        instance = this;
        
        controllerInputReference.Initialize();

        pageController = GetComponent<PageController>();
;        
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        
        Initialize();

        pageController.OnSuspendInput += SuspendInputEvent;
    }
    
    protected abstract void Initialize();
    
    private void OnEnable() {
        EventHandler<StartPuzzleEvent>.RegisterListener((OnPuzzleStart));
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
        controllerInputReference.InputMaster.Menu.performed += HandleBackInput;
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener((OnPuzzleStart));
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
    }

    public UIMenuItemBase RequestOption<T>() => pageController.FindRequestedOption<T>();

    private void HandleBackInput(InputAction.CallbackContext e) => onBackInput?.Invoke();

    private void OnPuzzleStart(StartPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed -= HandleBackInput;

    private void OnPuzzleExit(ExitPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed += HandleBackInput;


    
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }

}
