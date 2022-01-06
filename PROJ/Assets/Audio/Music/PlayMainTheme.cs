using UnityEngine;

public class PlayMainTheme : MonoBehaviour
{
    //[SerializeField] private AudioClip theme;
    //SerializeField] private AudioSource source;

    private FMOD.Studio.EventInstance cutsceneTheme;
    //private void Awake()
    //{
    //    if (source == null)
    //        source = GetComponent<AudioSource>();
    //}

    public void PlayTheme()
    {
        //    source.PlayOneShot(theme);
        cutsceneTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Cutscene/Aria Main theme startscreen");
        cutsceneTheme.start();
        cutsceneTheme.release();
    }

    public void StopTheme()
    {
        cutsceneTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        cutsceneTheme.release();
    }
}
