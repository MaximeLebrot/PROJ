public class AudioSettings : MenuSettings {
    
    public override void UpdateMenuItems(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("Mute").ID].SetValue(settingsData.mute);
        menuOptions[ExtractMenuItem("Master").ID].SetValue(settingsData.masterVolume);
        menuOptions[ExtractMenuItem("Music").ID].SetValue(settingsData.musicVolume);
        menuOptions[ExtractMenuItem("Ambience").ID].SetValue(settingsData.ambience);
        menuOptions[ExtractMenuItem("SFX").ID].SetValue(settingsData.soundEffectsVolume);
        menuOptions[ExtractMenuItem("Voice").ID].SetValue(settingsData.voiceVolume);
        
    }

    public override void ExtractMenuItemValues(ref SettingsData settingsData) {
        settingsData.mute = menuOptions[ExtractMenuItem("Mute").ID].GetValue();
        settingsData.masterVolume = menuOptions[ExtractMenuItem("Master").ID].GetValue();
        settingsData.musicVolume = menuOptions[ExtractMenuItem("Music").ID].GetValue();
        settingsData.ambience = menuOptions[ExtractMenuItem("Ambience").ID].GetValue();
        settingsData.soundEffectsVolume = menuOptions[ExtractMenuItem("SFX").ID].GetValue();
        settingsData.voiceVolume = menuOptions[ExtractMenuItem("Voice").ID].GetValue();
    }
}
