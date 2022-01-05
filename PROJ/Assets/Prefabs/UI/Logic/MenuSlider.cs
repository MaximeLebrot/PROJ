using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class MenuSlider : UIMenuItem<float> {

    [SerializeField] protected Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    protected override void Initialize() => AddListener(UpdateSlider);

    public override float GetValue() => slider.value;

    public override void SetValue(float value) => UpdateSlider(value);

    public void AddListener(UnityAction<float> callback) => slider.onValueChanged.AddListener(callback);
    
    private void UpdateSlider(float newValue) {
        slider.value = newValue;
        percentText.text = ((int)(newValue)).ToString();
    }
    
    protected virtual void ExecuteAdditionalLogic() {}
    
}
