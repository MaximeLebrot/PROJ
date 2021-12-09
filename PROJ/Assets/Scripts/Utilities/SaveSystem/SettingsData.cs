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
    public FullScreenMode fullscreen;
    public Resolution screenResolution;
    public string quality;
    
    //Accessibility
    public int fontSize;
    public bool blindMode;
    public bool highContrastMode;
    public bool dyslexiaFont;
    public bool oneHandMode;
    public float nodeSize;
    public float lineSize;
    public bool currentNodeMarker;
    public bool showClearedSymbols;
    public bool easyPuzzleControls;
    
}

[Serializable]
public class SavedSettings
{
    public SettingsData settingsData; 
}