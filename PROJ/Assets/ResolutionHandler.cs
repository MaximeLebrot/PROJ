using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour {


    private void OnEnable() {
        
        Debug.Log($"Current sr is {Screen.currentResolution}");
        (GameMenuController.Instance.RequestOption<SResolution>() as SResolution).AddListener(ChangeScreenResolution);
        
        
    }

    private void ChangeScreenResolution(int value) {
        (GameMenuController.Instance.RequestOption<SResolution>() as SResolution).GetValue();
    }
    
}
