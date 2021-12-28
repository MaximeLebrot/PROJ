using System;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
    private void OnEnable() => EventHandler<SaveSettingsEvent>.RegisterListener(SwitchToContrastMode);
    private void OnDisable() => EventHandler<SaveSettingsEvent>.UnregisterListener(SwitchToContrastMode);
    

    private void SwitchToContrastMode(SaveSettingsEvent settings) {
        contrastModeActive = settings.settingsData.highContrastMode;
        overlayCamera.gameObject.SetActive(contrastModeActive);
        mainCamera.cullingMask = contrastModeActive ? contrastModeRenderLayers : mainRegularRenderLayers; 
        colorAdjustments.saturation.value = contrastModeActive ? colorAdjustments.saturation.min : 0;
    }
    
}
