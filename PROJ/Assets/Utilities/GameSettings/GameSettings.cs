using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    //Unsure if this will be used, i guess there are just settings presets? 
    public enum GraphicsQuality
    {
        Ultra,
        High,
        Medium,
        Low
    }

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
    public GraphicsQuality Quality;
    public bool fullscreen;
    //private Resolution screenRes? 
}
