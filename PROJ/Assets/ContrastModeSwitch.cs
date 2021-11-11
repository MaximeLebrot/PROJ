using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ContrastModeSwitch : MonoBehaviour {

    private Camera mainCamera;
    private GameObject overlayCamera;

    [SerializeField] private Volume postProcess;
    [SerializeField] private ControllerInputReference inputReference;

    private ColorAdjustments colorAdjustments;
    
    [Header("Layers to render when in contrast mode")]
    [SerializeField] private LayerMask contrastModeRenderLayers;
    private LayerMask mainRegularRenderLayers;

    private bool contrastModeActivated;

    //Will be IEvent later, this is only for testing
    public delegate void ContrastModeActivate(bool isActive);
    public static event ContrastModeActivate OnContrastModeActivate;
    
    private void Awake() {

        contrastModeActivated = false;
        
        mainCamera = Camera.main;
        overlayCamera = mainCamera.transform.GetChild(0).gameObject;

        mainRegularRenderLayers = mainCamera.cullingMask = -1; //Render every layer. 

        postProcess.profile.TryGet(out colorAdjustments);

        overlayCamera.transform.gameObject.SetActive(false);
    }

    private void Start() {
        inputReference.InputMaster.ContrastMode.performed += SwitchToContrastMode;
    }
    
    private void SwitchToContrastMode(InputAction.CallbackContext e) {
        contrastModeActivated = !contrastModeActivated;
        overlayCamera.gameObject.SetActive(contrastModeActivated);
        mainCamera.cullingMask = contrastModeActivated ? contrastModeRenderLayers : mainRegularRenderLayers; 
        colorAdjustments.saturation.value = contrastModeActivated ? colorAdjustments.saturation.min : 0;

        OnContrastModeActivate?.Invoke(contrastModeActivated);
    }
    
}
