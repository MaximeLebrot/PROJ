using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] private SettingsData userSettings;

    [SerializeField] private List<GameObject> settingObjects;

    private Dictionary<int, UIMenuItem> menuOptions;
    
    private void Start() {
        menuOptions = new Dictionary<int, UIMenuItem>();
        
        foreach (GameObject settingsObject in settingObjects) {

            if(settingsObject.activeInHierarchy == false)
                    settingsObject.SetActive(true);

            List<UIMenuItem> menuItems = settingsObject.GetComponent<UIMenuManager>().GetMenuItems();

            foreach (UIMenuItem item in menuItems) 
                menuOptions.Add(item.ID, item);
            
            settingsObject.SetActive(false);
        }
        
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


        userSettings.musicVolume = GetMenuItem("Music").GetValue();
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
    }
    
    private void SetValues(SettingsData settings)
    {
        //Audio
        menuOptions[GetMenuItem("Mute").ID].SetValue(settings.mute);
        menuOptions[GetMenuItem("Master").ID].SetValue(settings.masterVolume);
        menuOptions[GetMenuItem("Music").ID].SetValue(settings.musicVolume);
        menuOptions[GetMenuItem("Ambience").ID].SetValue(settings.ambience);
        menuOptions[GetMenuItem("SFX").ID].SetValue(settings.soundEffectsVolume);
        menuOptions[GetMenuItem("Voice").ID].SetValue(settings.voiceVolume);
        
        
        
        /*blindMode.isOn = settings.blindMode;
        highContrastMode.isOn = settings.highContrastMode;
        */
        
        //Display                    
        menuOptions[GetMenuItem("Field of View").ID].SetValue(settings.fieldOfView);
        menuOptions[GetMenuItem("Brightness").ID].SetValue(settings.brightness);
        menuOptions[GetMenuItem("Quality").ID].SetValue(settings.quality);
        menuOptions[GetMenuItem("Fullscreen").ID].SetValue(settings.fullscreen);
        menuOptions[GetMenuItem("Resolution").ID].SetValue(settings.screenResolution);
        
        /*
        menuOptions[GetMenuItem("Use_HighContrastMode").ID].SetValue(settings.highContrastMode);
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;*/
            
        //Accessibility
        menuOptions[GetMenuItem("ChangeFontSize").ID].SetValue(settings.fontSize);
        menuOptions[GetMenuItem("Use_DyslexiaFont").ID].SetValue(settings.dyslexiaFont);
        menuOptions[GetMenuItem("Use_HighContrastMode").ID].SetValue(settings.highContrastMode);
        menuOptions[GetMenuItem("BlindMode").ID].SetValue(settings.blindMode);
    }

    //Might want to store the hashed values instead of hashing them at runtime.
    private UIMenuItem GetMenuItem(string menuName) => menuOptions[menuName.GetHashCode()];
}
