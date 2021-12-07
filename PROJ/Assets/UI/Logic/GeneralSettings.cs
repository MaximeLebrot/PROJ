public class GeneralSettings : MenuSettings {
    
    public override void UpdateMenuItems(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("MouseSensitivity").ID].SetValue(settingsData.mouseSensitivity);
        menuOptions[ExtractMenuItem("HoldToSprint").ID].SetValue(settingsData.holdToSprint);
        menuOptions[ExtractMenuItem("PressToSprint").ID].SetValue(settingsData.pressToSprint);
        
    }

    public override void ExtractMenuItemValues(ref SettingsData settingsData) {
        settingsData.mouseSensitivity = menuOptions[ExtractMenuItem("MouseSensitivity").ID].GetValue();
        settingsData.holdToSprint = menuOptions[ExtractMenuItem("HoldToSprint").ID].GetValue();
        settingsData.pressToSprint = menuOptions[ExtractMenuItem("PressToSprint").ID].GetValue();
    }
}
