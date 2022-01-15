public class AudioSettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        (menuOptions[typeof(MasterVolume)] as MasterVolume).SetValue(settingsData.masterVolume);
        (menuOptions[typeof(MusicVolume)] as MusicVolume).SetValue(settingsData.musicVolume);
        (menuOptions[typeof(SFXVolume)] as SFXVolume).SetValue(settingsData.soundEffectsVolume);
        (menuOptions[typeof(AmbianceVolume)] as AmbianceVolume).SetValue(settingsData.ambience);
        (menuOptions[typeof(VoiceVolume)] as VoiceVolume).SetValue(settingsData.voiceVolume);
        
    }

    public override void ApplyItemValues(ref SettingsData settingsData) {
        settingsData.masterVolume = (menuOptions[typeof(MasterVolume)] as MasterVolume).GetValue();
        settingsData.musicVolume = (menuOptions[typeof(MusicVolume)] as MusicVolume).GetValue();
        settingsData.soundEffectsVolume = (menuOptions[typeof(SFXVolume)] as SFXVolume).GetValue();
        settingsData.ambience = (menuOptions[typeof(AmbianceVolume)] as AmbianceVolume).GetValue();
        settingsData.voiceVolume = (menuOptions[typeof(VoiceVolume)] as VoiceVolume).GetValue();
    }
}
