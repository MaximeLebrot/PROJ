using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGroup : MonoBehaviour {

    [Header("FADE IN occurs from index 0-n | FADE OUT occurs from index n-0")]
    [SerializeField] private List<FadeEntity> fadeOrder;
    
    public IEnumerator Fade(FadeMode fadeMode, Action onDone) {

        float totalTime = 0;

        foreach (FadeEntity fadeEntity in fadeOrder)
            totalTime += fadeEntity.FadeTime;

        float endTime = Time.time + totalTime;
        float currentTime = Time.time;
        
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

        while (currentTime < endTime) {
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        if (onDone != null)
            onDone?.Invoke();
    }
    
    private IEnumerator FadeIn(FadeEntity fadeEntity) {
        fadeEntity.CanvasGroup.interactable = false;
        fadeEntity.CanvasGroup.alpha = 0;
        
        while (fadeEntity.CanvasGroup.alpha < (int)FadeMode.FadeIn) {
            fadeEntity.CanvasGroup.alpha += Time.deltaTime / fadeEntity.FadeTime;
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 1;
        fadeEntity.CanvasGroup.interactable = true;
    }

    private IEnumerator FadeOut(FadeEntity fadeEntity) {
        fadeEntity.CanvasGroup.interactable = false;
        fadeEntity.CanvasGroup.alpha = 1;
        
        while (fadeEntity.CanvasGroup.alpha > (int)FadeMode.FadeOut) {
            fadeEntity.CanvasGroup.alpha -= Time.deltaTime / fadeEntity.FadeTime;
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 0;
        fadeEntity.CanvasGroup.interactable = true;
    }

}
