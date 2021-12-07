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
    }
    
    //Called from button in settings menu
    private void RestoreDefaultValues(string json) {
        SetValues(JsonUtility.FromJson<SettingsData>(json));
    }

    //Called from button in settings menu
    private void SaveSettings() {
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
                
                Debug.Log(json);
                SaveSettings();
            }
        }
        SettingsData savedSettings = JsonUtility.FromJson<SettingsData>(json);
        userSettings = savedSettings;
        SetValues(savedSettings);
    }
    
    private void UpdateUserSettings() {

        foreach(MenuSettings menuSettings in settingObjects)
            menuSettings.ExtractMenuItemValues(ref userSettings);
    }

    private void SetValues(SettingsData settings) {

        foreach (MenuSettings menuSettings in settingObjects)
            menuSettings.UpdateMenuItems(settings);
    }
}
