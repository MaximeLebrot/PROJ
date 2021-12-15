using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DropDownItem : UIMenuItem {

    [SerializeField] private TMP_Dropdown dropdownList;
    
    protected override void Initialize() {}
    public override dynamic GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(dynamic value) {
        dropdownList.value = dropdownList.options.FindIndex(dropdownOption => dropdownOption.text.Equals(value.ToString()));
    }
    
  
    public override void OnValueChanged(Action action) {
        dropdownList.onValueChanged.AddListener((e) => action.Invoke());
    }
}
