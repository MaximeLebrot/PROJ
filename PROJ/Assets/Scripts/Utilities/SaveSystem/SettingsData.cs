using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SettingsData
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
    public bool highContrastMode;
    

    //Display
    public float fieldOfView;
    public float brightness;
    //public GraphicsQuality Quality;
    public bool fullscreen;
    //private Resolution screenRes? 
}

[Serializable]
public class SavedSettings
{
    public SettingsData settingsData; 
}