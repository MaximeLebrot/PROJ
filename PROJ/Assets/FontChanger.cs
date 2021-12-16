using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontChanger : MonoBehaviour {

    [SerializeField] private List<TextMeshProUGUI> textComponents;

    private Dictionary<int, float> defaultFontSizes;

    public void Awake() {

        defaultFontSizes = new Dictionary<int, float>();
        
        foreach (TextMeshProUGUI text in textComponents)
            defaultFontSizes.Add(text.GetInstanceID(), text.fontSize);
    }

    public void ChangeFontSize(float newFontSize) {
        foreach (TextMeshProUGUI text in textComponents)
            text.fontSize = newFontSize;
    }
    
    

}
