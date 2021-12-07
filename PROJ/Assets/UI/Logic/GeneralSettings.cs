public class GeneralSettings : MenuSettings {
    
    public override void UpdateSettings(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("MouseSensitivity").ID].SetValue(settingsData.mouseSensitivity);
        menuOptions[ExtractMenuItem("HoldToSprint").ID].SetValue(settingsData.holdToSprint);
        menuOptions[ExtractMenuItem("PressToSprint").ID].SetValue(settingsData.pressToSprint);
        
    }
}
