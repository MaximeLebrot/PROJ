using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UIMenuManager : MonoBehaviour {

    private List<UIMenuItemBase> uiElements;
    
    private void Awake() {
        uiElements = new List<UIMenuItemBase>();

        uiElements = GetComponentsInChildren<UIMenuItemBase>().ToList();
    }

    public List<UIMenuItemBase> GetMenuItems() => uiElements;


}
