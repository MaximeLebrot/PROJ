using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChanger : MonoBehaviour {

    private List<TextMeshProUGUI> textComponents;

    private Dictionary<int, float> defaultFontSizes;

    public void GatherAllTextComponents() {

        defaultFontSizes = new Dictionary<int, float>();
        
        textComponents = new List<TextMeshProUGUI>();
        textComponents.AddRange(GetComponentsInChildren<TextMeshProUGUI>());

        foreach (TextMeshProUGUI text in textComponents)
            defaultFontSizes.Add(text.GetInstanceID(), text.fontSize);
    }

    public void ChangeFontSize(float newFontSize) {
        foreach (TextMeshProUGUI text in textComponents)
            text.fontSize = newFontSize;
    }

}
