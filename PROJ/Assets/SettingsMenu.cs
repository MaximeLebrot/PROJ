using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Tooltip("DONT TOUCH")]
    [SerializeField] private GameSettings defaultSettings;
    [SerializeField] public  GameSettings userSettings;
    public static GameSettings settings;
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
       fieldOfView.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }
    private void OnDisable()
    {
        fieldOfView.onValueChanged.RemoveListener(delegate { OnValueChanged(); });
    }
    public void OnValueChanged()
    {
        //Temporary code to not break the FoV slider before the event and listeners are implemented
        userSettings.fieldOfView = fieldOfView.value;
        Camera.main.fieldOfView = fieldOfView.value;

        //assign values to user settings - settings object are only used for storing data between sessions.

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
       // fontSize.value = settings.fontSize;
       // pointerSize.value = settings.pointerSize;
        //showDesktop.isOn = settings.showDesktop;
        blindMode.isOn = settings.blindMode;

        //Display                    
        fieldOfView.value = settings.fieldOfView;
        brightness.value = settings.brightness;
        //quality = settings.Quality;
        //resolution  = settings.
        fullscreen.isOn = settings.fullscreen;
    }


}
