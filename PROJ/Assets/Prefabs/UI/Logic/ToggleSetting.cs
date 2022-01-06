using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ToggleSetting : UIMenuItem<bool> {

    [SerializeField] protected Toggle toggle;

    public void AddListener(UnityAction<bool> callback) => toggle.onValueChanged.AddListener(callback);
    
    public override bool GetValue() => toggle.isOn;
    public override void SetValue(bool value) => toggle.isOn = value;
}
