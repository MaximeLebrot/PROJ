using UnityEngine;

public class VideoSettings : MenuSettings {
    
    public override void UpdateSettings(SettingsData settingsData) {
        
        menuOptions[ExtractMenuItem("Field of View").ID].SetValue(settingsData.fieldOfView);
        menuOptions[ExtractMenuItem("Brightness").ID].SetValue(settingsData.brightness);
        menuOptions[ExtractMenuItem("Quality").ID].SetValue(settingsData.quality);
        menuOptions[ExtractMenuItem("Fullscreen").ID].SetValue(settingsData.fullscreen);
        menuOptions[ExtractMenuItem("Resolution").ID].SetValue(settingsData.screenResolution);
        
    }

    public override void SaveSettings(ref SettingsData settingsData) {
        settingsData.fieldOfView = menuOptions[ExtractMenuItem("Field of View").ID].GetValue();
        settingsData.brightness = menuOptions[ExtractMenuItem("Brightness").ID].GetValue();
        settingsData.quality = menuOptions[ExtractMenuItem("Quality").ID].GetValue();
        settingsData.fullscreen = menuOptions[ExtractMenuItem("Fullscreen").ID].GetValue();
        
        string resolution = menuOptions[ExtractMenuItem("Resolution").ID].GetValue();
        Debug.Log(resolution);
        
        //settingsData.screenResolution = 
    }
}
