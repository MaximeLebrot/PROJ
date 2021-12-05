using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlider : UIMenuItem {

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    protected override void Initialize() => slider.onValueChanged.AddListener(UpdateText);
    public override dynamic GetValue() => slider.value;

    private void UpdateText(float newValue) {
        slider.value = newValue;
        percentText.text = ((int)(newValue)).ToString();
    }
    
}
