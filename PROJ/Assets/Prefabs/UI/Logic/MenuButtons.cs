using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtons : MenuSettings {

    [SerializeField] private AnimationCurve moveCurve;

    [SerializeField] private List<RectTransform> buttonRects;
    
    public override void SetMenuItems(SettingsData settingsData) {}

    public override void ApplyItemValues(ref SettingsData settingsData) {}

    public void MoveToSide(float horizontalEndValue) {
        StartCoroutine(MoveSequence(horizontalEndValue));
    }

    private IEnumerator MoveSequence(float horizontalEndValue) {

        IEnumerator pointer = buttonRects.GetEnumerator();
        
        Debug.Log(pointer);

        while(pointer.MoveNext()){
            
            StartCoroutine(Move_Coroutine(pointer.Current as RectTransform, horizontalEndValue, 1));

            yield return new WaitForSeconds(.1f);
        }
        
    }
    
    private IEnumerator Move_Coroutine(RectTransform item, float target, float duration) {
        
        Vector2 start = item.anchoredPosition;
        Vector2 end = item.anchoredPosition + (Vector2.left * target); 

        float endTime = Time.time + duration;

        float lerp = 0;
        
        while (Time.time <= endTime) {
            
            item.anchoredPosition = Vector2.Lerp(start, end, moveCurve.Evaluate(lerp));
            
            lerp += Time.deltaTime / duration;
            
            yield return null;
        }        

    }
    
}
