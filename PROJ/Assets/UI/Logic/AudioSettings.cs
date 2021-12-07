public class AudioSettings : MenuSettings {
    
    public override void UpdateSettings(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("Mute").ID].SetValue(settingsData.mute);
        menuOptions[ExtractMenuItem("Master").ID].SetValue(settingsData.masterVolume);
        menuOptions[ExtractMenuItem("Music").ID].SetValue(settingsData.musicVolume);
        menuOptions[ExtractMenuItem("Ambience").ID].SetValue(settingsData.ambience);
        menuOptions[ExtractMenuItem("SFX").ID].SetValue(settingsData.soundEffectsVolume);
        menuOptions[ExtractMenuItem("Voice").ID].SetValue(settingsData.voiceVolume);
        
    }
}
