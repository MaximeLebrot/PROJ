using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] private SettingsData userSettings;
    [SerializeField] private List<MenuSettings> settingObjects;

    private void Awake() => EventHandler<RequestSettingsEvent>.RegisterListener(SendOutUserSettingsData);

    private void OnDisable() => EventHandler<RequestSettingsEvent>.UnregisterListener(SendOutUserSettingsData);

    private void Start() {

        foreach (MenuSettings menuSettings in settingObjects)
            menuSettings.Initialize();
        
        LoadSavedSettings();
    }
    
    //Called from button in settings menu
    public void RestoreDefaultValues(string json) {
        SetValues(JsonUtility.FromJson<SettingsData>(json));
        SendOutUserSettingsData(null);
    }

    //Called from button in settings menu
    public void SaveSettings() {
        UpdateUserSettings();
        SendOutUserSettingsData(null);
    }

    private void SendOutUserSettingsData(RequestSettingsEvent requestSettingsEvent) => EventHandler<SaveSettingsEvent>.FireEvent(new SaveSettingsEvent(userSettings));

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

        foreach(MenuSettings menuSettings in settingObjects)
            menuSettings.ApplyItemValues(ref userSettings);
    }

    private void SetValues(SettingsData settings) {

        foreach (MenuSettings menuSettings in settingObjects)
            menuSettings.SetMenuItems(settings);
    }
}
