using UnityEngine;

public class VolumeController : PersistentSingleton<VolumeController>
{
 
    float masterVolume, musicVolume, soundEffectsVolume, voiceVolume;

    private FMOD.Studio.VCA VcaController;
    
    private void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/Master");       
    }

}
