using UnityEngine;

public class VolumeController : MonoBehaviour
{
 
    float masterVolume, musicVolume, soundEffectsVolume, voiceVolume;

    private FMOD.Studio.VCA VcaController;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/Master");

        (GameMenuController.Instance.RequestOption<MasterVolume>() as MasterVolume).AddListener(SetMasterVolume);
        (GameMenuController.Instance.RequestOption<MusicVolume>() as MusicVolume).AddListener(SetMusicVolume);
        (GameMenuController.Instance.RequestOption<SFXVolume>() as SFXVolume).AddListener(SetSFXVolume);
    }

    private void SetMasterVolume(float val)
    {
        masterVolume = val;
        VcaController.setVolume(masterVolume);
    }
    private void SetMusicVolume(float val)
    {
        musicVolume = val; 
    }
    private void SetSFXVolume(float val)
    {
       soundEffectsVolume = val;
    }
    private void SetVoiceVolume(float val)
    {
        voiceVolume = val;
    }


    #region Legacy listener
    /*
    private void OnEnable()
    {
        EventHandler<SaveSettingsEvent>.RegisterListener(UpdateVolume);
    }

    private void OnDisable()
    {
        EventHandler<SaveSettingsEvent>.UnregisterListener(UpdateVolume);
    }

    private void UpdateVolume(SaveSettingsEvent eve)
    {
        //Debug.Log("Update Volume called with master vol: " + eve.settingsData.masterVolume);
        masterVolume = eve.settingsData.masterVolume;
        musicVolume = eve.settingsData.musicVolume;
        soundEffectsVolume = eve.settingsData.soundEffectsVolume;
        voiceVolume = eve.settingsData.voiceVolume;

        VcaController.setVolume(masterVolume);
    }*/
    #endregion
}
