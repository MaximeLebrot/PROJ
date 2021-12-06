using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    private Dictionary<int, GameObject> pageObjects;

    private GameObject currentActivePage;
    
    private void Awake() {

        pageObjects = new Dictionary<int, GameObject>();
        
        for (int i = 0; i < transform.childCount; i++) {

            GameObject child = transform.GetChild(i).gameObject;
            
            pageObjects.Add(child.name.GetHashCode(), child);
            
        }
        
        MenuController.OnActivatePage += ActivatePage;

    }

    private void ActivatePage(int ID) {
        
        if (pageObjects.ContainsKey(ID) == false && currentActivePage != null) {
            
            currentActivePage.SetActive(false);
            currentActivePage = null;

            return;
        }

        if (pageObjects.ContainsKey(ID)) {
            
            if(currentActivePage != null) 
                currentActivePage.SetActive(false);
            
            currentActivePage = pageObjects[ID];
            currentActivePage.SetActive(true);
        }
    }
}
