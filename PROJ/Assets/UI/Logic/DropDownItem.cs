using TMPro;
using UnityEngine;

public class DropDownItem : UIMenuItem {

    [SerializeField] private TMP_Dropdown dropdownList;
    
    protected override void Initialize() {}

    public override dynamic GetValue() => dropdownList.options[dropdownList.value].text;
}
