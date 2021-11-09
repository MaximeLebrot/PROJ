using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContrastModeObject : MonoBehaviour {

    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private List<Material> contrastMaterials;
    [SerializeField] private bool replaceMaterials;

    private List<Material> defaultMaterials;

    private List<Material> swapMaterials;


    private void Awake() {
        defaultMaterials = meshRenderer.materials.ToList();

        if (replaceMaterials)
            swapMaterials.AddRange(defaultMaterials);

        swapMaterials = contrastMaterials;

        //Will be IEvent later, this is only for testing
        ContrastModeSwitch.OnContrastModeActivate += OnContrastModeEvent;

    }

    private void OnContrastModeEvent(bool isActive) => meshRenderer.materials = isActive ? swapMaterials.ToArray() : defaultMaterials.ToArray();
}