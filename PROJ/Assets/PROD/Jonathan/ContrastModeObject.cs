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

        DetermineIfContrastModeIsActive();
    }

    private void OnEnable() {
        EventHandler<SaveSettingsEvent>.RegisterListener(OnContrastModeEvent);
        DetermineIfContrastModeIsActive();
    }

    private void DetermineIfContrastModeIsActive() {

        var menu = FindObjectOfType<InGameMenu>();
        bool isContrastModeActive = false;

        if (menu != null)
            isContrastModeActive = menu.SettingsMenu.userSettings.highContrastMode;

        if (!isContrastModeActive) return;
        
        SwapMaterials(true);

    }
    
    private void OnDisable() => EventHandler<SaveSettingsEvent>.UnregisterListener(OnContrastModeEvent);

    private void SwapMaterials(bool isContrastModeActive) => meshRenderer.materials = isContrastModeActive ? swapMaterials.ToArray() : defaultMaterials.ToArray();

    private void OnContrastModeEvent(SaveSettingsEvent settings) => SwapMaterials(settings.settingsData.highContrastMode);
}