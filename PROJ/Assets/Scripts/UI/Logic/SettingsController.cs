using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] private SettingsData userSettings;
    [SerializeField] private List<MenuSettings> settingObjects;

    private const string JSONFileName = "SavedSettings";

   
    private void Awake() {
        LoadSavedSettings();
        EventHandler<RequestSettingsEvent>.RegisterListener(SendOutUserSettingsData);
    }

    private void OnDisable() => EventHandler<RequestSettingsEvent>.UnregisterListener(SendOutUserSettingsData);
    
    
    //Called from button in settings menu
    public void RestoreDefaultValues(string json, bool fireSaveEvent) {
        SetValues(JsonUtility.FromJson<SettingsData>(json));
        
        if(fireSaveEvent)
            SendOutUserSettingsData(null);
    }

    //Called from button in settings menu
    public void SaveSettings(bool fireSaveEvent) {
        UpdateUserSettings();
        
        if(fireSaveEvent)
            SendOutUserSettingsData(null);
        
    }

    private void SendOutUserSettingsData(RequestSettingsEvent requestSettingsEvent) {
        EventHandler<SaveSettingsEvent>.FireEvent(new SaveSettingsEvent(userSettings));
    }

    private void LoadSavedSettings()
    {
        string json = PlayerPrefs.GetString("SavedSettings");
        
        //If PlayerPrefs have no settings, read from DefaultSettings file
        if (json == "")
        {
            string path = Path.Combine(Application.streamingAssetsPath, "DefaultSettings.json");
            StreamReader streamReader = new StreamReader(path);
            
            json = File.ReadAllText(path);
            
             //= streamReader.ReadToEnd();
            RestoreDefaultValues(json, false);
            PlayerPrefs.SetString("SavedSettings", json);
            
        }
        
        SettingsData savedSettings = JsonUtility.FromJson<SettingsData>(json);
        userSettings = savedSettings;
        SetValues(savedSettings);
        SaveSettings(true);
    }
    
    private void UpdateUserSettings() {
        foreach(MenuSettings menuSettings in settingObjects)
            menuSettings.ExtractValues(ref userSettings);
    }

    private void SetValues(SettingsData settings) {

        foreach(MenuSettings menuSettings in settingObjects)
            menuSettings.SetMenuItems(userSettings);
    }
    
}
