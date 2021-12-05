using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] private SettingsData userSettings;

    [SerializeField] private List<GameObject> settingObjects;
    
    private List<UIMenuItem> menuItems;

    private void Awake() {

        try {
            foreach (GameObject settingsObject in settingObjects) {
                settingsObject.SetActive(true);
                menuItems.AddRange(settingsObject.GetComponentsInChildren<UIMenuItem>());
                settingsObject.SetActive(false);
            }
            
            foreach(UIMenuItem menuItem in menuItems)
                Debug.Log($"{menuItem.name} is {menuItem.GetValue()}");
        }
        catch (NullReferenceException e) {
            Debug.Log(e.StackTrace);
        }
        
        
        
       
        
    }
    
    private void OnEnable() => LoadSavedSettings();

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
    
    private void UpdateUserSettings()
    {
        //Audio
         /*userSettings.musicVolume = musicSlider.value;
        userSettings.voiceVolume = voiceSlider.value;
        userSettings.soundEffectsVolume = sfxSlider.value;
        userSettings.mute = mute.isOn;
        userSettings.highContrastMode = highContrastMode.isOn;

        //Ease of use
        //userSettings.fontSize = (int)fontSize.value;
        //userSettings.pointerSize = pointerSize.value;
        //userSettings.showDesktop = showDesktop.isOn;
        //userSettings.blindMode = blindMode.isOn;

        //Display                    
        userSettings.fieldOfView = fieldOfView.value;
        userSettings.brightness = brightness.value;
        //quality = settings.Quality;
        //resolution  = settings.
        userSettings.fullscreen = fullscreen.isOn;*/
    }
    
    private void SetValues(SettingsData settings)
    {
        //Audio
        /*musicSlider.value = settings.musicVolume;
        voiceSlider.value = settings.voiceVolume;
        sfxSlider.value = settings.soundEffectsVolume;
        mute.isOn = settings.mute;

        //Ease of use
        blindMode.isOn = settings.blindMode;
        highContrastMode.isOn = settings.highContrastMode;

        //Display                    
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;*/
    }
    
}
