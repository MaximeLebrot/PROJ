using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler {

    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public static event Action<Transform> OnHover;

    public void OnPointerEnter(PointerEventData eventData) {
        OnHover.Invoke(transform);
    }
    
}
