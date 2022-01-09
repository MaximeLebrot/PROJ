using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FadeGroup : MonoBehaviour {

    [Header("FADE IN occurs from index 0-n | FADE OUT occurs from index n-0")]
    [SerializeField] private List<FadeEntity> fadeOrder;

    private void OnDisable() => SetAlpha(0);


    public void SetAlpha(float value) {
        
        foreach (FadeEntity fadeEntity in fadeOrder)
            fadeEntity.CanvasGroup.alpha = value;
        
    }
    
    public async Task InitiateFade(FadeMode fadeMode) => await Fade(fadeMode);

    private async Task Fade(FadeMode fadeMode) {
        
        foreach (FadeEntity entity in fadeOrder) {
            
            switch (fadeMode) {
                case FadeMode.FadeIn:
                    await Fade(entity, 0, 1);
                    break;
                
                case FadeMode.FadeOut:
                    await Fade(entity, 1, 0);
                    break;
            }
            
        }
        
    }

    private async Task Fade(FadeEntity fadeEntity, float startValue, float endValue) {
        fadeEntity.CanvasGroup.alpha = startValue;

        float endTime = Time.time + fadeEntity.FadeTime;
        float lerpValue = 0;
        
        while (endTime > Time.time) {
            
            fadeEntity.CanvasGroup.alpha = Mathf.Lerp(startValue, endValue, lerpValue);
                
            lerpValue += Time.deltaTime / fadeEntity.FadeTime;    
            
            await Task.Yield();
        }

        fadeEntity.CanvasGroup.alpha = endValue;
        
    }
    
    
  /*  //Har viktigare saker för mig än att vara effektiv. 
    private async Task FadeIn(FadeEntity fadeEntity) {
        fadeEntity.CanvasGroup.alpha = 0;

        float endTime = Time.time + fadeEntity.FadeTime;
        float lerpValue = 0;
        
        while (endTime > Time.time) {
            fadeEntity.CanvasGroup.alpha = Mathf.Lerp(0, 1, lerpValue);
                
            lerpValue += Time.deltaTime / fadeEntity.FadeTime;    
            
            await Task.Yield();
        }

        fadeEntity.CanvasGroup.alpha = 1;
    }

    private async Task FadeOut(FadeEntity fadeEntity) {

        fadeEntity.CanvasGroup.alpha = 1;

        float endTime = Time.time + fadeEntity.FadeTime;
        float lerpValue = 0;
        
        while (endTime > Time.time) {
            fadeEntity.CanvasGroup.alpha = Mathf.Lerp(1, 0, lerpValue);
                
            lerpValue += Time.deltaTime / fadeEntity.FadeTime;

            await Task.Yield();
        }

        fadeEntity.CanvasGroup.alpha = 0;

    }
    */

}
