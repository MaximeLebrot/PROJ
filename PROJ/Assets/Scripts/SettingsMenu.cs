using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("DONT TOUCH")]
    [SerializeField] public  SettingsData userSettings;
    public static SettingsData settings;
    public static SettingsMenu settingsMenuInstance;

    #region Compnent References
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
    [SerializeField] private TextMeshProUGUI brightnessSliderText;
    [SerializeField] private TMP_Dropdown quality;
    //[SerializeField]private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;

    //Puzzle Settings
    /*
    [SerializeField] private Toggle currentNodeMarker;
    [SerializeField] private Slider nodeSize;
    [SerializeField] private TextMeshProUGUI nodeSizeText;
    [SerializeField] private Slider lineSize;
    [SerializeField] private TextMeshProUGUI lineSizeText;
    [SerializeField] private Toggle animatedNodes;
    [SerializeField] private Toggle animatedLines;
    [SerializeField] private Toggle showClearedSymbols;
    [SerializeField] private Toggle easyPuzzleControls;
    */
    #endregion

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

        /*
        nodeSize.onValueChanged.AddListener(newValue => {
            nodeSize.value = newValue;
            nodeSizeText.text = ((int)(newValue)).ToString();
        });

        lineSize.onValueChanged.AddListener(newValue => {
            lineSize.value = newValue;
            lineSizeText.text = ((int)(newValue)).ToString();
        });
        */
    }
    
    private void OnEnable()
    {
        LoadSavedSettings();        
    }
    
    //Called from button in settings menu
    public void RestoreDefaultValues(string json)
    {
        SetValues(JsonUtility.FromJson<SettingsData>(json));
    }
    //Called from button in settings menu
    public void SaveSettings()
    {
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


        //PuzzleSettings
        /*
        userSettings.nodeSize = nodeSize.value;
        userSettings.lineSize = lineSize.value;
        userSettings.animatedLines = animatedLines.isOn;
        userSettings.animatedNodes = animatedNodes.isOn;
        userSettings.currentNodeMarker = currentNodeMarker.isOn;
        userSettings.showClearedSymbols = showClearedSymbols.isOn;
        userSettings.easyPuzzleControls = easyPuzzleControls.isOn;
        */
    }
    private void SetValues(SettingsData settings)
    {
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

        //PuzzleSettings
        /*
        nodeSize.value = settings.nodeSize;
        lineSize.value = settings.lineSize;
        animatedLines.isOn = settings.animatedLines;
        animatedNodes.isOn = settings.animatedNodes;
        currentNodeMarker.isOn = settings.currentNodeMarker;
        showClearedSymbols.isOn = settings.ShowClearedSymbols;
        easyPuzzleControls.isOn = settings.easyPuzzleControls;
        */
    }

}

