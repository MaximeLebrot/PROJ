using TMPro;
using UnityEngine;

public class DropDownItem : UIMenuItem {

    [SerializeField] private TMP_Dropdown dropdownList;
    
    protected override void Initialize() {}
    public override dynamic GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(dynamic value) => dropdownList.value = dropdownList.options.FindIndex(resolutionOption => resolutionOption.text.Equals(value.ToString()));
}
