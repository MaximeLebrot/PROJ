public class AudioSettings : MenuSettings {
    
    
    /// <summary>
    /// Receives a SettingsData-objects, picks out the relevant fields and values to the corresponding
    /// UI-element. 
    /// </summary>
    /// <param name="settingsData"></param>
    public override void SetMenuItems(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("Mute").ID].SetValue(settingsData.mute);
        menuOptions[ExtractMenuItem("Master").ID].SetValue(settingsData.masterVolume);
        menuOptions[ExtractMenuItem("Music").ID].SetValue(settingsData.musicVolume);
        menuOptions[ExtractMenuItem("Ambience").ID].SetValue(settingsData.ambience);
        menuOptions[ExtractMenuItem("SFX").ID].SetValue(settingsData.soundEffectsVolume);
        menuOptions[ExtractMenuItem("Voice").ID].SetValue(settingsData.voiceVolume);
        
    }

    ///TODO: Settings-menu's are not supposed to manipulate a settingsData directly, should return instead all the values instead. 
    /// <summary>
    /// Assigns all the values in the UI to the SettingsData-object 
    /// </summary>
    /// <param name="settingsData"></param>
    public override void ExtractValues(ref SettingsData settingsData) {
        settingsData.mute = menuOptions[ExtractMenuItem("Mute").ID].GetValue();
        settingsData.masterVolume = menuOptions[ExtractMenuItem("Master").ID].GetValue();
        settingsData.musicVolume = menuOptions[ExtractMenuItem("Music").ID].GetValue();
        settingsData.ambience = menuOptions[ExtractMenuItem("Ambience").ID].GetValue();
        settingsData.soundEffectsVolume = menuOptions[ExtractMenuItem("SFX").ID].GetValue();
        settingsData.voiceVolume = menuOptions[ExtractMenuItem("Voice").ID].GetValue();
    }

}
