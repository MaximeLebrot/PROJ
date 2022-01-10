using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class DropDownItem : UIMenuItem<string> {

    [SerializeField] protected TMP_Dropdown dropdownList;

    public delegate void OnValueChangedString(string optionValue);

    private event OnValueChangedString externalOnValueChanged;                    
    
    public override void Initialize() {
        base.Initialize();

        dropdownList.onValueChanged.AddListener(delegate { externalOnValueChanged?.Invoke(GetValue());});
    }

    public override string GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(string value) {
        dropdownList.value = dropdownList.options.FindIndex(resolutionOption => resolutionOption.text.Equals(value));
    }
    
    public void AddListener(OnValueChangedString callback) {
        externalOnValueChanged += callback;
    }

    public void RemoveListener(OnValueChangedString callback) => externalOnValueChanged -= callback;

    public override void DemandFirstRead() => dropdownList.onValueChanged.Invoke(dropdownList.value);
}
