using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] private ControllerInputReference controllerInputReference;
    private Animator animator;

    private int onAnyKeyHash;
    private int showMainMenu;
    private int showSettingsMenu;
    private int showGeneralHash;
    private int showVideoHash;
    private int showAudioHash;
    private int showAccessibilityHash;

    [SerializeField] private GraphicRaycaster raycaster;
    
    private Stack<int> depth = new Stack<int>();

    private Dictionary<string, int> pageHashes = new Dictionary<string, int>();
    
    public delegate void ActivatePage(int ID);
    public static event ActivatePage OnActivatePage;

    private bool inputSuspended;
    
    private void Awake() {
        controllerInputReference.Initialize();
        
        animator = GetComponent<Animator>();

        onAnyKeyHash = Animator.StringToHash("AnyKeyPressed");
        showSettingsMenu = Animator.StringToHash("ShowSettingsMenu");
        showGeneralHash = Animator.StringToHash("ShowGeneral");
        showVideoHash = Animator.StringToHash("ShowVideo");    
        showAudioHash = Animator.StringToHash("ShowAudio");    
        showAccessibilityHash = Animator.StringToHash("ShowAccessibility");
        
        pageHashes.Add("SettingsMenu", showSettingsMenu);
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
    
    public async void SwitchPage(string pageName) {
        animator.SetBool(pageHashes[pageName], true);
        
        depth.Push(pageHashes[pageName]);
    }

    private void GoBack(InputAction.CallbackContext e) {

        if (depth.Count < 1 || inputSuspended)
            return;
        
        int currentLevel = depth.Pop();
        
        animator.SetBool(currentLevel, !animator.GetBool(currentLevel));
    }
    
    public void OnActivatePageEvent(string name) {
        Debug.Log($"{name} has hashcode {name.GetHashCode()}");
        OnActivatePage?.Invoke(name.GetHashCode());
    }
    
    public void OnSuspendInput(int value) => inputSuspended = value == 1;
}