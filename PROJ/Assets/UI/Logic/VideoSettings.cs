public class VideoSettings : MenuSettings {
    
    public override void UpdateSettings(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("Field of View").ID].SetValue(settingsData.fieldOfView);
        menuOptions[ExtractMenuItem("Brightness").ID].SetValue(settingsData.brightness);
        menuOptions[ExtractMenuItem("Quality").ID].SetValue(settingsData.quality);
        menuOptions[ExtractMenuItem("Fullscreen").ID].SetValue(settingsData.fullscreen);
        menuOptions[ExtractMenuItem("Resolution").ID].SetValue(settingsData.screenResolution);
        
    }
}
