using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public enum ButtonState {
    
    Inactive = 0,
    NotSelected = -450,
    Selected = -400,
    
} 

public class UIButton : MonoBehaviour {

    private static float moveDuration = 1f;

    [SerializeField] private ControllerInputReference controllerInputReference;
    
    [SerializeField] private ButtonState state;
    [SerializeField] private MenuSettings menuSettings;
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float timeUntilMove;

    public MenuSettings MenuSetting => menuSettings;
    
    public delegate void OnButtonClick(UIButton pressedButton);
    public delegate void OnResetCalled();
    
    public static event OnButtonClick onButtonClicked;
    public static event OnResetCalled onResetCalled;

    public event Action onMoveCallback;
    
    private RectTransform rectTransform;
    
    private Button buttonComponent;
    
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        buttonComponent = GetComponent<Button>();

        onButtonClicked += OnAnotherButtonPressed;
        onResetCalled += ResetButton;
        state = ButtonState.Inactive;
        
    }

    private void Start() => controllerInputReference.InputMaster.Menu.performed += ResetButton;
    
    private void OnDisable() {
        controllerInputReference.InputMaster.Menu.performed -= ResetButton;
    }

    private void Move(float duration) {
        if (Math.Abs(rectTransform.anchoredPosition.x - (int)state) < .01f)
            return;
        
        StartCoroutine(Move_Coroutine(duration));
    }

    public void AddListener(UnityAction action) => buttonComponent.onClick.AddListener(action);

    public void OnAnotherButtonPressed(UIButton button) {

        if (this != button || state == ButtonState.Inactive)
            state = ButtonState.NotSelected;
        
        Move(moveDuration);
    }

    private void ResetButton(InputAction.CallbackContext e) {
        ResetButton();
    }
    
    private void ResetButton() {
        state = ButtonState.Inactive;
        
        Move(moveDuration);
    }

    //Called on button
    public void ActivateButton() {
        if(state == ButtonState.Selected)
            onResetCalled?.Invoke();
        else {
            state = ButtonState.Selected;
            onButtonClicked?.Invoke(this);
        }
        
    }

    private IEnumerator Move_Coroutine(float duration) {

        buttonComponent.enabled = false;
        
        yield return new WaitForSeconds(timeUntilMove);
        
        Vector2 start = rectTransform.anchoredPosition;
        Vector2 end = new Vector2((int)state, rectTransform.anchoredPosition.y);

        float endTime = Time.time + duration;

        float lerp = 0;
        
        while (Time.time <= endTime) {
            
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, movementCurve.Evaluate(lerp));
            
            lerp += Time.deltaTime / duration;
            
            yield return null;
        }
        
        onMoveCallback?.Invoke();
        onMoveCallback = null;
        buttonComponent.enabled = true;
    }
}


