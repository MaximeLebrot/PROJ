using TMPro;
using UnityEngine;

public class DropDownItem : UIMenuItem {

    [SerializeField] private TMP_Dropdown dropdownList;
    
    protected override void Initialize() {}

    public override dynamic GetValue() => dropdownList.options[dropdownList.value].text;
    public override void SetValue(dynamic value) {

       // Debug.Log(value);
        
        //dropdownList.value = dropdownList.options[value];
    }
}
