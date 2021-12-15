using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    
    [SerializeField] protected ControllerInputReference controllerInputReference;
    public event ActivatePage OnActivatePage;
    public delegate void ActivatePage(MenuSettings page);

    protected bool inputSuspended;

    protected Stack<MenuSettings> subMenuDepth = new Stack<MenuSettings>(); 
    
    private GraphicRaycaster graphicRaycaster;

    protected void Awake() {
        controllerInputReference.Initialize();
        
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();

        InputController.SuspendInputEvent += SuspendInputEvent;
        
        Initialize();
    }
    
    protected abstract void Initialize();
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }
    
    public void ActivateSubMenu(MenuSettings pageName) {
        OnActivatePage?.Invoke(pageName);
        subMenuDepth.Push(pageName);
    }

    protected void ActivateMenu(MenuSettings pageName) {
        OnActivatePage?.Invoke(pageName);
    }
    
}
