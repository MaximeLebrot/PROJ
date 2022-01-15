using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettingsController : MonoBehaviour {

    public List<MenuSettings> PageObjects { get; private set; }
    
    
    public void Initialize() {
        
        PageObjects = new List<MenuSettings>();
        
        for (int i = 0; i < transform.childCount; i++) {

            transform.GetChild(i).gameObject.SetActive(true);
            
            transform.GetChild(i).TryGetComponent(out MenuSettings menuSettings);

            if (menuSettings != null) {
                menuSettings.Initialize();
                PageObjects.Add(menuSettings);
            }
            
            transform.GetChild(i).gameObject.SetActive(false);
            
        }

    }

 
    private void Start() => StartCoroutine(LateStart());

    /// <summary>
    /// Waits until end of frame to force every UIItem to fire its onValueChanged so every listener updates their value. 
    /// </summary>
    private IEnumerator LateStart() {
        
        yield return new WaitForEndOfFrame();

        foreach (var menuSetting in PageObjects) {
            menuSetting.gameObject.SetActive(true);
            menuSetting.InvokeFirstRead();
            menuSetting.gameObject.SetActive(false);
        }
        
    }

    public UIMenuItemBase FindRequestedOption<T>() {
        
        foreach (MenuSettings menuSetting in PageObjects) {
            
            if (menuSetting.HasMenuItem<T>()) 
                return menuSetting.GetOption<T>();
        }

        return null;
    }
    
}
