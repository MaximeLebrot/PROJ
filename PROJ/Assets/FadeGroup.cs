using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGroup : MonoBehaviour {

    [Header("FADE IN occurs from index 0-n | FADE OUT occurs from index n-0")]
    [SerializeField] private List<FadeEntity> fadeOrder;
    
    public IEnumerator Fade(FadeMode fadeMode, Action callback) {
        
        foreach (FadeEntity entity in fadeOrder) {
            
            switch (fadeMode) {
                case FadeMode.FadeIn:
                    StartCoroutine(FadeIn(entity));
                    break;
                
                case FadeMode.FadeOut:
                    StartCoroutine(FadeOut(entity));
                    break;
            }
            yield return new WaitForSeconds(entity.TimeUntilNextFade);
        }

        if(callback != null)
            callback.Invoke();
    }
    
    private IEnumerator FadeIn(FadeEntity fadeEntity) {

        fadeEntity.CanvasGroup.alpha = 0;
        
        while (fadeEntity.CanvasGroup.alpha < (int)FadeMode.FadeIn) {
            fadeEntity.CanvasGroup.alpha += Time.deltaTime / fadeEntity.FadeTime;
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(FadeEntity fadeEntity) {
        
        fadeEntity.CanvasGroup.alpha = 1;
        
        while (fadeEntity.CanvasGroup.alpha > (int)FadeMode.FadeOut) {
            fadeEntity.CanvasGroup.alpha -= Time.deltaTime / fadeEntity.FadeTime;
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 0;
    }

}
