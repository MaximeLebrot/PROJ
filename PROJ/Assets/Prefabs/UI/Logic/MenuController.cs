using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuAnimator))]
public abstract class MenuController : MonoBehaviour {
    public event ActivatePage OnActivatePage;

    public delegate void ActivatePage(int ID);

    protected bool inputSuspended;

    [SerializeField] protected ControllerInputReference controllerInputReference;

    protected Stack<string> subMenuDepth = new Stack<string>(); 
    
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
    
    public void OnActivatePageEvent(string pageName) {
        OnActivatePage?.Invoke(pageName.GetHashCode());
        subMenuDepth.Push(pageName);
    }
    
    protected void SwitchSubMenu(string pageName) {
        OnActivatePage?.Invoke(pageName.GetHashCode());
    }
}
