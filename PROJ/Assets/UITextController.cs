using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI textComponent;

    private float defaultFontSize;
    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
        defaultFontSize = textComponent.fontSize;
    }

    public void ChangeFont(float newSize) => textComponent.fontSize = newSize;

    public void ResetFont() => ChangeFont(defaultFontSize);
}
