using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScribble : MonoBehaviour
{
    private FMOD.Studio.EventInstance PageScribbleSound;
    private FMOD.Studio.EventInstance EndMusic;
    public void PlayScribbleSound()
    {
        PageScribbleSound = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageScribble");
        PageScribbleSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageScribbleSound.start();
        PageScribbleSound.release();
    }

    public void PlayEndMusic()
    {
        EndMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Cutscene/EndCutscene");
        EndMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        EndMusic.start();
        EndMusic.release();
    }
}
