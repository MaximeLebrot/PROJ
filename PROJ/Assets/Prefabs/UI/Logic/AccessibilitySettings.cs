public class AccessibilitySettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        menuOptions[ExtractMenuItem("ChangeFontSize").ID].SetValue(settingsData.fontSize);
        menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].SetValue(settingsData.dyslexiaFont);
        menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].SetValue(settingsData.highContrastMode);
        menuOptions[ExtractMenuItem("BlindMode").ID].SetValue(settingsData.blindMode);
        menuOptions[ExtractMenuItem("OneHandMode").ID].SetValue(settingsData.oneHandMode);
        menuOptions[ExtractMenuItem("NodeSize").ID].SetValue(settingsData.nodeSize);
        menuOptions[ExtractMenuItem("LineSize").ID].SetValue(settingsData.lineSize);
        menuOptions[ExtractMenuItem("CurrentNodeMarker").ID].SetValue(settingsData.currentNodeMarker);
        menuOptions[ExtractMenuItem("ShowClearedSymbols").ID].SetValue(settingsData.showClearedSymbols);
        menuOptions[ExtractMenuItem("EasyPuzzleControls").ID].SetValue(settingsData.easyPuzzleControls);
        
    }
    
    public override void ApplyItemValues(ref SettingsData settingsData) {
        settingsData.fontSize = int.Parse(menuOptions[ExtractMenuItem("ChangeFontSize").ID].GetValue());
        settingsData.dyslexiaFont = menuOptions[ExtractMenuItem("Use_DyslexiaFont").ID].GetValue();
        settingsData.highContrastMode = menuOptions[ExtractMenuItem("Use_HighContrastMode").ID].GetValue();
        settingsData.blindMode = menuOptions[ExtractMenuItem("BlindMode").ID].GetValue();
        settingsData.oneHandMode = menuOptions[ExtractMenuItem("OneHandMode").ID].GetValue();
        settingsData.nodeSize = menuOptions[ExtractMenuItem("NodeSize").ID].GetValue();
        settingsData.lineSize = menuOptions[ExtractMenuItem("LineSize").ID] .GetValue(); 
        settingsData.currentNodeMarker = menuOptions[ExtractMenuItem("CurrentNodeMarker").ID].GetValue();
        settingsData.showClearedSymbols = menuOptions[ExtractMenuItem("ShowClearedSymbols").ID].GetValue();
        settingsData.easyPuzzleControls = menuOptions[ExtractMenuItem("EasyPuzzleControls").ID].GetValue();
    }
}
