public class AccessibilitySettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        
        (menuOptions[typeof(ChangeFontSize)] as ChangeFontSize).SetValue(settingsData.fontSize);
        (menuOptions[typeof(Use_DyslexiaFont)] as Use_DyslexiaFont).SetValue(settingsData.dyslexiaFont);
        (menuOptions[typeof(Use_HighContrastMode)] as Use_HighContrastMode).SetValue(settingsData.highContrastMode);
        (menuOptions[typeof(BlindMode)] as BlindMode).SetValue(settingsData.blindMode);
        (menuOptions[typeof(OneHandMode)] as OneHandMode).SetValue(settingsData.oneHandMode);
        (menuOptions[typeof(CurrentNodeMarker)] as CurrentNodeMarker).SetValue(settingsData.currentNodeMarker);
        (menuOptions[typeof(ShowClearedSymbols)] as ShowClearedSymbols).SetValue(settingsData.showClearedSymbols);
        (menuOptions[typeof(EasyPuzzleControls)] as EasyPuzzleControls).SetValue(settingsData.easyPuzzleControls);
        (menuOptions[typeof(BigNodes)] as BigNodes).SetValue(settingsData.bigNodes);
        (menuOptions[typeof(SymbolDifficulty)] as SymbolDifficulty).SetValue(settingsData.symbolDifficulty);
        (menuOptions[typeof(OneSwitchMode)] as OneSwitchMode).SetValue(settingsData.oneSwitchMode);
    }
    
    public override void ApplyItemValues(ref SettingsData settingsData) {
        
        settingsData.fontSize = (menuOptions[typeof(ChangeFontSize)] as ChangeFontSize).GetValue();
        settingsData.dyslexiaFont = (menuOptions[typeof(Use_DyslexiaFont)] as Use_DyslexiaFont).GetValue();
        settingsData.highContrastMode = (menuOptions[typeof(Use_HighContrastMode)] as Use_HighContrastMode).GetValue();
        settingsData.blindMode = (menuOptions[typeof(BlindMode)] as BlindMode).GetValue();
        settingsData.oneHandMode =(menuOptions[typeof(OneHandMode)] as OneHandMode).GetValue();
        settingsData.currentNodeMarker = (menuOptions[typeof(CurrentNodeMarker)] as CurrentNodeMarker).GetValue();
        settingsData.showClearedSymbols = (menuOptions[typeof(ShowClearedSymbols)] as ShowClearedSymbols).GetValue();
        settingsData.easyPuzzleControls = (menuOptions[typeof(EasyPuzzleControls)] as EasyPuzzleControls).GetValue();
        settingsData.bigNodes = (menuOptions[typeof(BigNodes)] as BigNodes).GetValue();
        settingsData.symbolDifficulty = (menuOptions[typeof(SymbolDifficulty)] as SymbolDifficulty).GetValue();
        settingsData.oneSwitchMode = (menuOptions[typeof(OneSwitchMode)] as OneSwitchMode).GetValue();
        
    }
    
}
