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

    private void OnEnable() => EventHandler<SaveSettingsEvent>.RegisterListener(OnContrastModeEvent);
    private void OnDisable() => EventHandler<SaveSettingsEvent>.UnregisterListener(OnContrastModeEvent);
    
    
    private void OnContrastModeEvent(SaveSettingsEvent settings) => meshRenderer.materials = settings.settingsData.highContrastMode ? swapMaterials.ToArray() : defaultMaterials.ToArray();
}