using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public enum ButtonState {
    
    Inactive = 0,
    NotSelected = -450,
    Selected = -400,
    
} 

public class UIButton : MonoBehaviour {

    private static float moveDuration = .2f;
    private ButtonState state;
    
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private double timeUntilMove;

    [SerializeField] private MenuSettings associatedMenuSetting;
    public MenuSettings AssociatedMenuSetting => associatedMenuSetting;
    
    
    private RectTransform rectTransform;
    
    private void OnEnable() {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.localPosition = Vector2.zero;
        
        state = ButtonState.Inactive;
    }

    public void SetState(ButtonState newState) => state = newState;
    
    private void OnDisable() => GetComponent<CanvasGroup>().alpha = 0;
    
    public async Task Move() {
        
        await Task.Delay(TimeSpan.FromSeconds(timeUntilMove));
        
        Vector2 start = rectTransform.anchoredPosition;
        Vector2 end = new Vector2((int)state, rectTransform.anchoredPosition.y);

        float endTime = Time.time + moveDuration;

        float lerp = 0;
        
        while (Time.time <= endTime) {
            
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, movementCurve.Evaluate(lerp));
            
            lerp += Time.deltaTime / moveDuration;
            
            await Task.Yield();
        }
        
    }
}


