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

        (GameMenuController.Instance.RequestOption<MasterVolume>() as MasterVolume).AddListener((value) => SetMasterVolume(value));
        (GameMenuController.Instance.RequestOption<MusicVolume>() as MusicVolume).AddListener((value) => SetMusicVolume(value));
        (GameMenuController.Instance.RequestOption<SFXVolume>() as SFXVolume).AddListener((value) => SetSFXVolume(value));
        (GameMenuController.Instance.RequestOption<VoiceVolume>() as VoiceVolume).AddListener((value) => SetVoiceVolume(value));
    }

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
}
