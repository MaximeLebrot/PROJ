public class AccessibilitySettings : MenuSettings {
    
    public override void UpdateSettings(SettingsData settingsData) {
        menuOptions[ExtractMenuItem("ChangeFontSize").ID].SetValue(settingsData.fontSize);
        menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].SetValue(settingsData.dyslexiaFont);
        menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].SetValue(settingsData.highContrastMode);
        menuOptions[ExtractMenuItem("BlindMode").ID].SetValue(settingsData.blindMode);
    }
}
