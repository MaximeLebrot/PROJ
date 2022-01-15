using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ContrastModeSwitch : MonoBehaviour {

    private Camera mainCamera;
    [SerializeField] private GameObject overlayCamera;

    [SerializeField] private Volume postProcess;
    
    private ColorAdjustments colorAdjustments;
    
    private LayerMask colorLayer;
    
    private void Awake() {
        
        mainCamera = Camera.main;
        
        colorLayer = overlayCamera.GetComponent<Camera>().cullingMask;
        
        postProcess.profile.TryGet(out colorAdjustments); 
        overlayCamera.transform.gameObject.SetActive(false);
    }

    private void OnEnable() {
        
        (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).AddListener(SwitchToContrastMode);
    }

    private void OnDisable() {
        if(GameMenuController.Instance != null)
            (GameMenuController.Instance.RequestOption<Use_HighContrastMode>() as Use_HighContrastMode).RemoveListener(SwitchToContrastMode);
    }


    private void SwitchToContrastMode(bool isOn) {
        overlayCamera.gameObject.SetActive(isOn);
        mainCamera.cullingMask = isOn ? ~colorLayer : -1;
        colorAdjustments.saturation.value = isOn ? colorAdjustments.saturation.min : 0;
    }
    
}
