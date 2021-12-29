using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleSetting<T> : UIMenuItem {

    [SerializeField] private Toggle toggle;
    
    public override dynamic GetValue() => toggle.isOn;
    public override void SetValue(dynamic value) => toggle.isOn = value;
    

}
