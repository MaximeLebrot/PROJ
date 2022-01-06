using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class DropDownItem : UIMenuItem<string> {

    [SerializeField] private TMP_Dropdown dropdownList;
    
    
    public override string GetValue() {
        return dropdownList.options[dropdownList.value].text;
    }

    public override void SetValue(string value) {
        dropdownList.value = dropdownList.options.FindIndex(resolutionOption => resolutionOption.text.Equals(value));
    }

    public void AddListener(UnityAction<int> callback) => dropdownList.onValueChanged.AddListener(callback);
    
    public void RemoveListener(UnityAction<int> callback) => dropdownList.onValueChanged.RemoveListener(callback);
    

}
