using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class MenuSlider : UIMenuItem<float> {

    [SerializeField] protected Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    public override void Initialize() => AddListener(UpdateSlider);

    public override float GetValue() => slider.value;

    public override void SetValue(float value) => UpdateSlider(value);

    public void AddListener(UnityAction<float> callback) => slider.onValueChanged.AddListener(callback);
    
    public void RemoveListener(UnityAction<float> callback) => slider.onValueChanged.RemoveListener(callback);
    
    private void UpdateSlider(float newValue) {

        slider.value = newValue;
        
        float displayValue = newValue * 100;
        percentText.text = ((int)(displayValue)).ToString();
        ExecuteAdditionalLogic();
    }
    
}
