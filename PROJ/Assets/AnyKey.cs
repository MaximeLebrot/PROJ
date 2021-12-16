using UnityEngine;

public class AnyKey : MonoBehaviour {
    
    [SerializeField] private float speed;
    [SerializeField] private ControllerInputReference controllerInputReference;
    private CanvasGroup canvasGroup;
    
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Start() {
        controllerInputReference.InputMaster.Anykey.performed += (e) => gameObject.SetActive(false);
    }
    
    private void Update() => canvasGroup.alpha = Mathf.PingPong(Time.time, speed);
    
}
