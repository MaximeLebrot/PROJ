using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGroup : MonoBehaviour {

    [Header("FADE IN occurs from index 0-n | FADE OUT occurs from index n-0")]
    [SerializeField] private List<FadeEntity> fadeOrder;

    private bool isRunning;
    
    public void InitiateFade(FadeMode fadeMode, Action onDone) {
        
        if(isRunning)
            StopCoroutine("Fade");
      
        StartCoroutine(Fade(fadeMode, onDone));
    }

    private IEnumerator Fade(FadeMode fadeMode, Action onDone) {

        isRunning = true;
        
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
        
        onDone?.Invoke();
        isRunning = false;
    }

    
    //Har viktigare saker för mig än att vara effektiv. 
    private IEnumerator FadeIn(FadeEntity fadeEntity) {
        fadeEntity.CanvasGroup.interactable = false;
        fadeEntity.CanvasGroup.alpha = 0;

        float endTime = Time.time + fadeEntity.FadeTime;
        float lerpValue = 0;
        
        while (endTime > Time.time) {
            fadeEntity.CanvasGroup.alpha = Mathf.Lerp(0, 1, lerpValue);
                
            lerpValue += Time.deltaTime / fadeEntity.FadeTime;    
            
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 1;
        fadeEntity.CanvasGroup.interactable = true;
    }

    private IEnumerator FadeOut(FadeEntity fadeEntity) {
        fadeEntity.CanvasGroup.interactable = false;
        fadeEntity.CanvasGroup.alpha = 1;

        float endTime = Time.time + fadeEntity.FadeTime;
        float lerpValue = 0;
        
        while (endTime > Time.time) {
            fadeEntity.CanvasGroup.alpha = Mathf.Lerp(1, 0, lerpValue);
                
            lerpValue += Time.deltaTime / fadeEntity.FadeTime;    
            
            yield return null;
        }

        fadeEntity.CanvasGroup.alpha = 0;
        fadeEntity.CanvasGroup.interactable = true;
    }

}
