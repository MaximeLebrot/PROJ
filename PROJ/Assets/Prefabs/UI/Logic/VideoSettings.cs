public class VideoSettings : MenuSettings {
    
    public override void SetMenuItems(SettingsData settingsData) {
        (menuOptions[typeof(FieldOfView)] as FieldOfView).SetValue(settingsData.fieldOfView);
        (menuOptions[typeof(Fullscreen)] as Fullscreen).SetValue(settingsData.fullscreen);
        (menuOptions[typeof(SResolution)] as SResolution).SetValue(settingsData.screenResolution);
    }

    public override void ApplyItemValues(ref SettingsData settingsData) {
        settingsData.fieldOfView = (menuOptions[typeof(FieldOfView)] as FieldOfView).GetValue();
        settingsData.fullscreen = (menuOptions[typeof(Fullscreen)] as Fullscreen).GetValue();
        settingsData.screenResolution = (menuOptions[typeof(SResolution)] as SResolution).GetValue();
    }
}
