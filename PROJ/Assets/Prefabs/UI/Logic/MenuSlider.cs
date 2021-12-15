using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlider : UIMenuItem {

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    protected override void Initialize() => slider.onValueChanged.AddListener(UpdateSlider);
    public override dynamic GetValue() => slider.value;
    public override void SetValue(dynamic value) => UpdateSlider(value);
    
    private void UpdateSlider(float newValue) {
        slider.value = newValue;
        percentText.text = ((int)(newValue)).ToString();
    }
    
    public override void OnValueChanged(Action action) => slider.onValueChanged.AddListener(e => action());
}
