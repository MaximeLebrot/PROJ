using TMPro;
using UnityEngine;

public abstract class DropDownItem : UIMenuItem<string> {

    [SerializeField] private TMP_Dropdown dropdownList;

    public delegate void OnValueChanged(dynamic value);

    public event OnValueChanged onValueChanged;
    
    public override void Initialize() {
        dropdownList.onValueChanged.AddListener(ValueChanged);
    }
    public override string GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(string value) {
        dropdownList.value = dropdownList.options.FindIndex(resolutionOption => resolutionOption.text.Equals(value));
    }

    private void ValueChanged(int value) {

        dynamic item = dropdownList.options[dropdownList.value].text;
        
        onValueChanged?.Invoke(item);
        
        ExecuteAdditionalLogic();
    }

}
