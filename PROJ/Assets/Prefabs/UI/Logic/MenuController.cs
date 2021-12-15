using UnityEngine;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    
    [SerializeField] protected ControllerInputReference controllerInputReference;

    protected bool inputSuspended;
    
    private GraphicRaycaster graphicRaycaster;

    protected PageController pageController;
    
    protected void Awake() {
        controllerInputReference.Initialize();

        pageController = GetComponentInChildren<PageController>();
;        
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();

        InputController.SuspendInputEvent += SuspendInputEvent;
        
        Initialize();
    }
    
    protected abstract void Initialize();
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }
    
    public void ActivateSubMenu(MenuSettings page) => pageController.RegisterSubMenuAsActive(page);
}
