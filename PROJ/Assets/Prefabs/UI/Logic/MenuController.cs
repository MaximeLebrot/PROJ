using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuAnimator))]
public abstract class MenuController : MonoBehaviour {

    [SerializeField] protected ControllerInputReference controllerInputReference;
    public delegate void ActivatePage(int ID);
    public event ActivatePage OnActivatePage;

    protected MenuAnimator menuAnimator;
    protected bool inputSuspended;

    private GraphicRaycaster graphicRaycaster;

    protected void Awake() {
        controllerInputReference.Initialize();
        
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        menuAnimator = GetComponent<MenuAnimator>();
        
        InputController.SuspendInputEvent += SuspendInputEvent;
        
        Initialize();
    }
    
    public void SwitchPage(string pageName) {

        if (inputSuspended)
            return;
        
        menuAnimator.SetBool(pageName, true);
    }


    protected abstract void Initialize();
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }
    
    public void OnActivatePageEvent(string name) {
        OnActivatePage?.Invoke(name.GetHashCode());
    }
}
