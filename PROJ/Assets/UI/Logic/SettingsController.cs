using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] private SettingsData userSettings;

    [SerializeField] private List<MenuSettings> settingObjects;

    
    private void Start() {

        foreach (MenuSettings menuSettings in settingObjects)
            menuSettings.Initialize();
        
        LoadSavedSettings();
        UpdateUserSettings();
        SetValues(userSettings);
        SaveSettings();
    }
    
 //   private void OnEnable() => 

    //Called from button in settings menu
    public void RestoreDefaultValues(string json) => SetValues(JsonUtility.FromJson<SettingsData>(json));
    
    //Called from button in settings menu
    public void SaveSettings() {
        UpdateUserSettings();
        EventHandler<SaveSettingsEvent>.FireEvent(new SaveSettingsEvent(userSettings));
    }
    
    private void LoadSavedSettings()
    {
        string json = PlayerPrefs.GetString("SavedSettings");

        //If PlayerPrefs have no settings, read from DefaultSettings file
        if (json == "")
        {
            Debug.Log("json is empty");
            string path = Path.Combine(Application.streamingAssetsPath, "DefaultSettings.json");
            using (StreamReader streamReader = new StreamReader(path))
            {
                json = streamReader.ReadToEnd();
                RestoreDefaultValues(json);
                SaveSettings();
            }
        }
        SettingsData savedSettings = JsonUtility.FromJson<SettingsData>(json);
        userSettings = savedSettings;
        SetValues(savedSettings);
    }
    
    private void UpdateUserSettings() {


       /* userSettings.musicVolume = GetMenuItem("Music").GetValue();
        userSettings.voiceVolume = GetMenuItem("Voice").GetValue();
        userSettings.soundEffectsVolume = GetMenuItem("SFX").GetValue();
        userSettings.mute = GetMenuItem("Mute").GetValue();
        userSettings.highContrastMode = GetMenuItem("Use_HighContrastMode").GetValue();

        //Ease of use
        //userSettings.fontSize = (int)fontSize.value;
        //userSettings.pointerSize = pointerSize.value;
        //userSettings.showDesktop = showDesktop.isOn;
        //userSettings.blindMode = blindMode.isOn;

        //Display                    
        userSettings.fieldOfView = GetMenuItem("Field of View").GetValue();
        userSettings.brightness = GetMenuItem("Brightness").GetValue();
        //quality = settings.Quality;
        //resolution  = settings.
        userSettings.fullscreen = GetMenuItem("Fullscreen").GetValue();
        */
    }
    
    private void SetValues(SettingsData settings)
    {
        
        foreach(MenuSettings menuSettings in settingObjects)
            menuSettings.UpdateSettings(settings);

        //Display                    
        
        /*
        menuOptions[GetMenuItem("Use_HighContrastMode").ID].SetValue(settings.highContrastMode);
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;*/
            
        //Accessibility
        
    }

    //Might want to store the hashed values instead of hashing them at runtime.
}
