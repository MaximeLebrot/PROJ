using TMPro;
using UnityEngine;

public abstract class DropDownItem<T> : UIMenuItem {

    [SerializeField] private TMP_Dropdown dropdownList;

    public delegate void OnValueChanged(dynamic value);

    public event OnValueChanged onValueChanged;
    
    protected override void Initialize() {
        dropdownList.onValueChanged.AddListener(ValueChanged);
    }
    public override dynamic GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(dynamic value) {
        dropdownList.value = dropdownList.options.FindIndex(resolutionOption => resolutionOption.text.Equals(value.ToString()));
    }

    
    private void ValueChanged(int value) {

        dynamic item = dropdownList.options[dropdownList.value].text;
        
        onValueChanged?.Invoke(item);
    }

}
