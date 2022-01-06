using UnityEngine;

public class GeneralSettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        (menuOptions[typeof(MouseSensitivity)] as MouseSensitivity).SetValue(settingsData.mouseSensitivity);
        (menuOptions[typeof(SprintMode)] as SprintMode).SetValue(settingsData.sprintMode);
    }

    public override void ApplyItemValues(ref SettingsData settingsData) {
        settingsData.mouseSensitivity = (menuOptions[typeof(MouseSensitivity)] as MouseSensitivity).GetValue();
        settingsData.sprintMode = (menuOptions[typeof(SprintMode)] as SprintMode).GetValue();
    }
}
