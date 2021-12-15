using System;
using System.Threading.Tasks;
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

    public async Task Fade(FadeMode fadeMode) {
        
        switch (fadeMode) {
            case FadeMode.FadeOut:
                await FadeOut((float)FadeMode.FadeOut);
                break;
            
            case FadeMode.FadeIn:
                await FadeIn(((float)FadeMode.FadeIn));
                break;
        }

        await Task.Delay((int)TimeUntilNextFade * 1000);
    }
    
    public async Task FadeIn(float targetValue) {

        CanvasGroup.alpha = 0;
        
        while (CanvasGroup.alpha < targetValue) {
            Debug.Log(CanvasGroup.alpha);
            CanvasGroup.alpha += Time.deltaTime / FadeTime;
            await Task.Yield();
        }

        CanvasGroup.alpha = 1;
    }

    public async Task FadeOut(float targetValue) {
        
        CanvasGroup.alpha = 1;
        
        while (CanvasGroup.alpha > targetValue) {
            CanvasGroup.alpha -= Time.deltaTime / FadeTime;
            await Task.Yield();
        }
        
        CanvasGroup.alpha = 0;
    }
    
}
