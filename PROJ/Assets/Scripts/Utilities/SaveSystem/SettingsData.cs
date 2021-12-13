using UnityEngine;
using System;

[Serializable]
public class SettingsData
{
    //General
    public float mouseSensitivity;
    public bool holdToSprint;
    public bool pressToSprint;
    
    //Audio
    public float masterVolume;
    public float musicVolume;
    public float voiceVolume;
    public float soundEffectsVolume;
    public bool mute;
    public float ambience;
    
    //Display
    public float fieldOfView;
    public float brightness;
    public bool fullscreen;
    public Resolution screenResolution;
    public string quality;
    
    //Accessibility
    public int fontSize;
    public bool blindMode;
    public bool highContrastMode;
    public bool dyslexiaFont;
    public bool oneHandMode;
    public bool bigNodes;
    public bool currentNodeMarker; 
    public bool showClearedSymbols; //CHECK
    public bool easyPuzzleControls;
    public string symbolDifficulty; //CHECK
    
}

[Serializable]
public class SavedSettings
{
    public SettingsData settingsData; 
}