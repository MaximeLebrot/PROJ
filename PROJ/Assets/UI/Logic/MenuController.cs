using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {

    [SerializeField] private ControllerInputReference controllerInputReference;
    private Animator animator;
    private GraphicRaycaster graphicRaycaster;
    
    private int onAnyKeyHash;
    private int showSettingsMenu;
    private int showGeneralHash;
    private int showVideoHash;
    private int showAudioHash;
    private int showAccessibilityHash;
    
    private Stack<int> depth = new Stack<int>();

    private Dictionary<string, int> pageHashes = new Dictionary<string, int>();
    
    public delegate void ActivatePage(int ID);
    public static event ActivatePage OnActivatePage;

    private bool inputSuspended;
    
    private void Awake() {
        controllerInputReference.Initialize();
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        
        animator = GetComponent<Animator>();

        InputController.SuspendInputEvent += SuspendInputEvent;
        
        onAnyKeyHash = Animator.StringToHash("AnyKeyPressed");
        showSettingsMenu = Animator.StringToHash("ShowSettingsMenu");
        showGeneralHash = Animator.StringToHash("ShowGeneral");
        showVideoHash = Animator.StringToHash("ShowVideo");    
        showAudioHash = Animator.StringToHash("ShowAudio");    
        showAccessibilityHash = Animator.StringToHash("ShowAccessibility");
        
        pageHashes.Add("MenuButtons", showSettingsMenu);
        pageHashes.Add("General", showGeneralHash);
        pageHashes.Add("Video", showVideoHash);
        pageHashes.Add("Audio", showAudioHash);
        pageHashes.Add("Accessibility", showAccessibilityHash);
        
        controllerInputReference.InputMaster.Anykey.performed += OnAnyKeyPressed;
        controllerInputReference.InputMaster.Menu.performed += GoBack;
       
    }
    
    private void OnAnyKeyPressed(InputAction.CallbackContext e) {
        controllerInputReference.InputMaster.Anykey.performed -= OnAnyKeyPressed;
        
        animator.SetTrigger(onAnyKeyHash);
    }
    
    public void SwitchPage(string pageName) {

        if (inputSuspended)
            return;
        
        animator.SetBool(pageHashes[pageName], true);
        
        depth.Push(pageHashes[pageName]);
    }

    private void GoBack(InputAction.CallbackContext e) {
        
        if (depth.Count < 1 || inputSuspended)
            return;
        
        int currentLevel = depth.Pop();
        
        animator.SetBool(currentLevel, !animator.GetBool(currentLevel));
    }
    
    public void OnActivatePageEvent(string name) => OnActivatePage?.Invoke(name.GetHashCode());
    
    private void SuspendInputEvent(bool suspend) {
        inputSuspended = suspend;
        graphicRaycaster.enabled = !inputSuspended;
    }
}