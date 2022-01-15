using UnityEngine;
using System;

[Serializable]
public class SettingsData
{
    //General
    public float mouseSensitivity;
    public string sprintMode;

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
    public string screenResolution;
    public string quality;
    
    //Accessibility
    public string fontSize;
    public string controlMode;
    public bool blindMode;
    public bool highContrastMode;
    public bool dyslexiaFont;
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