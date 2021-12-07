using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SettingsData
{
    //Audio
    public float masterVolume;
    public float musicVolume;
    public float voiceVolume;
    public float soundEffectsVolume;
    public bool mute;
    public float ambience;

    //Easy of Access
    public int fontSize;
    public float pointerSize;
    public bool showDesktop; //What is this? 
    public bool blindMode;
    public bool highContrastMode;
    

    //Display
    public float fieldOfView;
    public float brightness;
    //public GraphicsQuality Quality;
    public bool fullscreen;
    public Resolution screenResolution;
    public string quality;
    
    //private Resolution screenRes? 
    
    //Accessibility
    public bool dyslexiaFont;
}

[Serializable]
public class SavedSettings
{
    public SettingsData settingsData; 
}