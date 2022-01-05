using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleSetting : UIMenuItem<bool> {

    [SerializeField] private Toggle toggle;

    public override bool GetValue() => toggle.isOn;
    public override void SetValue(bool value) => toggle.isOn = value;
}
