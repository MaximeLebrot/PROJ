public class AccessibilitySettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        menuOptions[ExtractMenuItem("ChangeFontSize").ID].SetValue(settingsData.fontSize);
        menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].SetValue(settingsData.dyslexiaFont);
        menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].SetValue(settingsData.highContrastMode);
        menuOptions[ExtractMenuItem("BlindMode").ID].SetValue(settingsData.blindMode);
        menuOptions[ExtractMenuItem("OneHandMode").ID].SetValue(settingsData.oneHandMode);
        menuOptions[ExtractMenuItem("CurrentNodeMarker").ID].SetValue(settingsData.currentNodeMarker);
        menuOptions[ExtractMenuItem("ShowClearedSymbols").ID].SetValue(settingsData.showClearedSymbols);
        menuOptions[ExtractMenuItem("EasyPuzzleControls").ID].SetValue(settingsData.easyPuzzleControls);
        menuOptions[ExtractMenuItem("BigNodes").ID].SetValue(settingsData.bigNodes);
        menuOptions[ExtractMenuItem("SymbolDifficulty").ID].SetValue(settingsData.symbolDifficulty);
        menuOptions[ExtractMenuItem("OneSwitchMode").ID].SetValue(settingsData.oneSwitchMode);
    }
    
    public override void ExtractValues(ref SettingsData settingsData) {
        
        settingsData.fontSize = menuOptions[ExtractMenuItem("ChangeFontSize").ID].GetValue();
        settingsData.dyslexiaFont = menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].GetValue();
        settingsData.highContrastMode = menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].GetValue();
        settingsData.blindMode = menuOptions[ExtractMenuItem("BlindMode").ID].GetValue();
        settingsData.oneHandMode = menuOptions[ExtractMenuItem("OneHandMode").ID].GetValue();
        settingsData.currentNodeMarker = menuOptions[ExtractMenuItem("CurrentNodeMarker").ID].GetValue();
        settingsData.showClearedSymbols = menuOptions[ExtractMenuItem("ShowClearedSymbols").ID].GetValue();
        settingsData.easyPuzzleControls = menuOptions[ExtractMenuItem("EasyPuzzleControls").ID].GetValue();
        settingsData.bigNodes = menuOptions[ExtractMenuItem("BigNodes").ID].GetValue();
        settingsData.symbolDifficulty = menuOptions[ExtractMenuItem("SymbolDifficulty").ID].GetValue();
        settingsData.oneSwitchMode = menuOptions[ExtractMenuItem("OneSwitchMode").ID].GetValue();
    }
    
}
