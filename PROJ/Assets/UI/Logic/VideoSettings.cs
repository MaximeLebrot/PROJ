using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VideoSettings : MonoBehaviour {

    private Dictionary<int, UIMenuItem>uiElements;
    
    private void Start() {
        uiElements = new Dictionary<int, UIMenuItem>();
        
        UIMenuItem[] menuItems = GetComponentsInChildren<UIMenuItem>();

        foreach (UIMenuItem uiMenuItem in menuItems) {
            uiElements.Add(uiMenuItem.ID, uiMenuItem);

            Debug.Log($"{uiMenuItem.name} is {uiMenuItem.GetValue()}");
            
        }
            
        
    }
    
 
}
