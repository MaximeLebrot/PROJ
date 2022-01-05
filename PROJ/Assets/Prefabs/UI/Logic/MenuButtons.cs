using UnityEngine;

public class MenuButtons : MonoBehaviour {
    
    [SerializeField] private FadeGroup fadeGroup;
    
    public void SetActive(bool activate, bool instantDeactivation) {

        if (instantDeactivation) {
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
