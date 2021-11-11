using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("DONT TOUCH")]
    [SerializeField] private GameSettings defaultSettings;
    [SerializeField] public  SettingsData userSettings;
    public static SettingsData settings;
    public static SettingsMenu settingsMenuInstance;

    //Audio
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicSliderValueText;
    
    [SerializeField] private Slider voiceSlider;
    [SerializeField] private TextMeshProUGUI voiceSliderText;
    
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxSliderText;
    [SerializeField] private Toggle mute;

    //Ease of Use
    [SerializeField] private Toggle blindMode;
    [SerializeField] private Toggle highContrastMode;

    //Display
    [SerializeField] private Slider fieldOfView;
    [SerializeField] private TextMeshProUGUI fieldOfViewSliderText;
    
    [SerializeField] private Slider brightness;
    [SerializeField] private TextMeshProUGUI brightnessSliderText
        ;
    [SerializeField] private TMP_Dropdown quality;
    //[SerializeField]private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;


    private void Awake()
    {
        settingsMenuInstance = this;

        musicSlider.onValueChanged.AddListener(newValue => {
            musicSlider.value = newValue;
            musicSliderValueText.text = ((int)(newValue)).ToString();
        });

        voiceSlider.onValueChanged.AddListener(newValue => {
            voiceSlider.value = newValue;
            voiceSliderText.text = ((int)(newValue)).ToString();
        });
        
        sfxSlider.onValueChanged.AddListener(newValue => {
            sfxSlider.value = newValue;
            sfxSliderText.text = ((int)(newValue)).ToString();
        });
        
        fieldOfView.onValueChanged.AddListener(newValue => {
            fieldOfView.value = newValue;
            fieldOfViewSliderText.text = ((int)(newValue)).ToString();
        });
        
        brightness.onValueChanged.AddListener(newValue => {
            brightness.value = newValue;
            brightnessSliderText.text = ((int)(newValue)).ToString();
        });
        
        
    }
    
    private void OnEnable()
    {

        Debug.Log("Settings menu on enable");
        LoadSavedSettings();
        
    }
    
    //Called from button in settings menu
    public void RestoreDefaultValues()
    {
        //copy default values into user values
        SetValues(defaultSettings);
    }
    //Called from button in settings menu
    public void SaveSettings()
    {
        UpdateUserSettings();
        EventHandler<SaveSettingsEvent>.FireEvent(new SaveSettingsEvent(userSettings));
        Debug.Log("Fired save settings event");
    }
    private void LoadSavedSettings()
    {
        string json = PlayerPrefs.GetString("SavedSettings");

        if (json == "")
        {
            RestoreDefaultValues();
            SaveSettings();
            Debug.Log("Loaded default settings");
            return;
        }
        Debug.Log(json);
        SettingsData savedSettings = JsonUtility.FromJson<SettingsData>(json);
        userSettings = savedSettings;
        Debug.Log("Saved settings music volume: " + savedSettings.musicVolume);
        SetValues(savedSettings);
    }
  
    private void UpdateUserSettings()
    {
        //Audio
        userSettings.musicVolume = musicSlider.value;
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
        userSettings.fullscreen = fullscreen.isOn;
    }
    private void SetValues(SettingsData settings)
    {
        //Audio
        musicSlider.value = settings.musicVolume;
        voiceSlider.value = settings.voiceVolume;
        sfxSlider.value = settings.soundEffectsVolume;
        mute.isOn = settings.mute;
        Debug.Log("Set values, Music slider is + " + musicSlider.value + "it should be " + settings.musicVolume);

        //Ease of use
        blindMode.isOn = settings.blindMode;
        highContrastMode.isOn = settings.highContrastMode;

        //Display                    
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;
    }
    private void SetValues(GameSettings settings)
    {
        Debug.Log("WRong set values");
        //Audio
        musicSlider.value = settings.musicVolume;
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
        fullscreen.isOn = settings.fullscreen;
    }

    //How would we like to store the default values as json? 
    //Using existing scriptable object meanwhile
    private void SetDefaultValues()
    {
        string json = PlayerPrefs.GetString("DefaultSettings");
        SettingsData settings = JsonUtility.FromJson<SettingsData>(json);
        //SetValues(settings);
    }
    
}

