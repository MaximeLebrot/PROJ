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
    [SerializeField] private Slider voiceSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle mute;

    //Ease of Use
    [SerializeField] private Slider fontSize;
    [SerializeField] private Slider pointerSize;
    [SerializeField] private Toggle showDesktop;
    [SerializeField] private Toggle blindMode;

    //Display
    [SerializeField] private Slider fieldOfView;
    [SerializeField] private Slider brightness;
    [SerializeField] private TMP_Dropdown quality;
    //[SerializeField]private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;


    private void Awake()
    {
        settingsMenuInstance = this;
        settings = userSettings;

        SetValues(userSettings);
        Camera.main.fieldOfView = fieldOfView.value;
    }
    private void OnEnable()
    {
        musicSlider.onValueChanged.AddListener(delegate { OnValueChanged();});
        voiceSlider.onValueChanged.AddListener(delegate { OnValueChanged();});
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged();});
        mute.onValueChanged.AddListener(delegate { OnValueChanged();});

        fontSize.onValueChanged.AddListener(delegate { OnValueChanged();});
        pointerSize.onValueChanged.AddListener(delegate { OnValueChanged();});
        showDesktop.onValueChanged.AddListener(delegate { OnValueChanged(); });
        blindMode.onValueChanged.AddListener(delegate { OnValueChanged(); });

        fieldOfView.onValueChanged.AddListener(delegate { OnValueChanged();});
        brightness.onValueChanged.AddListener(delegate { OnValueChanged(); });
        quality.onValueChanged.AddListener(delegate { OnValueChanged(); });
        fullscreen.onValueChanged.AddListener(delegate { OnValueChanged(); });

    }
    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        voiceSlider.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        sfxSlider.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        mute.onValueChanged.RemoveListener(delegate { OnValueChanged(); });

        fontSize.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        pointerSize.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        showDesktop.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        blindMode.onValueChanged.RemoveListener(delegate { OnValueChanged(); });

        fieldOfView.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        brightness.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        quality.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        fullscreen.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
        fieldOfView.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
    }
    public void OnValueChanged()
    {
        //Temporary code to not break the FoV slider before the event and listeners are implemented
        userSettings.fieldOfView = fieldOfView.value;
        Camera.main.fieldOfView = fieldOfView.value;
        
        //assign values to user settings - settings object are only used for storing data between sessions.
        StoreValues();

        //Create and fire SettingsChangedEvent (AudioHandler, anything that uses fontSize etc needs to listen for this event

        //EventHandler<SettingsChangedEvent>.FireEvent(new SettingsChangedEvent(userSettings));
    }

    //This will need a button if we want to implement it
    public void RestoreDefaultValues()
    {
        //copy default values into user values
        
        //SetValues();
    }
    private void SetValues(GameSettings settings)
    {
        //Audio
        musicSlider.value = settings.musicVolume;
        voiceSlider.value = settings.voiceVolume;
        sfxSlider.value = settings.soundEffectsVolume;
        mute.isOn = settings.mute;

        //Ease of use
        fontSize.value = settings.fontSize;
        pointerSize.value = settings.pointerSize;
        showDesktop.isOn = settings.showDesktop;
        blindMode.isOn = settings.blindMode;

        //Display                    
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;
    }
    private void StoreValues()
    {
        //Audio
        userSettings.musicVolume = musicSlider.value;
        userSettings.voiceVolume = voiceSlider.value;
        userSettings.soundEffectsVolume = sfxSlider.value;
        userSettings.mute = mute.isOn;

        //Ease of use
        userSettings.fontSize = (int)fontSize.value;
        userSettings.pointerSize = pointerSize.value;
        userSettings.showDesktop = showDesktop.isOn;
        userSettings.blindMode = blindMode.isOn;

        //Display                    
        userSettings.fieldOfView = fieldOfView.value;
        userSettings.brightness = brightness.value;
        //quality = settings.Quality;
        //resolution  = settings.
        userSettings.fullscreen = fullscreen.isOn;
    }

}
    [Serializable]
    public struct SettingsData
{
    //Audio
    public float musicVolume;
    public float voiceVolume;
    public float soundEffectsVolume;
    public bool mute;

    //Easy of Access
    public int fontSize;
    public float pointerSize;
    public bool showDesktop; //What is this? 
    public bool blindMode;

    //Display
    public float fieldOfView;
    public float brightness;
    //public GraphicsQuality Quality;
    public bool fullscreen;
    //private Resolution screenRes? 

}
