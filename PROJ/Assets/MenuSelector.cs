using UnityEngine;

public class MenuSelector : MonoBehaviour {


    [SerializeField] private RectTransform selectorRect;

    private void Awake() {
        MainMenuButton.OnHover += MoveSelector;
    }
    
    private void MoveSelector(Transform target) {
        selectorRect.parent = target.transform;
        selectorRect.localPosition = Vector2.zero;
    }
    
    public void Quit() => Application.Quit();
}
