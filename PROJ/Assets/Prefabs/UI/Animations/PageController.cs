using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    private HashSet<MenuSettings> pageObjects;

    private MenuSettings currentActivePage;

    private MenuController menuController;
    
    private void Awake() {

        menuController = GetComponentInParent<MenuController>();
        
        pageObjects = new HashSet<MenuSettings>();
        
        for (int i = 0; i < transform.childCount; i++) {

            transform.GetChild(i).gameObject.SetActive(true);
            
            transform.GetChild(i).TryGetComponent(out MenuSettings menuSettings);

            if (menuSettings != null) {
                menuSettings.Initialize();
                pageObjects.Add(menuSettings);
            }
                
            
            
            transform.GetChild(i).gameObject.SetActive(false);
                
        }
        
        menuController.OnActivatePage += SwitchPage;

    }

    private void SwitchPage(MenuSettings page) {

        if (pageObjects.Contains(page)) {

            if (currentActivePage != null) {
                currentActivePage.FadeMenu(FadeMode.FadeOut);
                currentActivePage.gameObject.SetActive(false);
            }
            
            currentActivePage = page;
            currentActivePage.gameObject.SetActive(true);
            currentActivePage.FadeMenu(FadeMode.FadeIn);
        }
        
    }
}
