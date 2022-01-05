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
    
    protected void Awake() {
        controllerInputReference.Initialize();

        pageController = GetComponent<PageController>();
;        
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        
        Initialize();

        pageController.OnSuspendInput += SuspendInputEvent;
    }
    
    private void OnEnable() {
        EventHandler<StartPuzzleEvent>.RegisterListener((OnPuzzleStart));
        EventHandler<ExitPuzzleEvent>.RegisterListener(OnPuzzleExit);
        controllerInputReference.InputMaster.Menu.performed += HandleBackInput;
    }

    private void OnDisable() {
        EventHandler<StartPuzzleEvent>.UnregisterListener((OnPuzzleStart));
        EventHandler<ExitPuzzleEvent>.UnregisterListener(OnPuzzleExit);
    }

    private void HandleBackInput(InputAction.CallbackContext e) => onBackInput?.Invoke();

    private void OnPuzzleStart(StartPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed -= HandleBackInput;

    private void OnPuzzleExit(ExitPuzzleEvent e) => controllerInputReference.InputMaster.Menu.performed += HandleBackInput;


    protected abstract void Initialize();
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }

}
