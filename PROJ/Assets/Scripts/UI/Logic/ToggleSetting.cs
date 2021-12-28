using UnityEngine;
using UnityEngine.UI;

public class ToggleSetting : UIMenuItem {

    [SerializeField] private Toggle toggle;
    
    public override dynamic GetValue() => toggle.isOn;
    public override void SetValue(dynamic value) => toggle.isOn = value;
    

}
