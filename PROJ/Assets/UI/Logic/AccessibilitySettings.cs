public class AccessibilitySettings : MenuSettings {
    
    public override void UpdateMenuItems(SettingsData settingsData) {
        menuOptions[ExtractMenuItem("ChangeFontSize").ID].SetValue(settingsData.fontSize);
        menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].SetValue(settingsData.dyslexiaFont);
        menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].SetValue(settingsData.highContrastMode);
        menuOptions[ExtractMenuItem("BlindMode").ID].SetValue(settingsData.blindMode);
        menuOptions[ExtractMenuItem("OneHandMode").ID].SetValue(settingsData.oneHandMode);
    }
    
    public override void ExtractMenuItemValues(ref SettingsData settingsData) {
        settingsData.fontSize = int.Parse(menuOptions[ExtractMenuItem("ChangeFontSize").ID].GetValue());
        settingsData.dyslexiaFont = menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].GetValue();
        settingsData.highContrastMode = menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].GetValue();
        settingsData.blindMode = menuOptions[ExtractMenuItem("BlindMode").ID].GetValue();
        settingsData.oneHandMode = menuOptions[ExtractMenuItem("OneHandMode").ID].GetValue();
    }
}
