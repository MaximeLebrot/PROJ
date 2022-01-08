using System;
using UnityEngine;

public class MenuButtons : MonoBehaviour {
    
    [SerializeField] private FadeGroup fadeGroup;
    
    public static event Action OnCloseDone;
    
    public void SetActive(bool activate, bool instantDeactivation) {

        if (instantDeactivation) {
            OnCloseDone?.Invoke();
            gameObject.SetActive(false);
            return;
        }
        
        if (activate) {
            gameObject.SetActive(true);
            fadeGroup.InitiateFade(FadeMode.FadeIn, null);
        }
        else 
            fadeGroup.InitiateFade(FadeMode.FadeOut, () => gameObject.SetActive(false));
    }

    
}
