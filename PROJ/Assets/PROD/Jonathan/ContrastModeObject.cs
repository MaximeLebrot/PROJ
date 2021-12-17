using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContrastModeObject : MonoBehaviour {

    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private bool replaceMaterials;
    [SerializeField] private List<Material> contrastMaterials;
    
    private List<Material> defaultMaterials = new List<Material>();
    private List<Material> swapMaterials = new List<Material>();
    
    private void Awake() {
        
        defaultMaterials = meshRenderer.materials.ToList();
        
        if (!replaceMaterials)
            swapMaterials.AddRange(defaultMaterials);

        swapMaterials = contrastMaterials;

    }
    
    private void OnEnable() => EventHandler<SaveSettingsEvent>.RegisterListener(DetermineIfContrastModeIsActive);

    private void DetermineIfContrastModeIsActive(SaveSettingsEvent requestSettingsEvent) {

        bool isContrastModeActive = requestSettingsEvent.settingsData.highContrastMode;

        SwapMaterials(isContrastModeActive);
    }
    
    private void OnDisable() => EventHandler<SaveSettingsEvent>.UnregisterListener(DetermineIfContrastModeIsActive);

    private void SwapMaterials(bool isContrastModeActive) => meshRenderer.materials = isContrastModeActive ? swapMaterials.ToArray() : defaultMaterials.ToArray();
    
}