using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ContrastModeSwitch : MonoBehaviour {

    private Camera mainCamera;
    [SerializeField] private GameObject overlayCamera;

    [SerializeField] private Volume postProcess;
    
    private ColorAdjustments colorAdjustments;

    private bool contrastModeActive;
    
    [Header("Layers to render when in contrast mode")]
    [SerializeField] private LayerMask contrastModeRenderLayers;
    private LayerMask mainRegularRenderLayers;
    
    private void Awake() {
        
        mainCamera = Camera.main;
        
        mainRegularRenderLayers = mainCamera.cullingMask = -1; //Render every layer. 

        postProcess.profile.TryGet(out colorAdjustments); 
        overlayCamera.transform.gameObject.SetActive(false);
    }

    private void OnEnable() {
        
        (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).AddListener(SwitchToContrastMode);
        
       // EventHandler<SaveSettingsEvent>.RegisterListener(SwitchToContrastMode);
    }

    private void OnDisable() {
        if(GameMenuController.Instance != null)
            (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).RemoveListener(SwitchToContrastMode);
    }


    private void SwitchToContrastMode(bool isOn) {
        contrastModeActive = isOn;
        overlayCamera.gameObject.SetActive(contrastModeActive);
        mainCamera.cullingMask = contrastModeActive ? contrastModeRenderLayers : mainRegularRenderLayers; 
        colorAdjustments.saturation.value = contrastModeActive ? colorAdjustments.saturation.min : 0;
    }
    
}
