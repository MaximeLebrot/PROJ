using System;
using UnityEngine;

public enum FadeMode {
    FadeOut = 0,
    FadeIn = 1
}

[Serializable]
public struct FadeEntity {

    public CanvasGroup CanvasGroup;
    public float FadeTime;
    public float TimeUntilNextFade;
    
}
