using System.Collections;
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

        //Will be IEvent later, this is only for testing
    }

    private void OnContrastModeEvent(bool isActive) => meshRenderer.materials = isActive ? swapMaterials.ToArray() : defaultMaterials.ToArray();
}