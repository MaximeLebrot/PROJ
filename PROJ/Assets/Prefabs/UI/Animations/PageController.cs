using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    private Dictionary<int, MenuSettings> pageObjects;

    private MenuSettings currentActivePage;

    private MenuController menuController;
    
    private void Awake() {

        menuController = GetComponentInParent<MenuController>();
        
        pageObjects = new Dictionary<int, MenuSettings>();
        
        for (int i = 0; i < transform.childCount; i++) {

            transform.GetChild(i).TryGetComponent(out MenuSettings menuSettings);
            
            if(menuSettings != null)
                pageObjects.Add(menuSettings.name.GetHashCode(), menuSettings);
        }
        
        menuController.OnActivatePage += SwitchPage;

    }

    private void SwitchPage(int ID) {
        
        if (pageObjects.ContainsKey(ID) == false && currentActivePage != null) {
            
            currentActivePage.gameObject.SetActive(false);
            Debug.Log(currentActivePage.name);
            currentActivePage = null;
            return;
        }
        
        if (pageObjects.ContainsKey(ID)) {

            if (currentActivePage != null) {
                currentActivePage.gameObject.SetActive(false);
            }
            
            currentActivePage = pageObjects[ID];
            currentActivePage.gameObject.SetActive(true);

        }
        
    }
}
