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
    }

}
