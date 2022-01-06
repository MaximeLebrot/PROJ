using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScribble : MonoBehaviour
{
    private FMOD.Studio.EventInstance PageScribbleSound;
    public void PlayScribbleSound()
    {
        PageScribbleSound = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/PageScribble");
        PageScribbleSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        PageScribbleSound.start();
        PageScribbleSound.release();
    }
}
