using UnityEngine;

public class VolumeController : MonoBehaviour
{
 
    float masterVolume, musicVolume, soundEffectsVolume, voiceVolume;

    private FMOD.Studio.VCA VcaController;

    private void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        Debug.Log(VcaController);
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
        Debug.Log("Update Volume called with master vol: " + eve.settingsData.masterVolume);
        masterVolume = eve.settingsData.masterVolume;
        musicVolume = eve.settingsData.musicVolume;
        soundEffectsVolume = eve.settingsData.soundEffectsVolume;
        voiceVolume = eve.settingsData.voiceVolume;

        VcaController.setVolume(masterVolume);
    }
}
