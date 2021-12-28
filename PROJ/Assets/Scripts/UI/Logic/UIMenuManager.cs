using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UIMenuManager : MonoBehaviour {

    private List<UIMenuItem> uiElements;
    
    private void Awake() {
        uiElements = new List<UIMenuItem>();

        uiElements = GetComponentsInChildren<UIMenuItem>().ToList();
    }

    public List<UIMenuItem> GetMenuItems() => uiElements;


}
