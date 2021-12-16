using System;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour {

    private HashSet<MenuSettings> pageObjects;

    private MenuSettings currentActivePage;

    private Action onDone;

    private MenuSettings newPage;
    
    private readonly Stack<MenuSettings> subMenuDepth = new Stack<MenuSettings>();

    //Förlåt Jonathan /martin
    GameMenuController gameMenuController;

    private void Awake() {
        gameMenuController = GetComponentInParent<GameMenuController>();


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
    }
    
    public void RegisterSubMenuAsActive(MenuSettings page) {
        SwitchPage(page);
        subMenuDepth.Push(page);
    }

    private void SwitchPage(MenuSettings page) {

        newPage = page;

        if (currentActivePage != null) {
            onDone = DisableCurrentPage;
            onDone += ActivateNewPage;
            currentActivePage.FadeMenu(FadeMode.FadeOut, onDone);
        }
        else 
            ActivateNewPage();
        
    }

    private void DisableCurrentPage() {
        currentActivePage.gameObject.SetActive(false);
        currentActivePage = null;
    }
    
    private void ActivateNewPage() {
        currentActivePage = newPage;
        currentActivePage.gameObject.SetActive(true);
        currentActivePage.FadeMenu(FadeMode.FadeIn, null);
        onDone = null;
        newPage = null;
    }

    public bool CanMoveUpOneLevel() {

        if (subMenuDepth.Count < 1)
            return false;
        
        subMenuDepth.Pop();

        if (subMenuDepth.Count > 0) {
            SwitchPage(subMenuDepth.Peek());
            return true;
        }

        return false;

    }

    //Called from scene changer buttons (beta release) /Martin
    public void CloseMenuOnSceneChange()
    {
        DisableCurrentPage();
        gameMenuController.SceneChangerCloseMenu();
        subMenuDepth.Clear();
    }
}
